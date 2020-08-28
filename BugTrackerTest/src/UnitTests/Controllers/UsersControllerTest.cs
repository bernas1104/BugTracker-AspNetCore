using System;

using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using BugTrackerApi.Controllers;
using BugTrackerService.Interfaces;
using BugTrackerService.ViewModels.User;
using BugTrackerTest.Bogus.ViewModels.User;

namespace BugTrackerTest.UnitTests.Controllers {
  public class UsersControllerTest {
    private readonly Mock<ICreateUserService> createUserService;
    private readonly UsersController usersController;

    public UsersControllerTest() {
      createUserService = new Mock<ICreateUserService>();
      usersController = new UsersController();
    }

    [Fact]
    public void Should_Return_201_Status_Code_If_User_Created() {
      // Arrange
      var data = BogusCreateUserViewModel.CreateUserViewModelFaker();
      var userViewModel = BogusCreateUserViewModel.UserViewModelFaker();

      createUserService.Setup(x => x.CreateNewUser(data)).Returns(userViewModel);

      // Act
      var response = usersController.Create(createUserService.Object, data);

      var actionResult = Assert.IsType<CreatedResult>(response.Result);
      var actionValue = Assert.IsType<UserViewModel>(actionResult.Value);

      // Assert
      Assert.NotNull(actionResult);
      Assert.Equal(StatusCodes.Status201Created, actionResult.StatusCode);
      Assert.True(Guid.TryParse(actionValue.Id, out Guid _));
    }

    [Fact]
    public void Should_Return_400_Status_Code_If_Invalid_ViewModel() {
      // Arrange
      var data = new CreateUserViewModel();

      // Act
      var response = usersController.Create(createUserService.Object, data);

      var actionResult = Assert.IsType<BadRequestObjectResult>(response.Result);

      // Assert
      Assert.NotNull(actionResult);
      Assert.Equal(StatusCodes.Status400BadRequest, actionResult.StatusCode);
    }
  }
}
