using BookStore.Repositories;
using System.Collections.Generic;
using BookStore.Models.DtoModels;
using BookStore.Models.ViewModels;
using Microsoft.Extensions.Configuration;
using System;

namespace BookStore.Services
{
    public class AuthorService
    {
        private AuthorRepository _authorRepository;

        public AuthorService(IConfiguration configuration) 
        {
            _authorRepository = new AuthorRepository(configuration.GetConnectionString("DefaultConnection"));
        }

        public List<Author> GetAll()
        {
            var result = new List<Author>();

            var authorDtos = _authorRepository.GetAll();
            
            foreach(var authorDto in authorDtos)
            {
                result.Add(new Author()
                {
                    Id = authorDto.Id,
                    FirstName = authorDto.FirstName,
                    LastName = authorDto.LastName
                });
            }

            return result;
        }

        public Author GetById(int id)
        {
            var authorDto = _authorRepository.GetById(id);

            return new Author()
            {
                Id = authorDto.Id,
                FirstName = authorDto.FirstName,
                LastName = authorDto.LastName
            };
        }

        public Author Add(Author author)
        {
            var result = _authorRepository.Add(new AuthorDto()
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName
            });

            return new Author()
            {
                Id = result.Id,
                FirstName = result.FirstName,
                LastName = result.LastName
            };
        }

        public Author Update(Author author)
        {
            var result = _authorRepository.Update(new AuthorDto()
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName
            });

            return new Author()
            {
                Id = result.Id,
                FirstName = result.FirstName,
                LastName = result.LastName
            };
        }

        public void Delete(int id)
        {
            _authorRepository.DeleteBookAuthorByAuthorId(id);
            _authorRepository.Delete(id);
        }
    }
}
