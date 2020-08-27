using BugTrackerService.ViewModels.User;

namespace BugTrackerService.Interfaces {
  public interface ICreateUserService {
    UserViewModel CreateNewUser(CreateUserViewModel data);
  }
}
