using System;

namespace BugTrackerService.ViewModels.Session {
  public class SessionViewModel {
    public string Id { get; set; }
    public string Email { get; set; }
    public DateTime ValidFrom { get; set; }
    public DateTime ValidTo { get; set; }
    public string Token { get; set; }
  }
}
