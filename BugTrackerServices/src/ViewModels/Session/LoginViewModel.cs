using Flunt.Validations;
using Flunt.Notifications;

namespace BugTrackerService.ViewModels.Session {
  public class LoginViewModel : Notifiable, IValidatable {
    public string Email { get; set; }
    public string Password { get; set; }

    public void Validate() {
      var EmailContract = new Contract()
        .IsNotNullOrEmpty(Email, "E-mail", "Is required for login");

      var PasswordContract = new Contract()
        .IsNotNullOrEmpty(Password, "Password", "Is required for login");

      EmailContract.IfNotNull(Email, contract => (
        contract
          .IsEmail(Email, "E-mail", "Should be a valid e-mail")
          .HasMaxLen(Email, 100, "E-mail", "Should have at most 100 characters")
      ));

      PasswordContract.IfNotNull(Password, contract => (
        contract
          .HasMinLen(Password, 6, "Password", "Should have at least 6 characters")
          .HasMaxLen(Password, 12, "Password", "Should have at most 12 characters")
      ));

      AddNotifications(EmailContract, PasswordContract);
    }
  }
}
