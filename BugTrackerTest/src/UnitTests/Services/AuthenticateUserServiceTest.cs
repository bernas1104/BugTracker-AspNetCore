using System;

using Moq;
using Xunit;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

using BugTrackerDomain;
using BugTrackerTest.Bogus.Domain;
using BugTrackerService.Interfaces;
using BugTrackerService.Implementations;
using BugTrackerService.ViewModels.Session;
using BugTrackerTest.Bogus.ViewModels.Session;
using BugTrackerPersistence.Repositories.Interfaces;

namespace BugTrackerTest.UnitTests.Services {
  public class AuthenticateUserServiceTest {
    private readonly Mock<IUsersRepository> usersRepository;
    private readonly Mock<IPasswordHasher<User>> passwordHasher;
    private readonly Mock<IMapper> mapper;
    private readonly Mock<IConfiguration> configuration;
    private readonly IAuthService authService;

    public AuthenticateUserServiceTest() {
      usersRepository = new Mock<IUsersRepository>();
      passwordHasher = new Mock<IPasswordHasher<User>>();
      mapper = new Mock<IMapper>();
      configuration = new Mock<IConfiguration>();

      authService = new AuthService(
        usersRepository.Object,
        passwordHasher.Object,
        mapper.Object,
        configuration.Object
      );

      configuration.Setup(x => x["JWTSecret"])
        .Returns("fedaf7d8863b48e197b9287d492b708e");
    }

    [Fact]
    public void Should_Be_Able_To_Authencticate_User() {
      // Arange
      var data = BogusSessionViewModels.LoginViewModelFaker();

      var user = BogusUser.UserFaker();
      user.Email = data.Email;
      user.Password = data.Password;

      var sessionViewModels = BogusSessionViewModels.SessionViewModelFaker();
      sessionViewModels.Id = user.Id;
      sessionViewModels.Email = user.Email;

      usersRepository.Setup(x => x.FindByEmail(data.Email)).Returns(user);
      passwordHasher.Setup(x => x.VerifyHashedPassword(user, user.Password, data.Password))
        .Returns(PasswordVerificationResult.Success);

      // Act
      var response = authService.AuthenticateUser(data);

      // Assert
      Assert.NotNull(response);
      Assert.True(Guid.TryParse(response.Id, out Guid _));
    }

    [Fact]
    public void Should_Be_Able_To_Authencticate_User_If_Password_Rehash_Needed() {
      // Arange
      var data = BogusSessionViewModels.LoginViewModelFaker();

      var user = BogusUser.UserFaker();
      user.Email = data.Email;
      user.Password = data.Password;

      var sessionViewModels = BogusSessionViewModels.SessionViewModelFaker();
      sessionViewModels.Id = user.Id;
      sessionViewModels.Email = user.Email;

      usersRepository.Setup(x => x.FindByEmail(data.Email)).Returns(user);
      passwordHasher.Setup(x => x.VerifyHashedPassword(user, user.Password, data.Password))
        .Returns(PasswordVerificationResult.SuccessRehashNeeded);
      passwordHasher.Setup(x => x.HashPassword(user, data.Password))
        .Returns("new-hashed-password");
      usersRepository.Setup(x => x.SaveChanges());

      // Act
      var response = authService.AuthenticateUser(data);

      // Assert
      Assert.NotNull(response);
      Assert.True(Guid.TryParse(response.Id, out Guid _));
    }

    [Fact]
    public void Should_Not_Authenticate_If_User_With_Email_Not_Exist() {
      // Arange
      var data = BogusSessionViewModels.LoginViewModelFaker();

      usersRepository.Setup(x => x.FindByEmail(data.Email)).Returns((User)null);

      // Act

      // Assert
      Assert.Throws<Exception>(() => authService.AuthenticateUser(data));
    }

    [Fact]
    public void Should_Not_Authenticate_If_Password_Dont_Match() {
      // Arange
      var data = BogusSessionViewModels.LoginViewModelFaker();

      var user = BogusUser.UserFaker();
      user.Email = data.Email;
      user.Password = data.Password;

      usersRepository.Setup(x => x.FindByEmail(data.Email)).Returns(user);
      passwordHasher.Setup(x => x.VerifyHashedPassword(user, user.Password, data.Password))
        .Returns(PasswordVerificationResult.Failed);

      // Act

      // Assert
      Assert.Throws<Exception>(() => authService.AuthenticateUser(data));
    }
  }
}
