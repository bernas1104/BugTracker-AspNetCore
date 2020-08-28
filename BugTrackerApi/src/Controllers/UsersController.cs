using Microsoft.AspNetCore.Mvc;

using BugTrackerService.Interfaces;
using BugTrackerService.ViewModels.User;
using Microsoft.AspNetCore.Authorization;

namespace BugTrackerApi.Controllers {
  [ApiController]
  [Route("v1/[controller]")]
  [Produces("application/json")]
  public class UsersController : ControllerBase {
    [HttpPost]
    [Route("")]
    [AllowAnonymous]
    public ActionResult<UserViewModel> Create(
      [FromServices] ICreateUserService service,
      [FromBody] CreateUserViewModel user
    ) {
      user.Validate();

      if (user.Invalid)
        return BadRequest(user.Notifications);

      var createdUser = service.CreateNewUser(user);

      return Created(nameof(Create), createdUser);
    }
  }
}
