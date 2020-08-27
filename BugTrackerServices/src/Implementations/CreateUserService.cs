using System;

using AutoMapper;
using Microsoft.AspNetCore.Identity;

using BugTrackerDomain;
using BugTrackerService.Interfaces;
using BugTrackerService.ViewModels.User;
using BugTrackerPersistence.Repositories.Interfaces;

namespace BugTrackerService.Implementations {
  public class CreateUserService : ICreateUserService {
    private readonly IUsersRepository usersRepository;
    private readonly IPasswordHasher<User> passwordHasher;
    private readonly IMapper mapper;

    public CreateUserService(
      IUsersRepository usersRepository,
      IPasswordHasher<User> passwordHasher,
      IMapper mapper
    ) {
      this.usersRepository = usersRepository;
      this.passwordHasher = passwordHasher;
      this.mapper = mapper;
    }

    public UserViewModel CreateNewUser(CreateUserViewModel data) {
      var user = usersRepository.FindByEmail(data.Email);

      if (user != null)
        throw new Exception();

      user = mapper.Map<User>(data);
      user.Password = passwordHasher.HashPassword(user, data.Password);
      user = usersRepository.CreateUser(user);

      return mapper.Map<UserViewModel>(user);
    }
  }
}
