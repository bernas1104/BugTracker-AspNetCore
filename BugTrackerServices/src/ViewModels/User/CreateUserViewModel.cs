using Flunt.Validations;
using Flunt.Notifications;
using System.Collections.Generic;

namespace BugTrackerService.ViewModels.User {
  public class CreateUserViewModel : Notifiable, IValidatable {
    public string Email { get; set; }
    public string Password { get; set; }
    public string PasswordConfirmation { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public void Validate() {
      var EmailContract = new Contract()
        .IsNotNullOrEmpty(Email, "Email", "Is required");

      var PasswordContract = new Contract()
        .IsNotNullOrEmpty(Password, "Password", "Is required");

      var PasswordConfirmationContract = new Contract()
        .IsNotNullOrEmpty(PasswordConfirmation, "Password Confirmation", "IsRequired");

      var FirstNameContract = new Contract()
        .IsNotNullOrEmpty(FirstName, "First Name", "Is required");

      var LastNameContract = new Contract()
        .IsNotNullOrEmpty(LastName, "Last Name", "Is required");

      EmailContract.IfNotNull(Email, contract => (
        contract
          .IsEmail(Email, "Email", "Should be a valid e-mail")
          .HasMaxLen(Email, 50, "Email", "Should be at most 50 characters")
      ));

      PasswordContract.IfNotNull(Password, contract => (
        contract
          .HasMinLen(Password, 6, "Password", "Should be at least 6 characters")
          .HasMaxLen(Password, 12, "Password", "Should be at most 12 characters")
      ));

      PasswordConfirmationContract.IfNotNull(PasswordConfirmation, contract => (
        contract
          .HasMinLen(PasswordConfirmation, 6, "Password", "Should be at least 6 characters")
          .HasMaxLen(PasswordConfirmation, 12, "Password", "Should be at most 12 characters")
          .AreEquals(
            PasswordConfirmation,
            Password,
            "Password Confirmation",
            "Should match 'password'",
            System.StringComparison.Ordinal
          )
      ));

      FirstNameContract.IfNotNull(FirstName, contract => (
        contract
          .HasMinLen(FirstName, 2, "First Name", "Should be at least 2 characters")
          .HasMaxLen(FirstName, 20, "First Name", "Should be at most 20 characters")
      ));

      LastNameContract.IfNotNull(LastName, contract => (
        contract
          .HasMinLen(LastName, 2, "Last Name", "Should be at least 2 characters")
          .HasMaxLen(LastName, 20, "Last Name", "Should be at least 20 characters")
      ));

      AddNotifications(
        EmailContract,
        PasswordContract,
        PasswordConfirmationContract,
        FirstNameContract,
        LastNameContract
      );
    }
  }
}
