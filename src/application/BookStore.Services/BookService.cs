using BookStore.Repositories;
using System.Collections.Generic;
using BookStore.Models.DtoModels;
using BookStore.Models.ViewModels;
using Microsoft.Extensions.Configuration;

namespace BookStore.Services
{
    public class BookService
    {
        BookRepository _bookRepository;
        GoodRepository _goodRepository;
        public BookService(IConfiguration configuration)
        {
            _bookRepository = new BookRepository(configuration.GetConnectionString("DefaultConnection"));
            _goodRepository = new GoodRepository(configuration.GetConnectionString("DefaultConnection"));
        }

        public List<Book> GetAll()
        {
            var result = new List<Book>();

            var bookDtos = _bookRepository.GetAllAvailable();
            
            foreach(var bookDto in bookDtos)
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

        public Book GetById(int id)
        {
            var bookDto = _bookRepository.GetById(id);

            return new Book()
            {
                Id = bookDto.Id,
                Name = bookDto.Name,
                Description = bookDto.Description,
                ReleaseDate = bookDto.ReleaseDate,
                Price = bookDto.Price,
                Count = bookDto.Count
            };
        }

        public bool Add(Book book)
        {
            var bookId = _bookRepository.Add(new BookDto()
            {
                Name = book.Name,
                Description = book.Description,
                ReleaseDate = book.ReleaseDate
            });

            var isBookAdded = _goodRepository.AddGood(new GoodDto()
            {
                BookId = bookId,
                Count = book.Count,
                Price = book.Price
            });

            return isBookAdded;
        }

        public bool Delete(int id)
        {
            var result = _bookRepository.Delete(id);
            return result;
        }
    }
}
