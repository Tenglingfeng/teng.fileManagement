using System;
using Microsoft.EntityFrameworkCore;
using Teng.FileManagement.Files;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Teng.FileManagement.EntityFrameworkCore
{
    public static class FileManagementDbContextModelCreatingExtensions
    {
        public static void ConfigureFileManagement(
            this ModelBuilder builder,
            Action<FileManagementModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new FileManagementModelBuilderConfigurationOptions(
                FileManagementDbProperties.DbTablePrefix,
                FileManagementDbProperties.DbSchema
            );

            optionsAction?.Invoke(options);

            builder.Entity<File>(b =>
            {
                //Configure table & schema name
                b.ToTable(options.TablePrefix + "Files", options.Schema);

                b.ConfigureByConvention();

                //Properties
                b.Property(q => q.FileName).IsRequired().HasMaxLength(FileConsts.MaxFileNameLength);
                b.Property(q => q.BlobName).IsRequired().HasMaxLength(FileConsts.MaxBlobNameLength);
                b.Property(q => q.ByteSize).IsRequired();
            });
        }
    }
}