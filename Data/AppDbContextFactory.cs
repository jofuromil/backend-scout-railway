using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using BackendScout.Models;

namespace BackendScout.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            // ✅ Cambiar a SQLite (debe coincidir con tu appsettings.json)
            optionsBuilder.UseSqlite("Data Source=ScoutDB.db");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}