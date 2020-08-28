using System;

using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

using BugTrackerDomain;
using BugTrackerService.Interfaces;
using BugTrackerService.ViewModels.Session;
using BugTrackerPersistence.Repositories.Interfaces;

namespace BugTrackerService.Implementations {
  public class AuthService : IAuthService {
    private readonly IUsersRepository usersRepository;
    private readonly IPasswordHasher<User> passwordHasher;
    private readonly IMapper mapper;
    private readonly IConfiguration configuration;

    public AuthService(
      IUsersRepository usersRepository,
      IPasswordHasher<User> passwordHasher,
      IMapper mapper,
      IConfiguration configuration
    ) {
      this.usersRepository = usersRepository;
      this.passwordHasher = passwordHasher;
      this.mapper = mapper;
      this.configuration = configuration;
    }

    public SessionViewModel AuthenticateUser(LoginViewModel data) {
      var user = usersRepository.FindByEmail(data.Email);

      if (user == null)
        throw new Exception();

      var checkPassword = passwordHasher
        .VerifyHashedPassword(user, user.Password, data.Password);

      if (checkPassword == PasswordVerificationResult.Failed)
        throw new Exception();

      if (checkPassword == PasswordVerificationResult.SuccessRehashNeeded) {
        user.Password = passwordHasher.HashPassword(user, data.Password);
        usersRepository.SaveChanges();
      }

      var token = TokenService.GenerateToken(configuration, user);

      return new SessionViewModel {
        Id = user.Id,
        Email = user.Email,
        ValidFrom = token.ValidFrom,
        ValidTo = token.ValidTo,
        Token = token.Token,
      };
    }
  }
}
