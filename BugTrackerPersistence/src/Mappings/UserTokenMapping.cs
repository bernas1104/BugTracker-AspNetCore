using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using BugTrackerDomain;

namespace BugTrackerPersistence.Mappings {
  public class UserTokenMapping : IEntityTypeConfiguration<UserToken> {
    public void Configure(EntityTypeBuilder<UserToken> builder) {
      builder.HasKey(x => x.Id);

      builder.Property(x => x.Token)
        .HasMaxLength(50)
        .HasDefaultValue(Guid.NewGuid().ToString());
      builder.Property(x => x.UserId).IsRequired();
      builder.Property(x => x.CreatedAt).HasDefaultValue(DateTime.Now);
      builder.Property(x => x.UpdatedAt)
        .ValueGeneratedOnUpdate()
        .HasDefaultValue(DateTime.Now);
      builder.Property(x => x.DeletedAt).IsRequired(false);

      builder.HasOne(x => x.User)
        .WithMany(x => x.UserTokens)
        .HasForeignKey(x => x.UserId);
    }
  }
}
