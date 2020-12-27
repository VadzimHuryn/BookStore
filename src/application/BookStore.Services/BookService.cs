using BookStore.Repositories;
using System.Collections.Generic;
using BookStore.Models.DtoModels;
using BookStore.Models.ViewModels;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace BookStore.Services
{
    public class BookService
    {
        BookRepository _bookRepository;
        GoodRepository _goodRepository;
        AuthorRepository _authorRepository;
        GenreRepository _genreRepository;

        public BookService(IConfiguration configuration)
        {
            _bookRepository = new BookRepository(configuration.GetConnectionString("DefaultConnection"));
            _goodRepository = new GoodRepository(configuration.GetConnectionString("DefaultConnection"));
            _authorRepository = new AuthorRepository(configuration.GetConnectionString("DefaultConnection"));
            _genreRepository = new GenreRepository(configuration.GetConnectionString("DefaultConnection"));
        }

        public List<Book> GetAll()
        {
            var result = new List<Book>();

            var bookDtos = _bookRepository.GetAllAvailable();
            var authorDots = _authorRepository.GetAll();
            var genreDtos = _genreRepository.GetAll();
            var bookAuthorDtos = _authorRepository.GetAllBookAuthors();
            var bookGenreDtos = _genreRepository.GetAllBookGenres();

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
                    Image = bookDto.Image,
                    AuthorIds = bookAuthorDtos.Where(x => x.BookId == bookDto.Id).Select(x => x.AuthorId).ToList(),
                    GenreIds = bookGenreDtos.Where(x => x.BookId == bookDto.Id).Select(x => x.GenreId).ToList()
                });
            }

            return result;
        }

        public Book GetById(int id)
        {
            var bookDto = _bookRepository.GetById(id);
            var bookAuthorDtos = _authorRepository.GetAllBookAuthorsByBookId(bookDto.Id);
            var bookGenreDtos = _genreRepository.GetBookGenresByBookId(bookDto.Id);

            return new Book()
            {
                Id = bookDto.Id,
                Name = bookDto.Name,
                Image = bookDto.Image,
                Description = bookDto.Description,
                ReleaseDate = bookDto.ReleaseDate,
                Price = bookDto.Price,
                Count = bookDto.Count,
                AuthorIds = bookAuthorDtos.Select(x => x.AuthorId).ToList(),
                GenreIds = bookGenreDtos.Select(x => x.BookId).ToList()
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

            _authorRepository.DeleteBookAuthorByBookId(bookId);
            _genreRepository.DeleteBookGenreByBookId(bookId);

            book.AuthorIds.ForEach(authorId =>
            {
                _authorRepository.AddBookAuthor(new BookAuthorDto()
                {
                    BookId = bookId,
                    AuthorId = authorId
                });
            });

            book.GenreIds.ForEach(genreId =>
            {
                _genreRepository.AddBookGenre(new BookGenreDto()
                {
                    BookId = bookId,
                    GenreId = genreId
                });
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

            _authorRepository.DeleteBookAuthorByBookId(bookResult.Id);
            _genreRepository.DeleteBookGenreByBookId(bookResult.Id);

            book.AuthorIds.ForEach(authorId =>
            {
                _authorRepository.AddBookAuthor(new BookAuthorDto()
                {
                    BookId = bookResult.Id,
                    AuthorId = authorId
                });
            });

            book.GenreIds.ForEach(genreId =>
            {
                _genreRepository.AddBookGenre(new BookGenreDto()
                {
                    BookId = bookResult.Id,
                    GenreId = genreId
                });
            });

            return new Book()
            {
                Id = bookResult.Id,
                Name = bookResult.Name,
                Description = bookResult.Description,
                ReleaseDate = bookResult.ReleaseDate,
                Count = bookResult.Count,
                Price = bookResult.Price,
                Image = bookResult.Image,
                AuthorIds = book.AuthorIds,
                GenreIds = book.GenreIds
            };
        }

        public bool Delete(int id)
        {
            _genreRepository.DeleteBookGenreByBookId(id);
            _authorRepository.DeleteBookAuthorByBookId(id);
            var result = _bookRepository.Delete(id);
            return result;
        }
    }
}
