using System;
using System.Collections.Generic;
using System.Text;
using BookStore.Models.DtoModels;

namespace BookStore.Repositories
{
    public class OrderRepository : BaseRepository
    {
        private string GET_ORDERS_LIST = $@"SELECT [o].[Id],
                                            	   [o].[Comment],
                                            	   (SELECT SUM([Count]) FROM [dbo].[OrderBook] WHERE [OrderId] = [o].[Id]) AS Count,
                                            	   [o].[SummaryPrice],
                                            	   [o].[OrderStatusId]
                                            FROM [dbo].[Order] [o]";


        public OrderRepository(string connectionString) : base(connectionString)
        {

        }

        public List<OrderDto> GetAll()
        {
            var result = Query<OrderDto>(GET_ORDERS_LIST);
            return result;
        }

    }
}
