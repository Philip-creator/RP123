-- Paste CREATE TABLE ... statements exported from the Progress database here.
-- Example:
-- CREATE TABLE Customer (
--   CustomerId INT PRIMARY KEY,
--   Name NVARCHAR(200) NOT NULL,
--   ...
-- );

-- If you prefer CSVs, add them to this directory (schema/csv/).

-- Sample DDL for starter entities: Customer, Item, SalesOrder, SalesOrderLine, Invoice
CREATE TABLE Customers (
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Name NVARCHAR(200) NOT NULL,
	ShortName NVARCHAR(200) NULL,
	Email NVARCHAR(200) NULL,
	Phone NVARCHAR(50) NULL,
	CreditLimit DECIMAL(18,2) DEFAULT(0),
	IsActive BIT DEFAULT(1)
);

CREATE TABLE Items (
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Sku NVARCHAR(50) NOT NULL,
	Description NVARCHAR(500) NULL,
	Cost DECIMAL(18,4) DEFAULT(0),
	Price DECIMAL(18,4) DEFAULT(0),
	IsStocked BIT DEFAULT(1)
);

CREATE TABLE SalesOrders (
	Id INT IDENTITY(1,1) PRIMARY KEY,
	CustomerId INT NOT NULL,
	OrderDate DATETIME2 NOT NULL,
	ShipDate DATETIME2 NULL,
	Status NVARCHAR(50) NULL,
	TotalAmount DECIMAL(18,4) DEFAULT(0),
	CONSTRAINT FK_SalesOrders_Customers FOREIGN KEY (CustomerId) REFERENCES Customers(Id)
);

CREATE TABLE SalesOrderLines (
	Id INT IDENTITY(1,1) PRIMARY KEY,
	SalesOrderId INT NOT NULL,
	ItemId INT NOT NULL,
	Quantity DECIMAL(18,4) DEFAULT(0),
	UnitPrice DECIMAL(18,4) DEFAULT(0),
	CONSTRAINT FK_SOLines_SalesOrders FOREIGN KEY (SalesOrderId) REFERENCES SalesOrders(Id),
	CONSTRAINT FK_SOLines_Items FOREIGN KEY (ItemId) REFERENCES Items(Id)
);

CREATE TABLE Invoices (
	Id INT IDENTITY(1,1) PRIMARY KEY,
	CustomerId INT NOT NULL,
	InvoiceDate DATETIME2 NOT NULL,
	DueDate DATETIME2 NULL,
	Amount DECIMAL(18,4) DEFAULT(0),
	PaidAmount DECIMAL(18,4) DEFAULT(0),
	Status NVARCHAR(50) NULL,
	CONSTRAINT FK_Invoices_Customers FOREIGN KEY (CustomerId) REFERENCES Customers(Id)
);
