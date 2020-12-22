using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Models.ViewModels
{
    public class OrderShort: BaseModel
    {
        public string Comment { get; set; }
        public int BooksCount { get; set; }
        public decimal SummaryPrice { get; set; }
        public int OrderStatusId { get; set; }
        public string BuyerName { get; set; }
        public string SellerName { get; set; }
        public string BuyerEmail { get; set; }
        public string BuyerPhoneNumber { get; set; }
        public DateTime OrderDateTime { get; set; }
    }
}
