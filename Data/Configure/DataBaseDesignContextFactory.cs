using ApiProject.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ApiProject.Configure;

// ReSharper disable once UnusedType.Global
public class DataBaseDesignContextFactory 
    : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<AppDbContext>();
        builder.UseMySql("server=localhost;port=3306;database=ApiDb;user=root;password=Fabricio123", ServerVersion.AutoDetect("server=localhost;port=3306;database=ApiDb;user=root;password=Fabricio123"));
        return new AppDbContext(builder.Options);
    }
}
