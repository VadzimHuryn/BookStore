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

            foreach (var bookDto in bookDtos)
            {
                result.Add(new Book()
                {
                    Id = bookDto.Id,
                    Name = bookDto.Name,
                    Description = bookDto.Description,
                    ReleaseDate = bookDto.ReleaseDate,
                    Price = bookDto.Price,
                    Count = bookDto.Count,
                    Image = bookDto.Image
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
                Image = bookDto.Image,
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
                ReleaseDate = book.ReleaseDate,
                Image = book.Image
            });

            var isBookAdded = _goodRepository.AddGood(new GoodDto()
            {
                BookId = bookId,
                Count = book.Count,
                Price = book.Price
            });

            return isBookAdded;
        }

        public Book Update(Book book)
        {
            var bookDto = new BookDto()
            {
                Id = book.Id,
                Name = book.Name,
                Description = book.Description,
                ReleaseDate = book.ReleaseDate,
                Image = book.Image
            };

            var goodDto = new GoodDto()
            {
                BookId = book.Id,
                Count = book.Count,
                Price = book.Price
            };

            _goodRepository.UpdateGood(goodDto);
            var bookResult = _bookRepository.Update(bookDto);
            
            return new Book() 
            {
                Id = bookResult.Id,
                Name = bookResult.Name,
                Description = bookResult.Description,
                ReleaseDate = bookResult.ReleaseDate,
                Count = bookResult.Count,
                Price = bookResult.Price,
                Image = bookResult.Image
            };
        }


        public bool Delete(int id)
            {
                var result = _bookRepository.Delete(id);
                return result;
            }
        }
    }
