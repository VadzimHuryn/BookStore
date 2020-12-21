﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookStore.Models.ViewModels
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(256)]
        public string Name { get; set; }
        public string Description { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Invalid count")]
        public int Count { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Invalid price")]
        public decimal Price { get; set; }
    }
}