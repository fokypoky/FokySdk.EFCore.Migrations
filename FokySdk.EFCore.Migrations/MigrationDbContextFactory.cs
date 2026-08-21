using FokySdk.EFCore.Migrations.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FokySdk.EFCore.Migrations
{
    public class MigrationDbContextFactory<T> : IDesignTimeDbContextFactory<T> where T : DbContext
    {
        public T CreateDbContext(string[] args)
        {
            var migrationOptions = ArgumentParser.ParseArguments(args);
            
            var optionsBuilder = new DbContextOptionsBuilder<T>();
            optionsBuilder.UseNpgsql(migrationOptions.ToConnectionString());

            var instance = (T)Activator.CreateInstance(typeof(T), optionsBuilder.Options) ?? throw new Exception($"Can't create instance of {typeof(T).Name}");
            return instance;
        }
    }
}