using AutoMapper;

using BugTrackerDomain;
using BugTrackerService.ViewModels.User;

namespace BugTrackerApi.Configurations {
  public class AutoMapperConfiguration : Profile {
    public AutoMapperConfiguration() {
      CreateMap<User, UserViewModel>().ForMember(
        viewModel => viewModel.FullName, mapper => mapper.MapFrom(
          user => user.FirstName + user.LastName
        )
      );
      CreateMap<CreateUserViewModel, User>();
    }
  }
}
