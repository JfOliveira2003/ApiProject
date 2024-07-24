using ApiProject.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiProject.Data;

public class AppDbContext : DbContext{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<Client> Clients => Set<Client>();
    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     optionsBuilder.UseMySql("server=localhost;port=3306;database=ApiDb;user=root;password=Fabricio123", ServerVersion.AutoDetect("server=localhost;port=3306;database=ApiDb;user=root;password=Fabricio123"));
    // }

}