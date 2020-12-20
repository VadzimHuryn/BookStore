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
    }
}
