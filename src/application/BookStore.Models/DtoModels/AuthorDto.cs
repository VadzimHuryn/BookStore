using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Models.DtoModels
{
    public class AuthorDto: BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsDisabled { get; set; }
    }
}
