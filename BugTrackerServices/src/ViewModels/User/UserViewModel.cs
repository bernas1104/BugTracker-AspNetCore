using System;

namespace BugTrackerService.ViewModels.User {
  public class UserViewModel {
    public string Id { get; set; }
    public string Email { get; set; }
    public string FullName { get; set; }
    public string GithubUsername { get; set; }
    public DateTime CreatedAt { get; set; }
  }
}
