CREATE DATABASE ShopDB;
GO

USE ShopDB;
GO

-- Users Table
CREATE TABLE Users (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    UserName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(255) UNIQUE NOT NULL,
    PasswordHash NVARCHAR(255) NOT NULL, -- Store hashed password
    Phone NVARCHAR(20) NULL,
    Address NVARCHAR(255) NULL,
    Status NVARCHAR(10) DEFAULT 'Active' CHECK (Status IN ('Active', 'Inactive')), -- Active by default
    CreatedAt DATETIME DEFAULT GETDATE()
);
GO

-- Products Table
CREATE TABLE Products (
    ProductID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(500) NULL,
    Price DECIMAL(10,2) NOT NULL,
    Stock INT NOT NULL CHECK (Stock >= 0), -- Prevent negative stock
    CreatedAt DATETIME DEFAULT GETDATE()
);
GO

-- Orders Table
CREATE TABLE Orders (
    OrderID INT IDENTITY(1,1) PRIMARY KEY,
    UserID INT NOT NULL,
    OrderDate DATETIME DEFAULT GETDATE(),
    TotalAmount DECIMAL(10,2) NOT NULL,
    Status NVARCHAR(50) DEFAULT 'Pending', -- e.g., Pending, Shipped, Delivered
    FOREIGN KEY (UserID) REFERENCES Users(UserID) ON DELETE CASCADE
);
GO

-- Order Details Table
CREATE TABLE OrderDetails (
    OrderDetailID INT IDENTITY(1,1) PRIMARY KEY,
    OrderID INT NOT NULL,
    ProductID INT NOT NULL,
    Quantity INT NOT NULL CHECK (Quantity > 0),
    Price DECIMAL(10,2) NOT NULL, -- Price at the time of purchase
    FOREIGN KEY (OrderID) REFERENCES Orders(OrderID) ON DELETE CASCADE,
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID) ON DELETE CASCADE
);
GO
CREATE TABLE Cart (
    CartID INT IDENTITY(1,1) PRIMARY KEY,
    UserID INT NOT NULL,
    ProductID INT NOT NULL,
    Quantity INT NOT NULL CHECK (Quantity > 0),
    AddedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserID) REFERENCES Users(UserID) ON DELETE CASCADE,
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID) ON DELETE CASCADE
);


ALTER TABLE Users
ADD UserType NVARCHAR(10) DEFAULT 'Customer' CHECK (UserType IN ('Admin', 'Customer'));



INSERT INTO Users (UserName, Email, PasswordHash, Phone, Address, Status) 
VALUES 
('omar reda', 'omaraladeeb45@gmail.com', 'omar2468', '4662326553', '123 samanourd NY', 'Active'),
('ahmed ali', 'ahmed45@gmail.com', 'ahmed123', '987654321', '456 mansoura LA', 'Inactive');


select * from Users

INSERT INTO Products (Name, Description, Price, Stock) VALUES
('Laptop', 'High-performance laptop', 1200.00, 10),
('Smartphone', 'Latest model smartphone', 800.00, 20),
('Headphones', 'Noise-canceling headphones', 150.00, 30),
('Mouse', 'Wireless mouse', 25.00, 50),
('Keyboard', 'Mechanical keyboard', 70.00, 40);


INSERT INTO Orders (UserID, TotalAmount)  
VALUES (2, 2222.00); 

INSERT INTO Cart (UserID, ProductID, Quantity) VALUES
(2, 1, 1),  -- 1 Laptop
(2, 2, 2),  -- 2 Smartphones
(2, 3, 1);  -- 1 Headphone

select * from Users
select * from Cart
select * from Products



select * from Orders
DECLARE @OrderID INT = SCOPE_IDENTITY();
select @OrderID;
INSERT INTO OrderDetails (OrderID, ProductID, Quantity, Price)  
SELECT @OrderID, C.ProductID, C.Quantity, P.Price  
FROM Cart C  
JOIN Products P ON C.ProductID = P.ProductID  
WHERE C.UserID = 5;

