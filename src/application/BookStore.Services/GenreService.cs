using BookStore.Repositories;
using System.Collections.Generic;
using BookStore.Models.DtoModels;
using BookStore.Models.ViewModels;
using Microsoft.Extensions.Configuration;
using System;

namespace BookStore.Services
{
    public class GenreService
    {
        private GenreRepository _genreRepository;

        public GenreService(IConfiguration configuration) 
        {
            _genreRepository = new GenreRepository(configuration.GetConnectionString("DefaultConnection"));
        }

        public List<Genre> GetAll()
        {
            var result = new List<Genre>();

            var genreDtos = _genreRepository.GetAll();
            
            foreach(var genreDto in genreDtos)
            {
                result.Add(new Genre()
                {
                    Id = genreDto.Id,
                    Name = genreDto.Name
                });
            }

            return result;
        }

        public Genre GetById(int id)
        {
            var genreDto = _genreRepository.GetById(id);

            return new Genre()
            {
                Id = genreDto.Id,
                Name = genreDto.Name
            };
        }

        public Genre Add(Genre genre)
        {
            var result = _genreRepository.Add(new GenreDto()
            {
                Id = genre.Id,
                Name = genre.Name
            });

            return new Genre()
            {
                Id = result.Id,
                Name = result.Name
            };
        }

        public Genre Update(Genre genre)
        {
            var result = _genreRepository.Update(new GenreDto()
            {
                Id = genre.Id,
                Name = genre.Name
            });

            return new Genre()
            {
                Id = result.Id,
                Name = result.Name
            };
        }

        public void Delete(int id)
        {
            _genreRepository.DeleteBookGenreByGenreId(id);
            _genreRepository.Delete(id);
        }
    }
}
