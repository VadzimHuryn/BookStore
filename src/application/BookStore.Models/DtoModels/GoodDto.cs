using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Models.DtoModels
{
    public class GoodDto: BaseModel
    {
        public int BookId { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
    }
}
