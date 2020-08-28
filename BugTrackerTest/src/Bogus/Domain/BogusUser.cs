using System;

using Bogus;

using BugTrackerDomain;

namespace BugTrackerTest.Bogus.Domain {
  public static class BogusUser {
    public static User UserFaker() {
      var user = new Faker<User>()
        .RuleFor(x => x.Id, () => Guid.NewGuid().ToString())
        .RuleFor(x => x.Email, (f) => f.Internet.Email())
        .RuleFor(x => x.FirstName, (f) => f.Person.FirstName)
        .RuleFor(x => x.LastName, (f) => f.Person.LastName)
        .RuleFor(x => x.Password, (f) => f.Internet.Password())
        .RuleFor(x => x.CreatedAt, () => DateTime.Now)
        .RuleFor(x => x.UpdatedAt, (_, u) => u.CreatedAt);

      return user.Generate();
    }
  }
}
