CREATE TABLE [Book]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[Name] NVARCHAR(256) NOT NULL,
	[ReleaseDate] DATETIME,
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
	[Price] INT NOT NULL DEFAULT(0)
)
GO

CREATE TABLE [Sold]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[BookId] INT NOT NULL,
	[Count] INT NOT NULL,
	[SummaryPrice] INT NOT NULL,
	[SoldDate] DATETIME NOT NULL DEFAULT GETDATE(),
	[SellerId] INT NOT NULL,
	[BuyerId] INT,
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

--SOLD

ALTER TABLE [Sold]
ADD CONSTRAINT [FK_Sold_BookId_Book_Id] FOREIGN KEY ([BookId]) REFERENCES [Book]([Id])
GO

ALTER TABLE [Sold]
ADD CONSTRAINT [FK_Sold_SellerId_User_Id] FOREIGN KEY ([SellerId]) REFERENCES [User]([Id])
GO

ALTER TABLE [Sold]
ADD CONSTRAINT [FK_Sold_BuyerId_User_Id] FOREIGN KEY ([BuyerId]) REFERENCES [User]([Id])
GO

-- USER

ALTER TABLE [User]
ADD CONSTRAINT [FK_User_UserRoleId_UserRole_Id] FOREIGN KEY ([UserRoleId]) REFERENCES [UserRole]([Id])





