using BookStore.Repositories;
using System.Collections.Generic;
using BookStore.Models.DtoModels;
using BookStore.Models.ViewModels;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace BookStore.Services
{
    public class OrderService
    {
        BookRepository _bookRepository;
        GoodRepository _goodRepository;
        OrderRepository _orderRepository;

        public OrderService(IConfiguration configuration)
        {
            _bookRepository = new BookRepository(configuration.GetConnectionString("DefaultConnection"));
            _goodRepository = new GoodRepository(configuration.GetConnectionString("DefaultConnection"));
            _orderRepository = new OrderRepository(configuration.GetConnectionString("DefaultConnection"));
        }

        public List<OrderShort> GetAll()
        {
            var result = new List<OrderShort>();

            var orderShortDtos = _orderRepository.GetAll();

            foreach(var orderShortDto in orderShortDtos)
            {
                result.Add(new OrderShort()
                {
                    Id = orderShortDto.Id,
                    Comment = orderShortDto.Comment,
                    BooksCount = orderShortDto.BooksCount,
                    BuyerEmail = orderShortDto.BuyerEmail,
                    BuyerName = orderShortDto.BuyerName,
                    SellerName = orderShortDto.SellerName,
                    BuyerPhoneNumber = orderShortDto.BuyerPhoneNumber,
                    OrderDateTime = orderShortDto.OrderDateTime,
                    OrderStatusId = orderShortDto.OrderStatusId,
                    SummaryPrice = orderShortDto.OrderStatusId
                });
            }

            return result;
        }

        public int Add(Order order)
        {
            var books = _bookRepository.GetAllAvailable();

            var orderBooks = books.Where(x => order.OrderBooks.Any(y => y.BookId == x.Id)).ToList();

            var orderDto = new OrderDto()
            {
                BuyerId = order.BuyerId,
                Comment = order.Comment,
                OrderDateTime = order.OrderDateTime,
                OrderStatusId = order.OrderStatusId,
                SellerId = order.SellerId,
                SummaryPrice = orderBooks.Sum(x => x.Price)
            };

            var orderId = _orderRepository.Add(orderDto);

            foreach(var orderBook in order.OrderBooks)
            {
                _orderRepository.AddOrderBook(orderId, orderBook.BookId, orderBook.Count);
            }

            return orderId;
        }

        public List<OrderStatus> GetStatuses()
        {
            var result = new List<OrderStatus>();

            var statusDtos = _orderRepository.GetStatuses();

            foreach(var statusDto in statusDtos)
            {
                result.Add(new OrderStatus()
                {
                    Id = statusDto.Id,
                    Code = statusDto.Code,
                    Name = statusDto.Name,
                    Comment = statusDto.Comment
                });
            }

            return result;
        }
    }
}