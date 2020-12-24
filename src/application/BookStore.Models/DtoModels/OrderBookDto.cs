using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Models.DtoModels
{
    public class OrderBookDto:BaseModel
    {
        public int OrderId { get; set; }
        public int BookId { get; set; }
        public int Count { get; set; }
    }
}
