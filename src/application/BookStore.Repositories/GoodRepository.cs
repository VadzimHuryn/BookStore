using System;
using System.Collections.Generic;
using System.Text;
using BookStore.Models.DtoModels;

namespace BookStore.Repositories
{
    public class GoodRepository : BaseRepository
    {
        public GoodRepository(string connectionString) : base(connectionString)
        {

        }

        public GoodDto GetGoodByBookId(int bookId)
        {
            var result = First<GoodDto>($"SELECT TOP 1 * FROM [dbo].[Good] WHERE [BookId] = {bookId}");
            return result;
        }

        public GoodDto UpdateGood(GoodDto goodDto)
        {
            Execute($"UPDATE [dbo].[Good] SET [Price] = @Price, [Count] = @Count WHERE [BookId] = @BookId", goodDto);
            var result = GetGoodByBookId(goodDto.BookId);
            return result;
        }

        public bool AddGood(GoodDto goodDto)
        {
            Execute($"INSERT INTO [dbo].[Good] ([BookId], [Count], [Price]) VALUES (@BookId, @Count, @Price)", goodDto);
            return true;
        }
    }
}
