using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using BookStore.Services;
using BookStore.Models.ViewModels;

namespace BookStore.Web.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorController : ControllerBase
    {
        AuthorService _authorService;

        public AuthorController(IConfiguration configuration)
        {
            _authorService = new AuthorService(configuration);
        }

        [HttpGet]
        public IEnumerable<Author> GetAll()
        {
            return _authorService.GetAll();
        }

        [HttpGet]
        [Route("{id}")]
        public Author GetById(int id)
        {
            return _authorService.GetById(id);
        }

        [HttpPost] 
        public Author Add([FromBody]Author author)
        {
           return _authorService.Add(author);
        }

        [HttpPut]
        public Author Update([FromBody] Author author)
        {
            return _authorService.Update(author);
        }

        [HttpDelete]
        [Route("{id}")]
        public void Delete(int id)
        {
            _authorService.Delete(id);
        }
    }
}
