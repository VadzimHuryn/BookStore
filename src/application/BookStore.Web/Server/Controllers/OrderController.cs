using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using BookStore.Services;
using BookStore.Models.ViewModels;

namespace BookStore.Web.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        OrderService _orderService;

        public OrderController(IConfiguration configuration)
        {
            _orderService = new OrderService(configuration);
        }

        [HttpGet]
        public IEnumerable<OrderShort> GetAll()
        {
            return _orderService.GetAll();
        }

        [HttpGet]
        [Route("Statuses")]
        public IEnumerable<OrderStatus> GetStatuses()
        {
            return _orderService.GetStatuses();
        }

        [HttpGet]
        [Route("{id}")]
        public Order GetOrderById(int id)
        {
            return _orderService.GetOrderById(id);
        }

        [HttpDelete]
        [Route("{id}")]
        public void Delete(int id)
        {
            _orderService.Delete(id);
        }

        [HttpPost]
        public int Add([FromBody] Order order)
        {
            return _orderService.Add(order);
        }
    }
}
