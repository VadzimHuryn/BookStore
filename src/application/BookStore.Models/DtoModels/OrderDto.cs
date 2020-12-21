using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Models.DtoModels
{
    public class OrderDto: BaseModel
    {
        public string Comment { get; set; }
        public int BooksCount { get; set; }
        public decimal SummaryPrice { get; set; }
        public int OrderStatusId { get; set; }
    }
}
