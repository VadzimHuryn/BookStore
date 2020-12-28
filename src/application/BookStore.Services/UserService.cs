using BookStore.Repositories;
using System.Collections.Generic;
using BookStore.Models.DtoModels;
using BookStore.Models.ViewModels;
using Microsoft.Extensions.Configuration;
using System;

namespace BookStore.Services
{
    public class UserService
    {
        private UserRepository _userRepository;

        public UserService(IConfiguration configuration) 
        {
            _userRepository = new UserRepository(configuration.GetConnectionString("DefaultConnection"));
        }

        public List<User> GetAllUsers()
        {
            var result = new List<User>();
            var userDtos = _userRepository.GetAll();

            foreach(var userDto in userDtos)
            {
                result.Add(new User()
                {
                    Id = userDto.Id,
                    UserId = userDto.UserId,
                    FirstName = userDto.FirstName,
                    LastName = userDto.LastName,
                    Email = userDto.Email,
                    PhoneNumber = userDto.PhoneNumber,
                    UserRoleId = userDto.UserRoleId
                });
            }

            return result;
        }

        public List<UserRole> GetUserRoles()
        {
            var result = new List<UserRole>();

            var userRoleDtos = _userRepository.GetUserRoles();

            foreach(var userRoleDto in userRoleDtos)
            {
                result.Add(new UserRole()
                {
                    Id = userRoleDto.Id,
                    Code = userRoleDto.Code,
                    Name = userRoleDto.Name,
                    Comment = userRoleDto.Comment
                });
            }

            return result;
        }

        public int Add(User user)
        {
            var dto = new UserDto()
            {
                UserId = user.UserId,
                Password = user.Password ?? string.Empty,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                UserRoleId = user.UserRoleId,
                LastLoginDateTime = DateTime.Now
            };

            return _userRepository.Add(dto);
        }

        public void Update(UserUpdate user)
        {
            var currentUser = _userRepository.GetByUserId(user.UserId);

            currentUser.FirstName = user.FirstName;
            currentUser.LastName = user.LastName;
            currentUser.PhoneNumber = user.PhoneNumber;
            currentUser.Email = user.Email;

            _userRepository.Update(currentUser);
        }

        public User GetUserByUserLogin(UserLogin userLogin)
        {
            var userDto = _userRepository.GetByUserId(userLogin.UserId);

            if (userDto == null)
                return null;

            return new User()
            {
                Id = userDto.Id,
                UserId = userDto.UserId,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                PhoneNumber = userDto.PhoneNumber,
                UserRoleId = userDto.UserRoleId
            };
        }

    }
}
