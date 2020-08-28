using Microsoft.AspNetCore.Mvc;

using BugTrackerService.Interfaces;
using BugTrackerService.ViewModels.Session;

namespace BugTrackerApi.Controllers {
  [ApiController]
  [Route("v1/sessions")]
  [Produces("application/json")]
  public class SessionsController : ControllerBase {
    public ActionResult<SessionViewModel> Create(
      [FromServices] IAuthService authService,
      [FromBody] LoginViewModel data
    ) {
      data.Validate();

      if (data.Invalid)
        return BadRequest(data.Notifications);

      var session = authService.AuthenticateUser(data);

      return Created(nameof(Create), session);
    }
  }
}
