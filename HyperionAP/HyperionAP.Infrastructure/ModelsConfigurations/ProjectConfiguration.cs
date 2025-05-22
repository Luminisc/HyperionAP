using HyperionAP.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HyperionAP.Infrastructure.ModelsConfigurations;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.ToTable("Projects");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasOne(x => x.Owner)
            .WithMany(x => x.Projects)
            .HasForeignKey($"{nameof(Project.Owner)}Id")
            .IsRequired();

        builder.HasMany(x => x.Datasets)
            .WithOne(x => x.Project)
            //.HasForeignKey(x => $"{nameof(Dataset.Project)}Id")
            ;
        builder.HasMany(x => x.Pipelines)
            .WithOne(x => x.Project)
            //.HasForeignKey(x => $"{nameof(Pipeline.Project)}Id")
            ;
    }
}
