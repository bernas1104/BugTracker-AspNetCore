using System;

using Moq;
using Xunit;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

using BugTrackerDomain;
using BugTrackerService.Interfaces;
using BugTrackerService.Implementations;
using BugTrackerService.ViewModels.User;
using BugTrackerTest.Bogus.ViewModels.User;
using BugTrackerPersistence.Repositories.Interfaces;

namespace BugTrackerTest.UnitTests.Services {
  public class CreateUserServiceTest {
    private readonly Mock<IUsersRepository> usersRepository;
    private readonly Mock<IPasswordHasher<User>> passwordHasher;
    private readonly Mock<IMapper> mapper;
    private readonly ICreateUserService createUserService;

    public CreateUserServiceTest() {
      usersRepository = new Mock<IUsersRepository>();
      passwordHasher = new Mock<IPasswordHasher<User>>();
      mapper = new Mock<IMapper>();

      createUserService = new CreateUserService(
        usersRepository.Object,
        passwordHasher.Object,
        mapper.Object
      );
    }

    [Fact]
    public void Should_Be_Able_To_Create_New_User() {
      // Arrange
      var data = BogusUserViewModels.CreateUserViewModelFaker();

      var user = new User() {
        Id = Guid.NewGuid().ToString(),
        Email = data.Email,
        FirstName = data.FirstName,
        LastName = data.LastName,
        Password = data.Password,
        CreatedAt = DateTime.Now,
      };

      usersRepository.Setup(x => x.FindByEmail(data.Email)).Returns((User)null);
      mapper.Setup(x => x.Map<User>(data)).Returns(user);
      passwordHasher.Setup(x => x.HashPassword(user, data.Password))
        .Returns(data.Password);
      usersRepository.Setup(x => x.CreateUser(user)).Returns(user);

      var userViewModel = new UserViewModel() {
        Id = user.Id,
        Email = user.Email,
        FullName = user.FirstName + user.LastName,
        GithubUsername = null,
        CreatedAt = user.CreatedAt
      };

      mapper.Setup(x => x.Map<UserViewModel>(user)).Returns(userViewModel);

      // Act
      var response = createUserService.CreateNewUser(data);

      // Assert
      Assert.True(Guid.TryParse(response.Id, out Guid _));
      Assert.Equal(data.Email, response.Email);
      Assert.Equal(data.FirstName + data.LastName, response.FullName);
    }

    [Fact]
    public void Should_Not_Be_Able_To_Create_New_User_If_Email_Not_Unique() {
      // Arrange
      var data = BogusUserViewModels.CreateUserViewModelFaker();

      usersRepository.Setup(x => x.FindByEmail(data.Email)).Returns(new User());

      // Act

      // Assert
      Assert.Throws<Exception>(
        () => createUserService.CreateNewUser(data)
      );
    }
  }
}
