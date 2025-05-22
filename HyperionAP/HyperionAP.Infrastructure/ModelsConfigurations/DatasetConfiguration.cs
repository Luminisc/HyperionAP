using HyperionAP.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HyperionAP.Infrastructure.ModelsConfigurations;

public class DatasetConfiguration : IEntityTypeConfiguration<Dataset>
{
    public void Configure(EntityTypeBuilder<Dataset> builder)
    {
        builder.ToTable("Datasets");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(p => p.Description)
            .IsRequired(false)
            .HasMaxLength(100);
        builder.Property(p => p.Filename)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(o => o.DatasetType)
            .HasConversion<string>();

        builder.HasOne(x => x.Project)
           .WithMany(x => x.Datasets)
           .HasForeignKey($"{nameof(Dataset.Project)}Id")
           .IsRequired();
    }
}
