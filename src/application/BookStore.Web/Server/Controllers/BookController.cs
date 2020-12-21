using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using BookStore.Services;
using BookStore.Models.ViewModels;

namespace BookStore.Web.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        BookService _bookService;

        public BookController(IConfiguration configuration)
        {
            _bookService = new BookService(configuration);
        }

        [HttpGet]
        public IEnumerable<Book> GetAll()
        {
            return _bookService.GetAll();
        }

        [HttpGet]
        [Route("{id}")]
        public Book GetById(int id)
        {
            return _bookService.GetById(id);
        }

        [HttpDelete]
        [Route("{id}")]
        public bool Delete(int id)
        {
            return _bookService.Delete(id);
        }

        [HttpPost]
        public bool Add([FromBody] Book book)
        {
            return _bookService.Add(book);
        }

        [HttpPut]
        public Book Update([FromBody] Book book)
        {
            return _bookService.Update(book);
        }
    }
}
