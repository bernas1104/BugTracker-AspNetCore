using System;

namespace BugTrackerDomain {
  public class UserToken {
    public string Id { get; set; }
    public string Token { get; set; }
    public string UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    public User User { get; set; }
  }
}
