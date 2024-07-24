using NSwag.AspNetCore;
using ApiProject.Data;
using ApiProject.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var conn = builder.Configuration.GetConnectionString("AppConn");
builder.Services.AddDbContext<AppDbContext>(options => {
    options.UseMySql(conn, ServerVersion.AutoDetect(conn));
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(config =>
{
    config.DocumentName = "ApiProject";
    config.Title = "Bank Clients Api 1.0";
    config.Version = "v1";
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi(config =>
    {
        config.DocumentTitle = "ClientsAPI";
        config.Path = "/swagger";
        config.DocumentPath = "/swagger/{documentName}/swagger.json";
        config.DocExpansion = "list";
    });
}

app.MapGet("/ClientItems", async (AppDbContext db)=> (
    await db.Clients.ToListAsync()
));
app.MapGet("/ClientItem/{id}", async (int id, AppDbContext db) => 
    await db.Clients.FindAsync(id)
        is Client client ? Results.Ok(client) : Results.NotFound()
);

app.MapPost("/ClientItems", async (Client client ,AppDbContext db) => {
    db.Clients.Add(client);
    await db.SaveChangesAsync();

    return Results.Created($"/ClientItems/{client.Id}", client);
    }
);

app.MapPut("/ClientItems/{id}", async(int id, Client inputClient, AppDbContext db) =>{
    var client = await db.Clients.FindAsync(id);

    if(client is null) return Results.NotFound(); // --> Early return

    client.Name = inputClient.Name;

    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/ClientItems/{id}", async(int id, AppDbContext db) => {
    var client = await db.Clients.FindAsync(id);
    if(client is null) return Results.NotFound();
    
    db.Clients.Remove(client);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();

