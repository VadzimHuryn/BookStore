
using BookStore.Models.DtoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Repositories
{
    public class UserRepository : BaseRepository
    {
        public UserRepository(string connectionString) : base(connectionString) { }

        public List<UserDto> GetAll()
        {
            var result = Query<UserDto>($@"SELECT * FROM [dbo].[User]").ToList();
            return result;
        }

        public List<UserDto> GetAllAvailable()
        {
            var result = Query<UserDto>($@"SELECT * FROM [dbo].[User] WHERE [IsDisabled] = 0").ToList();
            return result;
        }

        public List<UserRoleDto> GetUserRoles()
        {
            var result = Query<UserRoleDto>($@"SELECT * FROM [dbo].[UserRole]");
            return result;
        }

        public UserDto GetByUserId(string userId)
        {
            var result = First<UserDto>($@"SELECT * FROM [dbo].[User] WHERE [UserId] = '{userId}'");
            return result;
        }

        public int Add(UserDto user)
        {
            var result = QuerySingle($@"INSERT INTO [dbo].[User](UserId, Password, UserRoleId, FirstName, LastName, PhoneNumber, Email, LastLoginDateTime, IsDisabled) OUTPUT INSERTED.Id VALUES (@UserId, @Password, @UserRoleId, @FirstName, @LastName, @PhoneNumber, @Email, @LastLoginDateTime, @IsDisabled)", user);
            return result;
        }

        public bool Update(UserDto user)
        {
            Execute($@"UPDATE [dbo].[User]
                       SET [FirstName] = @FirstName, [LastName] = @LastName, [PhoneNumber] = @PhoneNumber, [Email] = @Email
                       WHERE [UserId] = @UserId", user);
            return true;
        }
    }
}
