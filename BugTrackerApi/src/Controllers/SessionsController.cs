using Microsoft.AspNetCore.Mvc;

using BugTrackerService.ViewModels.Session;

namespace BugTrackerApi.Controllers {
  [ApiController]
  [Route("v1/sessions")]
  [Produces("application/json")]
  public class SessionsController : ControllerBase {
    public ActionResult<SessionViewModel> Create(
      // [FromServices] ,
      [FromBody] LoginViewModel data
    ) {
      data.Validate();

      if (data.Invalid)
        return BadRequest(data.Notifications);

      return Created(nameof(Create), "");
    }
  }
}
