using Bogus;

using BugTrackerService.ViewModels.User;

namespace BugTrackerTest.Bogus.ViewModels.User {
  public static class BogusCreateUserViewModel {
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
