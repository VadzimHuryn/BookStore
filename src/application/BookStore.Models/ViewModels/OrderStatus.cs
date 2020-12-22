using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Models.ViewModels
{
    public class OrderStatus: BaseModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
    }
}
