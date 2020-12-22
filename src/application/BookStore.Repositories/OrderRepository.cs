using System;
using System.Collections.Generic;
using System.Text;
using BookStore.Models.DtoModels;

namespace BookStore.Repositories
{
    public class OrderRepository : BaseRepository
    {
        private string GET_ORDERS_LIST = $@"SELECT [o].[Id] AS Id,
                                            	   [o].[Comment] AS Comment,
                                            	   (SELECT SUM([Count]) FROM [dbo].[OrderBook] WHERE [OrderId] = [o].[Id]) AS BooksCount,
                                            	   [o].[SummaryPrice] AS SummaryPrice,
                                            	   [o].[OrderStatusId] AS OrderStatusId,
                                            	   CONCAT([ub].[FirstName], [ub].[LastName]) AS BuyerName,
                                                   CONCAT([us].[FirstName], [us].[LastName]) AS SellerName,
                                            	   [ub].[Email] AS BuyerEmail,
                                            	   [ub].[PhoneNumber] AS BuyerPhoneNumber,
                                            	   [o].[OrderDateTime] AS OrderDateTime
                                            FROM [dbo].[Order] [o]
                                            LEFT JOIN [dbo].[User] [ub] ON [o].[BuyerId] = [ub].[Id]
                                            LEFT JOIN [dbo].[User] [us] ON [o].[SellerId] = [us].[Id]";

        private string GET_SIMPLE_ORDER = $@"SELECT TOP 1 * FROM [dbo].[Order] WHERE [Id] = @Id";

        private string INSERT_ORDER = $@"INSERT INTO [dbo].[Order] ([Comment],[BuyerId],[SellerId],[OrderDateTime],[SummaryPrice],[OrderStatusId])
                                         OUTPUT INSERTED.Id
                                         VALUES (@Comment, @BuyerId, @SellerId, @OrderDateTime, @SummaryPrice, @OrderStatusId)";

        private string UPDATE_ORDER = $@"UPDATE [dbo].[Order] 
                                         SET [Comment] = @Comment, [BuyerId] = @BuyerId, [SellerId] = @SellerId, [OrderDateTime]= @OrderDateTime, [SummaryPrice] = @SummaryPrice, [OrderStatusId] = @OrderStatusId
                                         WHERE [Id] = @Id";

        private string INSERT_ORDER_BOOK = $@"INSERT INTO [dbo].[OrderBook] ([OrderId], [BookId], [Count]) 
                                              VALUES (@OrderId, @BookId, @Count)";

        private string GET_ORDER_STATUSES = $@"SELECT * FROM [dbo].[OrderStatus]";


        public OrderRepository(string connectionString) : base(connectionString) { }

        public List<OrderShortDto> GetAll()
        {
            var result = Query<OrderShortDto>(GET_ORDERS_LIST);
            return result;
        }

        public int Add(OrderDto orderDto)
        {
            var result = QuerySingle(INSERT_ORDER, orderDto);
            return result;
        }

        public int AddOrderBook(int orderId, int bookId, int count)
        {
            var result = QuerySingle(INSERT_ORDER_BOOK, new { OrderId = orderId, BookId = bookId, Count = count });
            return result;
        }

        public OrderDto GetById(int id)
        {
            var result = GetById<OrderDto>(GET_SIMPLE_ORDER, id);
            return result;
        }

        public OrderDto Update(OrderDto orderDto)
        {
            Execute(UPDATE_ORDER, orderDto);
            var result = GetById(orderDto.Id);
            return result;
        }

        public List<OrderStatusDto> GetStatuses()
        {
            var result = Query<OrderStatusDto>(GET_ORDER_STATUSES);
            return result;
        }

    }
}
