using System;

using Bogus;

using BugTrackerService.ViewModels.User;

namespace BugTrackerTest.Bogus.ViewModels.User {
  public static class BogusUserViewModels {
    public static UserViewModel UserViewModelFaker() {
      var userViewModel = new Faker<UserViewModel>()
        .RuleFor(x => x.Id, () => Guid.NewGuid().ToString())
        .RuleFor(x => x.Email, (f) => f.Internet.Email())
        .RuleFor(x => x.FullName, (f) => f.Person.FullName)
        .RuleFor(x => x.CreatedAt, () => DateTime.Now);

      return userViewModel.Generate();
    }

    public static CreateUserViewModel CreateUserViewModelFaker() {
      var createUserViewModel = new Faker<CreateUserViewModel>()
        .RuleFor(x => x.Email, (f) => f.Internet.Email())
        .RuleFor(x => x.Password, (f) => f.Internet.Password())
        .RuleFor(x => x.PasswordConfirmation, (_, u) => u.Password)
        .RuleFor(x => x.FirstName, (f) => f.Person.FirstName)
        .RuleFor(x => x.LastName, (f) => f.Person.LastName);

      return createUserViewModel.Generate();
    }
  }
}
