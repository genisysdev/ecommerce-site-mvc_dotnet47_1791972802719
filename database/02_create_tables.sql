-- 02_create_tables.sql
USE LegacyEcommerceDb;
GO

IF OBJECT_ID('dbo.ProductVariant','U') IS NOT NULL DROP TABLE dbo.ProductVariant;
IF OBJECT_ID('dbo.ProductImage','U')   IS NOT NULL DROP TABLE dbo.ProductImage;
IF OBJECT_ID('dbo.CartItem','U')       IS NOT NULL DROP TABLE dbo.CartItem;
IF OBJECT_ID('dbo.OrderLine','U')      IS NOT NULL DROP TABLE dbo.OrderLine;
IF OBJECT_ID('dbo.[Order]','U')        IS NOT NULL DROP TABLE dbo.[Order];
IF OBJECT_ID('dbo.Product','U')        IS NOT NULL DROP TABLE dbo.Product;
IF OBJECT_ID('dbo.Category','U')       IS NOT NULL DROP TABLE dbo.Category;
IF OBJECT_ID('dbo.Customer','U')       IS NOT NULL DROP TABLE dbo.Customer;
GO

CREATE TABLE dbo.Category (
    Id            INT IDENTITY(1,1) PRIMARY KEY,
    Name          NVARCHAR(100)  NOT NULL,
    ParentId      INT            NULL REFERENCES dbo.Category(Id),
    DisplayOrder  INT            NOT NULL DEFAULT(0)
);
GO

CREATE TABLE dbo.Product (
    Id            INT IDENTITY(1,1) PRIMARY KEY,
    Name          NVARCHAR(200)  NOT NULL,
    Description   NVARCHAR(MAX)  NULL,
    Price         DECIMAL(18,2)  NOT NULL,
    ThumbnailUrl  NVARCHAR(400)  NULL,
    CategoryId    INT            NOT NULL REFERENCES dbo.Category(Id),
    IsActive      BIT            NOT NULL DEFAULT(1),
    CreatedUtc    DATETIME       NOT NULL DEFAULT(GETUTCDATE())
);
GO

CREATE TABLE dbo.ProductImage (
    Id         INT IDENTITY(1,1) PRIMARY KEY,
    ProductId  INT           NOT NULL REFERENCES dbo.Product(Id),
    Url        NVARCHAR(400) NOT NULL,
    SortOrder  INT           NOT NULL DEFAULT(0)
);
GO

CREATE TABLE dbo.ProductVariant (
    Id         INT IDENTITY(1,1) PRIMARY KEY,
    ProductId  INT           NOT NULL REFERENCES dbo.Product(Id),
    Name       NVARCHAR(100) NOT NULL
);
GO

-- Stubbed schema so the EDMX model is complete for later slices.
CREATE TABLE dbo.Customer (
    Id     INT IDENTITY(1,1) PRIMARY KEY,
    Email  NVARCHAR(256) NOT NULL,
    Name   NVARCHAR(200) NULL
);
GO

CREATE TABLE dbo.[Order] (
    Id          INT IDENTITY(1,1) PRIMARY KEY,
    CustomerId  INT NOT NULL REFERENCES dbo.Customer(Id),
    CreatedUtc  DATETIME NOT NULL DEFAULT(GETUTCDATE()),
    Total       DECIMAL(18,2) NOT NULL DEFAULT(0)
);
GO

CREATE TABLE dbo.CartItem (
    Id         INT IDENTITY(1,1) PRIMARY KEY,
    ProductId  INT NOT NULL REFERENCES dbo.Product(Id),
    Quantity   INT NOT NULL DEFAULT(1)
);
GO

CREATE INDEX IX_Product_CategoryId ON dbo.Product(CategoryId);
CREATE INDEX IX_Category_ParentId  ON dbo.Category(ParentId);
GO
