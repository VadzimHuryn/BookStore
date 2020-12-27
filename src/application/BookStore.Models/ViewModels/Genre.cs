using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookStore.Models.ViewModels
{
    public class Genre: BaseModel
    {
        [Required]
        [MaxLength(256)]
        public string Name { get; set; }
    }
}
