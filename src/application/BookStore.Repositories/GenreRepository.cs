using BookStore.Models.DtoModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Repositories
{
    public class GenreRepository: BaseRepository
    {
        public GenreRepository(string connectionString) : base(connectionString)
        {

        }

        public List<GenreDto> GetAll()
        {
            var result = Query<GenreDto>("SELECT * FROM [dbo].[Genre] WHERE [IsDisabled] = 0");
            return result;
        }

        public GenreDto GetById(int id)
        {
            var result = First<GenreDto>($"SELECT TOP 1 * FROM [dbo].[Genre] WHERE [Id] = {id}");
            return result;
        }

        public List<BookGenreDto> GetAllBookGenres()
        {
            var result = Query<BookGenreDto>(@"SELECT * FROM [dbo].[BookGenre]");
            return result;
        }

        public List<BookGenreDto> GetBookGenresByBookId(int id)
        {
            var result = Query<BookGenreDto>($"SELECT * FROM [dbo].[BookGenre] WHERE [BookId] = {id}");
            return result;
        }


        public GenreDto Add(GenreDto genreDto)
        {
            var id = QuerySingle("INSERT INTO [dbo].[Genre]([Name]) OUTPUT INSERTED.Id VALUES (@Name)", genreDto);
            return GetById(id);
        }

        public int AddBookGenre(BookGenreDto genreDto)
        {
            var id = Execute("INSERT INTO [dbo].[BookGenre]([BookId],[GenreId]) OUTPUT INSERTED.GenreId VALUES (@BookId, @GenreId)", genreDto);
            return id;
        }

        public GenreDto Update(GenreDto genreDto)
        {
            Execute("UPDATE [dbo].[Genre] SET [Name] = @Name WHERE Id = @Id", genreDto);
            return GetById(genreDto.Id);
        }

        public void Delete(int id)
        {
            Execute("DELETE FROM [dbo].[Genre] WHERE Id = @Id", new { Id = id });
        }

        public void DeleteBookGenreByBookId(int id)
        {
            Execute("DELETE FROM [dbo].[BookGenre] WHERE BookId = @Id", new { Id = id });
        }

        public void DeleteBookGenreByGenreId(int id)
        {
            Execute("DELETE FROM [dbo].[BookGenre] WHERE GenreId = @Id", new { Id = id });
        }
    }
}
