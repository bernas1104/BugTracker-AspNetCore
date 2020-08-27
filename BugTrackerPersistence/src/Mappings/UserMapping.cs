using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using BugTrackerDomain;

namespace BugTrackerPersistence.Mappings {
  public class UserMapping : IEntityTypeConfiguration<User> {
    public void Configure(EntityTypeBuilder<User> builder) {
      builder.HasKey(x => x.Id);

      builder.Property(x => x.Id).ValueGeneratedOnAdd();
      builder.Property(x => x.Email).HasMaxLength(100).IsRequired();
      builder.Property(x => x.Password).HasMaxLength(255).IsRequired();
      builder.Property(x => x.FirstName).HasMaxLength(20).IsRequired();
      builder.Property(x => x.LastName).HasMaxLength(20).IsRequired(false);
      builder.Property(x => x.EmailConfirmed).HasDefaultValue(false);
      builder.Property(x => x.GithubUsername).HasMaxLength(50).IsRequired(false);
      builder.Property(x => x.CreatedAt).HasDefaultValue(DateTime.Now);
      builder.Property(x => x.UpdatedAt).HasDefaultValue(DateTime.Now);
      builder.Property(x => x.DeletedAt).IsRequired(false);

      builder.HasMany(x => x.UserTokens)
        .WithOne(x => x.User)
        .HasForeignKey(x => x.UserId);
    }
  }
}
