using BookStore.Repositories;
using System.Collections.Generic;
using BookStore.Models.DtoModels;
using BookStore.Models.ViewModels;
using Microsoft.Extensions.Configuration;

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
                    UserId = userDto.UserId,
                    FirstName = userDto.FirstName,
                    LastName = userDto.LastName,
                    Email = userDto.Email,
                    PhoneNumber = userDto.PhoneNumber
                });
            }

            return result;
        }
    }
}
