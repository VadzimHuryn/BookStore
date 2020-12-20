﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.ViewModels
{
    public class User
    {
        [Required]
        [MinLength(3)]
        [MaxLength(256)]
        public string UserId { get; set; }
        [Required]
        [MinLength(1)]
        [MaxLength(256)]
        public string FirstName { get; set; }
        [Required]
        [MinLength(1)]
        [MaxLength(256)]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [MinLength(8)]
        [MaxLength(256)]
        public string Password { get; set; }

        []
        public int UserRoleId { get; set; }
    }
}