UPDATE Products
SET Stock = Stock - C.Quantity
FROM Products P
JOIN Cart C ON P.ProductID = C.ProductID
WHERE C.UserID = 5;

DELETE FROM Cart WHERE UserID = 5;


select * from Cart


DECLARE @UserID INT = 5

BEGIN TRANSACTION;

-- Step 1: Insert a new order
INSERT INTO Orders (UserID, OrderDate, Status)  
VALUES (@UserID, GETDATE(), 'Pending');

-- Step 2: Get the OrderID of the newly inserted order
DECLARE @OrderID INT;
SET @OrderID = SCOPE_IDENTITY(); -- Retrieves the last inserted OrderID

-- Step 3: Insert all cart items into OrderDetails
INSERT INTO OrderDetails (OrderID, ProductID, Quantity, Price)
SELECT @OrderID, C.ProductID, C.Quantity, P.Price
FROM Cart C
JOIN Products P ON C.ProductID = P.ProductID
WHERE C.UserID = @UserID;

-- Step 4: Update stock in Products table
UPDATE P
SET P.Stock = P.Stock - C.Quantity  -- Corrected column name
FROM Products P
JOIN Cart C ON P.ProductID = C.ProductID
WHERE C.UserID = @UserID;

-- Step 5: Clear the user’s cart
DELETE FROM Cart WHERE UserID = @UserID;

COMMIT TRANSACTION;


select * from Cart


select * from Orders

CREATE PROCEDURE ProcessOrder
    @UserID INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRANSACTION;

    BEGIN TRY
        -- Step 1: Calculate TotalAmount for the order
        DECLARE @TotalAmount DECIMAL(10,2);
        SET @TotalAmount = (
            SELECT SUM(C.Quantity * P.Price)
            FROM Cart C
            JOIN Products P ON C.ProductID = P.ProductID
            WHERE C.UserID = @UserID
        );

        -- Ensure there are items in the cart before proceeding
        IF @TotalAmount IS NULL OR @TotalAmount = 0
        BEGIN
            PRINT 'Cart is empty. Cannot place order.';
            ROLLBACK TRANSACTION;
            RETURN;
        END

        -- Step 2: Insert a new order
        INSERT INTO Orders (UserID, OrderDate, TotalAmount, Status)  
        VALUES (@UserID, GETDATE(), @TotalAmount, 'Pending');

        -- Step 3: Get the OrderID of the newly inserted order
        DECLARE @OrderID INT;
        SET @OrderID = SCOPE_IDENTITY(); -- Retrieves the last inserted OrderID

        -- Step 4: Insert all cart items into OrderDetails
        INSERT INTO OrderDetails (OrderID, ProductID, Quantity, Price)
        SELECT @OrderID, C.ProductID, C.Quantity, P.Price
        FROM Cart C
        JOIN Products P ON C.ProductID = P.ProductID
        WHERE C.UserID = @UserID;

        -- Step 5: Update stock in Products table
        UPDATE P
        SET P.Stock = P.Stock - C.Quantity
        FROM Products P
        JOIN Cart C ON P.ProductID = C.ProductID
        WHERE C.UserID = @UserID;

        -- Step 6: Clear the user’s cart
        DELETE FROM Cart WHERE UserID = @UserID;

        -- Commit transaction if everything is successful
        COMMIT TRANSACTION;
        PRINT 'Order processed successfully!';
    END TRY
    BEGIN CATCH
        -- Rollback if an error occurs
        ROLLBACK TRANSACTION;
        PRINT 'Error occurred, transaction rolled back!';
        THROW;  -- Rethrows the error for debugging
    END CATCH
END;
GO
drop procedure ProcessOrder

exec ProcessOrder @UserID = 2


select * from Cart

select * from OrderDetails

select * from Orders

select * from Users

drop database ShopDB