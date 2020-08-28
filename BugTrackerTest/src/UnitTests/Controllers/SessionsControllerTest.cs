using System;

using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using BugTrackerApi.Controllers;
using BugTrackerService.Interfaces;
using BugTrackerService.ViewModels.Session;
using BugTrackerTest.Bogus.ViewModels.Session;

namespace BugTrackerTest.UnitTests.Controllers {
  public class SessionsControllerTest {
    private readonly Mock<IAuthService> authService;
    private readonly SessionsController sessionsController;

    public SessionsControllerTest() {
      authService = new Mock<IAuthService>();
      sessionsController = new SessionsController();
    }

    [Fact]
    public void Should_Return_201_Status_Code_If_User_Authenticated() {
      // Arrange
      var data = BogusSessionViewModels.LoginViewModelFaker();
      var sessionViewModel = BogusSessionViewModels.SessionViewModelFaker();

      authService.Setup(x => x.AuthenticateUser(data)).Returns(sessionViewModel);

      // Act
      var response = sessionsController.Create(authService.Object, data);

      var actionResult = Assert.IsType<CreatedResult>(response.Result);
      var actionValue = Assert.IsType<SessionViewModel>(actionResult.Value);

      // Assert
      Assert.NotNull(actionResult);
      Assert.Equal(StatusCodes.Status201Created, actionResult.StatusCode);
      Assert.True(Guid.TryParse(actionValue.Id, out Guid _));
    }

    [Fact]
    public void Should_Return_400_Status_Code_If_Invalid_ViewModel() {
      // Arrange
      var data = new LoginViewModel();

      // Act
      var response = sessionsController.Create(authService.Object, data);

      var actionResult = Assert.IsType<BadRequestObjectResult>(response.Result);

      // Assert
      Assert.NotNull(actionResult);
      Assert.Equal(StatusCodes.Status400BadRequest, actionResult.StatusCode);
    }
  }
}
