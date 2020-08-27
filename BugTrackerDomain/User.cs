using System;
using System.Collections.Generic;

namespace BugTrackerDomain {
  public class User {
    public string Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public bool? EmailConfirmed { get; set; }
    public string GithubUsername { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    public virtual IEnumerable<UserToken> UserTokens { get; set; }
  }
}
