using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using BookStore.Services;
using BookStore.Models.ViewModels;

namespace BookStore.Web.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GenreController : ControllerBase
    {
        GenreService _genreService;

        public GenreController(IConfiguration configuration)
        {
            _genreService = new GenreService(configuration);
        }

        [HttpGet]
        public IEnumerable<Genre> GetAll()
        {
            return _genreService.GetAll();
        }

        [HttpGet]
        [Route("{id}")]
        public Genre GetById(int id)
        {
            return _genreService.GetById(id);
        }

        [HttpPost] 
        public Genre Add([FromBody]Genre genre)
        {
           return _genreService.Add(genre);
        }

        [HttpPut]
        public Genre Update([FromBody] Genre genre)
        {
            return _genreService.Update(genre);
        }

        [HttpDelete]
        [Route("{id}")]
        public void Delete(int id)
        {
            _genreService.Delete(id);
        }
    }
}
