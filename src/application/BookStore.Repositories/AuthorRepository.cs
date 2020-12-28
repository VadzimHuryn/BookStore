using BookStore.Models.DtoModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Repositories
{
    public class AuthorRepository: BaseRepository
    {
        public AuthorRepository(string connectionString) : base(connectionString)
        {

        }

        public List<AuthorDto> GetAll()
        {
            var result = Query<AuthorDto>("SELECT * FROM [dbo].[Author] WHERE [IsDisabled] = 0");
            return result;
        }

        public List<BookAuthorDto> GetAllBookAuthors()
        {
            var result = Query<BookAuthorDto>("SELECT * FROM [dbo].[BookAuthor]");
            return result;
        }

        public List<BookAuthorDto> GetAllBookAuthorsByBookId(int id)
        {
            var result = Query<BookAuthorDto>($"SELECT * FROM [dbo].[BookAuthor] WHERE [BookId] = {id}");
            return result;
        }

        public AuthorDto GetById(int id)
        {
            var result = First<AuthorDto>($"SELECT TOP 1 * FROM [dbo].[Author] WHERE [Id] = {id}");
            return result;
        }

        public AuthorDto Add(AuthorDto authorDto)
        {
            var id = QuerySingle("INSERT INTO [dbo].[Author]([FirstName], [LastName]) OUTPUT INSERTED.Id VALUES (@FirstName, @LastName)", authorDto);
            return GetById(id);
        }

        public int AddBookAuthor(BookAuthorDto authorDto)
        {
            var id = Execute("INSERT INTO [dbo].[BookAuthor]([BookId], [AuthorId]) OUTPUT INSERTED.AuthorId VALUES (@BookId, @AuthorId)", authorDto);
            return id;
        }

        public AuthorDto Update(AuthorDto authorDto)
        {
            Execute("UPDATE [dbo].[Author] SET [FirstName] = @FirstName, [LastName] = @LastName WHERE Id = @Id", authorDto);
            return GetById(authorDto.Id);
        }

        public void Delete(int id)
        {
            Execute("DELETE FROM [dbo].[Author] WHERE Id = @Id", new { Id = id });
        }

        public void DeleteBookAuthorByBookId(int id)
        {
            Execute("DELETE FROM [dbo].[BookAuthor] WHERE BookId = @Id", new { Id = id });
        }

        public void DeleteBookAuthorByAuthorId(int id)
        {
            Execute("DELETE FROM [dbo].[BookAuthor] WHERE AuthorId = @Id", new { Id = id });
        }
    }
}
