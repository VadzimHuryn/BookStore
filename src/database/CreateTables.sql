CREATE TABLE [Book]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[Name] NVARCHAR(256) NOT NULL,
	[Description] NVARCHAR(MAX) NULL,
	[ReleaseDate] DATETIME NULL,
	[Image] NVARCHAR(MAX) NULL,
	[IsDisabled] BIT NOT NULL DEFAULT (0)
)
GO

CREATE TABLE [Genre]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[Name] NVARCHAR(256) NOT NULL,
	[IsDisabled] BIT NOT NULL DEFAULT (0)
)
GO

CREATE TABLE [Author]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[FirstName] NVARCHAR(256) NOT NULL,
	[LastName] NVARCHAR(256) NOT NULL,
	[IsDisabled] BIT NOT NULL DEFAULT (0)
)
GO

CREATE TABLE [BookGenre]
(
	[BookId] INT NOT NULL,
	[GenreId] INT NOT NULL
)
GO

CREATE TABLE [BookAuthor]
(
	[BookId] INT NOT NULL,
	[AuthorId] INT NOT NULL
)
GO

CREATE TABLE [Good]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[BookId] INT NOT NULL,
	[Count] INT NOT NULL DEFAULT(0),
	[Price] DECIMAL(16, 2) NOT NULL DEFAULT(0)
)
GO

CREATE TABLE [Order]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[Comment] NVARCHAR(MAX),
	[BuyerId] INT NOT NULL,
	[SellerId] INT NOT NULL,
	[OrderDateTime] DATETIME NOT NULL DEFAULT(GETDATE()),
	[SummaryPrice] DECIMAL(16,2) NOT NULL,
	[OrderStatusId] INT NOT NULL,
)
GO

CREATE TABLE [OrderBook]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[OrderId] INT NOT NULL,
	[BookId] INT NOT NULL,
	[Count] INT NOT NULL
)
GO

CREATE TABLE [OrderStatus]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[Code] NVARCHAR(256) NOT NULL,
	[Name] NVARCHAR(256) NOT NULL,
	[Comment] NVARCHAR(256) NOT NULL
)
GO

CREATE TABLE [UserRole]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[Code] NVARCHAR(256) NOT NULL,
	[Name] NVARCHAR(256) NOT NULL,
	[Comment] NVARCHAR(256) NOT NULL
)
GO

CREATE TABLE [User]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[UserId] NVARCHAR(128) NOT NULL,
	[UserRoleId] INT NOT NULL,
	[FirstName] NVARCHAR(256) NOT NULL,
	[LastName] NVARCHAR(256) NOT NULL,
	[PhoneNumber] NVARCHAR(256) NOT NULL,
	[Password] NVARCHAR(MAX) NOT NULL,
	[Email] NVARCHAR(256) NOT NULL,
	[LastLoginDateTime] DATETIME NULL,
	[IsDisabled] BIT NOT NULL DEFAULT (0)
)
GO

--BOOK GENRE

ALTER TABLE [BookGenre]
ADD CONSTRAINT [PK_BookGenre_BookId_GenreId] PRIMARY KEY ([BookId], [GenreId])
GO

ALTER TABLE [BookGenre]
ADD CONSTRAINT [FK_BookGenre_BookId_Book_Id] FOREIGN KEY ([BookId]) REFERENCES [Book]([Id])
GO

ALTER TABLE [BookGenre]
ADD CONSTRAINT [FK_BookGenre_GenreId_Genre_Id] FOREIGN KEY ([GenreId]) REFERENCES [Genre]([Id])
GO

--BOOK AUTHOR

ALTER TABLE [BookAuthor]
ADD CONSTRAINT [PK_BookAuthor_BookId_AuthorId] PRIMARY KEY ([BookId], [AuthorId])
GO

ALTER TABLE [BookAuthor]
ADD CONSTRAINT [FK_BookAuthor_BookId_Book_Id] FOREIGN KEY ([BookId]) REFERENCES [Book]([Id])
GO

ALTER TABLE [BookAuthor]
ADD CONSTRAINT [FK_BookAuthor_AuthorId_Author_Id] FOREIGN KEY ([AuthorId]) REFERENCES [Author]([Id])
GO

-- GOODS

ALTER TABLE [Good]
ADD CONSTRAINT [FK_Good_BookId_Book_Id] FOREIGN KEY ([BookId]) REFERENCES [Book]([Id])
GO

--ORDER

ALTER TABLE [Order]
ADD CONSTRAINT [FK_Order_SellerId_User_Id] FOREIGN KEY ([SellerId]) REFERENCES [User]([Id])
GO

ALTER TABLE [Order]
ADD CONSTRAINT [FK_Order_BuyerId_User_Id] FOREIGN KEY ([BuyerId]) REFERENCES [User]([Id])
GO

ALTER TABLE [Order]
ADD CONSTRAINT [FK_Order_OrderStatusId_OrderStatus_Id] FOREIGN KEY ([OrderStatusId]) REFERENCES [OrderStatus]([Id])
GO

--ORDER BOOK

ALTER TABLE [OrderBook]
ADD CONSTRAINT [FK_OrderBook_OrderId_Order_Id] FOREIGN KEY ([OrderId]) REFERENCES [Order]([Id])
GO

ALTER TABLE [OrderBook]
ADD CONSTRAINT [FK_OrderBook_BookId_Book_Id] FOREIGN KEY ([BookId]) REFERENCES [Book]([Id])
GO

-- USER

ALTER TABLE [User]
ADD CONSTRAINT [FK_User_UserRoleId_UserRole_Id] FOREIGN KEY ([UserRoleId]) REFERENCES [UserRole]([Id])
GO

INSERT INTO [UserRole] ([Code], [Name], [Comment])
VALUES ('Seller', 'Seller', 'Seller'),
	   ('Buyer', 'Buyer', 'Buyer')

INSERT INTO [OrderStatus] ([Code], [Name], [Comment])
VALUES ('New', 'New', 'New'),
	   ('Processed', 'Processed', 'Processed'),
	   ('Paid', 'Paid', 'Paid'),
	   ('Cancelled', 'Cancelled', 'Cancelled')

INSERT INTO [dbo].[User]
           ([UserId]
           ,[UserRoleId]
           ,[FirstName]
           ,[LastName]
           ,[PhoneNumber]
           ,[Password]
           ,[Email]
           ,[LastLoginDateTime]
           ,[IsDisabled])
     VALUES
           ('admin'
           ,'1'
           ,'admin'
           ,'admin'
           ,'6666666'
           ,'admin'
           ,'admin@admin.com'
           ,GETDATE()
           ,0)

