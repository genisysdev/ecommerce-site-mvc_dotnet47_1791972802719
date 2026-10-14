-- 03_seed_data.sql
USE LegacyEcommerceDb;
GO

SET IDENTITY_INSERT dbo.Category ON;
INSERT INTO dbo.Category (Id, Name, ParentId, DisplayOrder) VALUES
 (1, 'Electronics', NULL, 1),
 (2, 'Laptops',      1,   1),
 (3, 'Phones',       1,   2),
 (4, 'Accessories',  NULL,2);
SET IDENTITY_INSERT dbo.Category OFF;
GO

SET IDENTITY_INSERT dbo.Product ON;
INSERT INTO dbo.Product (Id, Name, Description, Price, ThumbnailUrl, CategoryId, IsActive) VALUES
 (1, 'UltraBook 14',   '14-inch lightweight laptop',        1299.00, '/Content/img/ultrabook.png', 2, 1),
 (2, 'PowerBook 16',   '16-inch performance laptop',        2199.00, '/Content/img/powerbook.png', 2, 1),
 (3, 'Galaxy Phone X', '6.5-inch flagship phone',            899.00, '/Content/img/phonex.png',    3, 1),
 (4, 'Mini Phone',     'Compact 5-inch phone',               499.00, '/Content/img/miniphone.png', 3, 1),
 (5, 'USB-C Charger',  '65W fast charger',                    49.00, '/Content/img/charger.png',    4, 1),
 (6, 'Laptop Sleeve',  '14-inch protective sleeve',           29.00, '/Content/img/sleeve.png',     4, 1);
SET IDENTITY_INSERT dbo.Product OFF;
GO

INSERT INTO dbo.ProductImage (ProductId, Url, SortOrder) VALUES
 (1, '/Content/img/ultrabook.png', 0),
 (1, '/Content/img/ultrabook-2.png', 1),
 (3, '/Content/img/phonex.png', 0),
 (3, '/Content/img/phonex-2.png', 1);
GO

INSERT INTO dbo.ProductVariant (ProductId, Name) VALUES
 (1, '8GB / 256GB'), (1, '16GB / 512GB'),
 (3, 'Black'), (3, 'Silver');
GO
