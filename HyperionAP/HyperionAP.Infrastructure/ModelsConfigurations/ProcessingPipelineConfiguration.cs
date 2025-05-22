using HyperionAP.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HyperionAP.Infrastructure.ModelsConfigurations;
public class PipelineConfiguration : IEntityTypeConfiguration<Pipeline>
{
    public void Configure(EntityTypeBuilder<Pipeline> builder)
    {
        builder.ToTable("Pipelines");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(p => p.Description)
            .IsRequired(false)
            .HasMaxLength(100);

        builder.HasOne(x => x.Project)
              .WithMany(x => x.Pipelines)
              .HasForeignKey($"{nameof(Pipeline.Project)}Id")
              .IsRequired();
    }
}
