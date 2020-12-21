using BookStore.Repositories;
using System.Collections.Generic;
using BookStore.Models.DtoModels;
using BookStore.Models.ViewModels;
using Microsoft.Extensions.Configuration;

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

        public List<Book> GetAll()
        {
            var result = new List<Book>();

            var bookDtos = _bookRepository.GetAllAvailable();

            foreach (var bookDto in bookDtos)
            {
                result.Add(new Book()
                {
                    Id = bookDto.Id,
                    Name = bookDto.Name,
                    Description = bookDto.Description,
                    ReleaseDate = bookDto.ReleaseDate,
                    Price = bookDto.Price,
                    Count = bookDto.Count
                });
            }

            return result;
        }
    }
}