using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Models.ViewModels
{
    public class Good: BaseModel
    {
        public int BookId { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
    }
}
