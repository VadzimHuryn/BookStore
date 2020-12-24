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
                    SummaryPrice = orderShortDto.SummaryPrice
                });
            }

            return result;
        }

        public Order GetOrderById(int id)
        {
            var orderDto = _orderRepository.GetById(id);
            var orderBooks = _orderRepository.GetOrderBooksByOrderId(orderDto.Id);

            return new Order()
            {
                Id = orderDto.Id,
                BuyerId = orderDto.BuyerId,
                SellerId = orderDto.SellerId,
                Comment = orderDto.Comment,
                OrderDateTime = orderDto.OrderDateTime,
                OrderStatusId = orderDto.OrderStatusId,
                OrderBooks = orderBooks.Select(x => new OrderBook()
                {
                    BookId = x.BookId,
                    Count = x.Count
                }).ToList(),
            };
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
                SummaryPrice = orderBooks.Sum(x => x.Price * order.OrderBooks.FirstOrDefault(y => y.BookId == x.Id).Count)
            };

            var orderId = _orderRepository.Add(orderDto);

            foreach(var orderBook in order.OrderBooks)
            {
                _orderRepository.AddOrderBook(orderId, orderBook.BookId, orderBook.Count);

                var goodDto = _goodRepository.GetGoodByBookId(orderBook.BookId);
                goodDto.Count = goodDto.Count - orderBook.Count;

                _goodRepository.UpdateGood(goodDto);
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

        public void Delete(int id)
        {
            var statuses = _orderRepository.GetStatuses();
            var order = _orderRepository.GetById(id);
            
            if(order.OrderStatusId != statuses.FirstOrDefault(x => x.Code == "Paid").Id)
            {
                var orderBook = _orderRepository.GetOrderBooksByOrderId(order.Id);
                var books = _bookRepository.GetAll().Where(x => orderBook.Any(y => y.BookId == x.Id)).ToList();

                foreach(var book in books)
                {
                    var good = _goodRepository.GetGoodByBookId(book.Id);
                    good.Count += orderBook.FirstOrDefault(x => x.BookId == good.BookId).Count;
                    _goodRepository.UpdateGood(good);
                }
            }

            _orderRepository.DeleteOrderBook(id);
            _orderRepository.Delete(id);
        }
    }
}