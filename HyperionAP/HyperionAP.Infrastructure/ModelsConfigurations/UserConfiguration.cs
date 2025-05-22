using HyperionAP.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HyperionAP.Infrastructure.ModelsConfigurations;
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.DisplayName)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(p => p.Login)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasMany(x => x.Projects)
            .WithOne(x => x.Owner)
            .HasForeignKey($"{nameof(Project.Owner)}Id");
    }
}
