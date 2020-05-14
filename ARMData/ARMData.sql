USE tempdb;

IF(db_id('ARMData') IS NOT NULL)
EXEC('
ALTER DATABASE ARMData SET RESTRICTED_USER WITH ROLLBACK IMMEDIATE;
USE ARMData;
ALTER DATABASE ARMData SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
USE tempdb;
DROP DATABASE ARMData;
');

CREATE DATABASE ARMData;
GO
USE ARMData
GO

IF OBJECT_ID('Products') IS NOT NULL
BEGIN
	DROP TABLE Products
END
GO
CREATE TABLE Products(
	Id int NOT NULL PRIMARY KEY IDENTITY(1, 1),
	ProductName nvarchar(100) NOT NULL,
	[Description] nvarchar(MAX) NOT NULL,
	RetailPrice money NOT NULL,
	QuantityInStock int NOT NULL DEFAULT 1,
	CreatedDate datetime2 NOT NULL DEFAULT GETUTCDATE(),
	LastModified datetime2 NOT NULL DEFAULT GETUTCDATE(),
	IsTaxable bit NOT NULL DEFAULT 1
)
GO

IF OBJECT_ID('Inventories') IS NOT NULL
BEGIN
	DROP TABLE Inventories
END
GO
CREATE TABLE Inventories(
	Id int NOT NULL PRIMARY KEY IDENTITY(1, 1),
	ProductId int NOT NULL FOREIGN KEY REFERENCES Products(Id),
	Quantity int NOT NULL DEFAULT 1,
	PurchasePrice money NOT NULL,
	PurchaseDate datetime2 NOT NULL DEFAULT GETUTCDATE()	
)
GO

IF OBJECT_ID('Users') IS NOT NULL
BEGIN
	DROP TABLE Users
END
GO
CREATE TABLE Users(
	Id nvarchar(128) NOT NULL PRIMARY KEY,
	FirstName nvarchar(50) NOT NULL,
	LastName nvarchar(50) NOT NULL
)

IF OBJECT_ID('Sales') IS NOT NULL
BEGIN
	DROP TABLE Sales
END
GO
CREATE TABLE Sales(
	Id int NOT NULL PRIMARY KEY IDENTITY(1, 1),
	CashierId nvarchar(128) NOT NULL FOREIGN KEY REFERENCES Users(Id),
	SaleDate datetime2 NOT NULL,
	SubTotal money NOT NULL,
	Tax money NOT NULL,
	Total money NOT NULL
)
GO

IF OBJECT_ID('SaleDetails') IS NOT NULL
BEGIN
	DROP TABLE SaleDetails
END
GO
CREATE TABLE SaleDetails(
	Id int NOT NULL PRIMARY KEY IDENTITY(1, 1),
	SaleId int NOT NULL FOREIGN KEY REFERENCES Sales(Id),
	ProductId int NOT NULL FOREIGN KEY REFERENCES Products(Id),
	Quantity int NOT NULL DEFAULT 1,
	PurchasePrice money NOT NULL,
	Tax money NOT NULL DEFAULT 0,	
)
GO

IF OBJECT_ID('GetAllProducts') IS NOT NULL
BEGIN
	DROP PROCEDURE GetAllProducts
END
GO
CREATE PROCEDURE GetAllProducts
		
AS
	SET NOCOUNT ON
	SET XACT_ABORT ON
	
	BEGIN

	SELECT Id, ProductName, [Description], RetailPrice, QuantityInStock, IsTaxable
	FROM dbo.Products
	ORDER BY ProductName	

	END
GO

IF OBJECT_ID('GetProductById') IS NOT NULL
BEGIN
	DROP PROCEDURE GetProductById
END
GO
CREATE PROCEDURE GetProductById
		@Id [int]
AS
	SET NOCOUNT ON
	SET XACT_ABORT ON
	
	BEGIN

	SELECT Id, ProductName, [Description], RetailPrice, QuantityInStock, IsTaxable
	FROM dbo.Products
	WHERE (Id = @Id)

	END
GO

IF OBJECT_ID('CreateProduct') IS NOT NULL
BEGIN
	DROP PROCEDURE CreateProduct
END
GO
CREATE PROCEDURE CreateProduct
	(
		@ProductName [nvarchar](100),
		@Description [nvarchar](max),
		@RetailPrice [money],
		@QuantityInStock [int]		
	)
AS
	SET NOCOUNT ON
	SET XACT_ABORT ON
	
	BEGIN

	--SET @CreatedDate = ISNULL(@CreatedDate, GETUTCDATE())
	--SET @LastModified = ISNULL(@CreatedDate, GETUTCDATE())

	INSERT INTO dbo.Products
	(
		ProductName, [Description], RetailPrice, QuantityInStock
	)
	VALUES
	(
		@ProductName,
		@Description,
		@RetailPrice,
		@QuantityInStock
	)

	END
GO

--EXEC CreateProduct @ProductName = 'Fluffy Bath Towels', @Description = 'Large fluffy',	@RetailPrice = 29.95, @QuantityInStock = 20, @CreatedDate = NULL, @LastModified = NULL;
--GO

EXEC CreateProduct @ProductName = 'Fluffy Bath Towels', @Description = 'Large fluffy bath towl set (2 towels and 2 washcloths', @RetailPrice = 29.95, @QuantityInStock = 20;
GO

EXEC CreateProduct @ProductName = '10" Skillet', @Description = 'A non-stick skillet made with stainless steel.',	@RetailPrice = 18.75, @QuantityInStock = 10;
GO

EXEC CreateProduct @ProductName = 'Large Toaster Oven', @Description = 'A temperature-adjustable toaster oven with dual racks and interior light',	@RetailPrice = 49.99, @QuantityInStock = 5;
GO

EXEC CreateProduct @ProductName = 'Home Repair Kit', @Description = 'Feautes four screwdrivers, a hammer, a tape measure, a pair of pliers, and a level',	@RetailPrice = 25, @QuantityInStock = 50;
GO

INSERT INTO dbo.Users
(
    Id,
    FirstName,
    LastName
)
VALUES
(
    N'1', -- Id - nvarchar
    N'Asger', -- FirstName - nvarchar
    N'Lassen' -- LastName - nvarchar
)

IF OBJECT_ID('SaleReport') IS NOT NULL
BEGIN
	DROP PROCEDURE SaleReport
END
GO
CREATE PROCEDURE SaleReport
		
AS
	SET NOCOUNT ON
	SET XACT_ABORT ON
	
	BEGIN

	SELECT s.SaleDate, s.SubTotal, s.Tax, s.Total, u.FirstName, u.LastName
	FROM dbo.Sales s 
	INNER JOIN dbo.Users u ON s.CashierId = u.Id

	END
GO

IF OBJECT_ID('CreateSale') IS NOT NULL
BEGIN
	DROP PROCEDURE CreateSale
END
GO
CREATE PROCEDURE CreateSale
	(
		@Id int = NULL,
		@CashierId [nvarchar](128),
		@SaleDate [datetime2](7),
		@SubTotal [money],
		@Tax [money],
		@Total [money]
	)
AS
	SET NOCOUNT ON
	SET XACT_ABORT ON
	
	BEGIN

	INSERT INTO dbo.Sales
	(
		CashierId, SaleDate, SubTotal, Tax, Total
	)
	VALUES
	(
		@CashierId,
		@SaleDate,
		@SubTotal,
		@Tax,
		@Total
	)

	SELECT SCOPE_IDENTITY();

	END
GO

IF OBJECT_ID('CreateSaleDetail') IS NOT NULL
BEGIN
	DROP PROCEDURE CreateSaleDetail
END
GO
CREATE PROCEDURE CreateSaleDetail
	(
		@Id int = NULL,
		@SaleId [int],
		@ProductId [int],
		@Quantity [int],
		@PurchasePrice [money],
		@Tax [money]
	)
AS
	SET NOCOUNT ON
	SET XACT_ABORT ON
	
	BEGIN

	INSERT INTO dbo.SaleDetails
	(
		SaleId, ProductId, Quantity, PurchasePrice, Tax
	)
	VALUES
	(
		@SaleId,
		@ProductId,
		@Quantity,
		@PurchasePrice,
		@Tax
	)		

	END
GO

--IF OBJECT_ID('Sale_Lookup') IS NOT NULL
--BEGIN
--	DROP PROCEDURE Sale_Lookup
--END
--GO
--CREATE PROCEDURE Sale_Lookup
--		@CahierId nvarchar(128),
--		@SaleDate datetime2
--AS
--	SET NOCOUNT ON
--	SET XACT_ABORT ON
	
--	BEGIN

--	SELECT Id
--	FROM dbo.Sales
--	WHERE (CashierId = @CahierId AND SaleDate = @SaleDate)

--	END
--GO

--EXEC CreateSale @CashierId = 1, 
--				@SaleDate = '2019', 
--				@SubTotal = 40, 
--				@Tax = 4, 
--				@Total = 44;
--				

--IF OBJECT_ID('TestTable') IS NOT NULL
--BEGIN
--	DROP TYPE TestTable
--END
--GO
---- Create a table type to match your input parameters
--CREATE TYPE TestTable AS TABLE 
--( CashierId INT, SaleDate datetime2, SubTotal money, Tax money, Total money );
--GO

-- --change your stored procedure to accept such a table type parameter
--CREATE PROCEDURE Register
--    @Values TestTable READONLY
--AS
--BEGIN
--    BEGIN TRY

--	DECLARE @output TABLE (id int)
    
--        INSERT INTO dbo.Sales
--	(
--		CashierId, SaleDate, SubTotal, Tax, Total
--	)
--	OUTPUT inserted.ID INTO @output

--          -- get the values from the table type parameter
--		  SELECT 
--             CashierId, SaleDate, SubTotal, Tax, Total
--          FROM
--             @Values

--	select [@output].id from @output
	
--	--https://stackoverflow.com/questions/25969/insert-into-values-select-from
        
--    END TRY
--	BEGIN CATCH
--        SELECT -1
--    END CATCH
--END
--GO

---- declare a variable of that table type
--DECLARE @InputTable TestTable

---- insert values into that table variable
--INSERT INTO @InputTable(CashierId, SaleDate, SubTotal, Tax, Total) 
--VALUES (1, GETUTCDATE(), 5, 4, 3), (1, GETUTCDATE(), 50, 40, 30)

---- execute your stored procedure with this table as input parameter
--EXECUTE Register @InputTable


---- Create a table type to match your input parameters
--CREATE TYPE IdNameTable AS TABLE 
--( ID INT, Name NVARCHAR(50) );
--GO

---- change your stored procedure to accept such a table type parameter
--ALTER PROCEDURE [dbo].[Register]
--    @Values IdNameTable READONLY
--AS
--BEGIN
--    BEGIN TRY
--        INSERT INTO dbo.Group (Id, Name) 
--          -- get the values from the table type parameter
--          SELECT 
--             Id, Name
--          FROM
--             @Values

--        SELECT 0
--    END TRY
--    BEGIN CATCH
--        SELECT -1
--    END CATCH
--END
--GO
--See the extensive and freely available SQL Server Books Online documentation for more details on the table-valued parameter feature and how to use it

--If you want to use this from T-SQL, use this code:

---- declare a variable of that table type
--DECLARE @InputTable IdNameTable

---- insert values into that table variable
--INSERT INTO @InputTable(ID, Name) 
--VALUES (1, 'Test 1'), (2, 'Test 2')

---- execute your stored procedure with this table as input parameter
--EXECUTE [dbo].[Register] @InputTable

IF OBJECT_ID('GetAllInventories') IS NOT NULL
BEGIN
	DROP PROCEDURE GetAllInventories
END
GO
CREATE PROCEDURE GetAllInventories
		
AS
	SET NOCOUNT ON
	SET XACT_ABORT ON
	
	BEGIN

	SELECT ProductId, Quantity, PurchasePrice, PurchaseDate
	FROM dbo.Inventories	

	END
GO

IF OBJECT_ID('CreateInventory') IS NOT NULL
BEGIN
	DROP PROCEDURE CreateInventory
END
GO
CREATE PROCEDURE CreateInventory
	(
		@ProductId [int],
		@Quantity [int],
		@PurchasePrice [money],
		@PurchaseDate [datetime2](7)
	)
AS
	SET NOCOUNT ON
	SET XACT_ABORT ON
	
	BEGIN

	INSERT INTO dbo.Inventories
	(
		ProductId, Quantity, PurchasePrice, PurchaseDate
	)
	VALUES
	(
		@ProductId,
		@Quantity,
		@PurchasePrice,
		@PurchaseDate

	)
	SELECT Id, ProductId, Quantity, PurchasePrice, PurchaseDate
	FROM dbo.Inventories	

	END
GO