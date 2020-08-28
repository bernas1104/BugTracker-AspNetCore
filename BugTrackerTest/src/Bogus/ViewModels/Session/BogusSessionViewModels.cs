using System;

using Bogus;

using BugTrackerService.ViewModels.Session;

namespace BugTrackerTest.Bogus.ViewModels.Session {
  public static class BogusSessionViewModels {
    public static LoginViewModel LoginViewModelFaker() {
      var loginViewModel = new Faker<LoginViewModel>()
        .RuleFor(x => x.Email, (f) => f.Internet.Email())
        .RuleFor(x => x.Password, (f) => f.Internet.Password());

      return loginViewModel.Generate();
    }

    public static SessionViewModel SessionViewModelFaker() {
      var sessionViewModel = new Faker<SessionViewModel>()
        .RuleFor(x => x.Id, () => Guid.NewGuid().ToString())
        .RuleFor(x => x.Email, (f) => f.Internet.Email())
        .RuleFor(x => x.ValidFrom, () => DateTime.UtcNow)
        .RuleFor(x => x.ValidTo, () => DateTime.UtcNow.AddDays(1));

      return sessionViewModel.Generate();
    }
  }
}
