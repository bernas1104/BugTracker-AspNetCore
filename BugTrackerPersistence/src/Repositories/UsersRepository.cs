using System;
using System.Linq;

using Microsoft.EntityFrameworkCore;

using BugTrackerDomain;
using BugTrackerPersistence.Context;
using BugTrackerPersistence.Repositories.Interfaces;

namespace BugTrackerPersistence.Repositories {
  public class UsersRepository : IUsersRepository {
    private readonly BugTrackerDbContext dbContext;

    public UsersRepository(BugTrackerDbContext dbContext) {
      this.dbContext = dbContext;
    }

    public User FindByEmail(string email) {
      return dbContext.Users.FirstOrDefault(x => x.Email == email);
    }

    public User CreateUser(User user) {
      dbContext.Users.Add(user);
      SaveChanges();

      return user;
    }

    private void SaveChanges() {
      dbContext.SaveChanges();
    }
  }
}
