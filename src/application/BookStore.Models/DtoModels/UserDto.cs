using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.DtoModels
{
    public class UserDto: BaseModel
    {
        public string UserId { get; set; }
        public string Password { get; set; }
        public int UserRoleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime LastLoginDateTime { get; set; }
        public bool IsDisabled { get; set; }
    }
}
