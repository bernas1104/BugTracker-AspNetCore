using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

using BugTrackerDomain;
using BugTrackerService.Interfaces;
using BugTrackerService.Implementations;
using BugTrackerPersistence.Repositories;
using BugTrackerPersistence.Repositories.Interfaces;

namespace BugTrackerApi.Configurations {
  public static class DependencyInjectionConfiguration {
    public static IServiceCollection ResolveDependencies(
      this IServiceCollection services
    ) {
      services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
      services.AddScoped<IUsersRepository, UsersRepository>();

      services.AddTransient<ICreateUserService, CreateUserService>();
      services.AddTransient<IAuthService, AuthService>();

      return services;
    }
  }
}
