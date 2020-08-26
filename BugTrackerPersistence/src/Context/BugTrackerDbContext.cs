using Microsoft.EntityFrameworkCore;

using BugTrackerDomain;

namespace BugTrackerPersistence.Context {
  public class BugTrackerDbContext : DbContext {
    public DbSet<User> Users { get; set; }
    public DbSet<UserToken> UserTokens { get; set; }

    public BugTrackerDbContext(DbContextOptions<BugTrackerDbContext> options)
      : base(options) {}

    protected override void OnModelCreating(ModelBuilder builder) {
      // TODO
    }
  }
}
