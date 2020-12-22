using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models.ViewModels
{
    public class Order : BaseModel
    {
        public string Comment { get; set; }
        public int? BuyerId { get; set; }
        public int? SellerId { get; set; }
        public DateTime OrderDateTime { get; set; }
        public List<OrderBook> OrderBooks { get; set; }
        public int OrderStatusId { get; set; }

        public Order()
        {
            OrderBooks = new List<OrderBook>();
        }
    }

    public class OrderBook
    {
        public int BookId { get; set; }
        public int Count { get; set; }
    }
}
