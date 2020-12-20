using System;
using System.Collections.Generic;
using System.Text;
using BookStore.Models.DtoModels;

namespace BookStore.Repositories
{
    public class BookRepository : BaseRepository
    {
        public BookRepository(string connectionString) : base(connectionString)
        {

        }

        public List<BookDto> GetAll()
        {
            var result = Query<BookDto>($@"SELECT [Name], [Description], [ReleaseDate], [IsDisabled] FROM [dbo].[Book]");
            return result;
        }

        public List<BookDto> GetAllAvailable()
        {
            var result = Query<BookDto>($@"SELECT [Name], [Description], [ReleaseDate], [IsDisabled] FROM [dbo].[Book] WHERE [IsDisabled] = 0");
            return result;
        }

        public BookDto GetById(int id)
        {
            var result = First<BookDto>($@"SELECT [Name], [Description], [ReleaseDate], [IsDisabled] FROM [dbo].[Book] WHERE [Id] = {id}");
            return result;
        }

        public int Add(BookDto book)
        {
            var result = Execute($@"INSERT INTO [dbo].[Book]([Name], [Description], [ReleaseDate]) OUTPUT INSERTED.Id VALUES (@Name, @Description, @ReleaseDate)", book);
            return result;
        }

        public BookDto Update(BookDto book)
        {
            Execute($@"UPDATE [dbo].[Book] SET [Name] = @Name, [Description] = @Description, [ReleaseDate] = @Date WHERE [Id] = @Id", book);
            var result = GetById(book.Id);
            return result;
        }

        public bool Delete(int id)
        {
            Execute($@"UPDATE [dbo].[Book] SET [IsDeleted] = 1 WHERE [Id] = @Id", new { Id = id });
            return true;
        }
    }
}
