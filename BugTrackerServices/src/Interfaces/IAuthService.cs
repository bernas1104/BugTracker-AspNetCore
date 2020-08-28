using BugTrackerService.ViewModels.Session;

namespace BugTrackerService.Interfaces {
  public interface IAuthService {
    SessionViewModel AuthenticateUser(LoginViewModel data);
  }
}
