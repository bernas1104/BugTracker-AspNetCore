using BugTrackerDomain;

namespace BugTrackerPersistence.Repositories.Interfaces {
  public interface IUsersRepository {
    User FindByEmail(string email);
    User CreateUser(User user);
    void SaveChanges();
  }
}
