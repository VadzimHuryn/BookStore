using System;
using System.Collections.Generic;
using System.Text;
using BookStore.Models.DtoModels;

namespace BookStore.Repositories
{
    public class BookRepository : BaseRepository
    {
        private string GET_ALL_BOOKS = $@"SELECT [b].[Id],
                                          	     [b].[Name],
                                          	     [b].[Description],
                                          	     [b].[ReleaseDate],
                                          	     [g].[Count],
                                          	     [g].[Price]
                                          FROM [dbo].[Book] [b]
                                          JOIN [dbo].[Good] [g] ON [b].[Id] = [g].[BookId]";


        public BookRepository(string connectionString) : base(connectionString)
        {

        }

        public List<BookDto> GetAll()
        {
            var result = Query<BookDto>(GET_ALL_BOOKS);
            return result;
        }

        public List<BookOutputDto> GetAllAvailable()
        {
            var result = Query<BookOutputDto>(GET_ALL_BOOKS + " WHERE [b].[IsDisabled] = 0");
            return result;
        }

        public BookOutputDto GetById(int id)
        {
            var result = First<BookOutputDto>(GET_ALL_BOOKS + $" WHERE [b].[IsDisabled] = 0 AND [b].[Id] = {id}");
            return result;
        }

        public int Add(BookDto book)
        {
            var result = QuerySingle($@"INSERT INTO [dbo].[Book]([Name], [Description], [ReleaseDate]) OUTPUT INSERTED.Id VALUES (@Name, @Description, @ReleaseDate)", book);
            return result;
        }

        public BookOutputDto Update(BookDto book)
        {
            Execute($@"UPDATE [dbo].[Book] SET [Name] = @Name, [Description] = @Description, [ReleaseDate] = @Date WHERE [Id] = @Id", book);
            var result = GetById(book.Id);
            return result;
        }

        public bool Delete(int id)
        {
            Execute($@"UPDATE [dbo].[Book] SET [IsDisabled] = 1 WHERE [Id] = @Id", new { Id = id });
            return true;
        }
    }
}
