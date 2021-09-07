﻿using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Teng.FileManagement.EntityFrameworkCore
{
    public class FileManagementHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<FileManagementHttpApiHostMigrationsDbContext>
    {
        public FileManagementHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
        {
            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<FileManagementHttpApiHostMigrationsDbContext>()
                .UseMySql(configuration.GetConnectionString("FileManagement"));

            return new FileManagementHttpApiHostMigrationsDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}