-- SmartMed Pharmacy Database Script
-- Run in SQL Server Management Studio

IF DB_ID('SmartMedDB') IS NOT NULL
    DROP DATABASE SmartMedDB;
GO

CREATE DATABASE SmartMedDB;
GO

USE SmartMedDB;
GO

CREATE TABLE Admin (
    AdminID   INT IDENTITY(1,1) PRIMARY KEY,
    Username  NVARCHAR(50)  NOT NULL UNIQUE,
    Password  NVARCHAR(100) NOT NULL,
    Email     NVARCHAR(100) NOT NULL
);

CREATE TABLE Customer (
    CustomerID INT IDENTITY(1,1) PRIMARY KEY,
    FullName   NVARCHAR(100) NOT NULL,
    Email      NVARCHAR(100) NOT NULL UNIQUE,
    Phone      NVARCHAR(20)  NOT NULL,
    Address    NVARCHAR(200) NOT NULL,
    Password   NVARCHAR(100) NOT NULL
);

CREATE TABLE Medicine (
    MedicineID            INT IDENTITY(1,1) PRIMARY KEY,
    MedicineName          NVARCHAR(100) NOT NULL UNIQUE,
    Category              NVARCHAR(50)  NOT NULL,
    Dosage                NVARCHAR(50)  NOT NULL,
    Price                 DECIMAL(10,2) NOT NULL CHECK (Price >= 0),
    StockQuantity         INT NOT NULL CHECK (StockQuantity >= 0),
    Supplier              NVARCHAR(100) NOT NULL,
    ExpiryDate            DATE NOT NULL,
    RequiresPrescription  BIT NOT NULL DEFAULT 0,
    DiscountPercent       DECIMAL(5,2) NOT NULL DEFAULT 0 CHECK (DiscountPercent >= 0 AND DiscountPercent <= 100),
    IsOnPromotion         BIT NOT NULL DEFAULT 0
);

CREATE TABLE [Order] (
    OrderID      INT IDENTITY(1,1) PRIMARY KEY,
    CustomerID   INT NOT NULL,
    OrderDate    DATETIME NOT NULL DEFAULT GETDATE(),
    Status       NVARCHAR(30) NOT NULL,
    TotalAmount  DECIMAL(10,2) NOT NULL CHECK (TotalAmount >= 0),
    CONSTRAINT FK_Order_Customer FOREIGN KEY (CustomerID) REFERENCES Customer(CustomerID),
    CONSTRAINT CK_Order_Status CHECK (Status IN ('Pending', 'Ready for Pickup', 'Delivered'))
);

CREATE TABLE OrderItem (
    OrderItemID INT IDENTITY(1,1) PRIMARY KEY,
    OrderID     INT NOT NULL,
    MedicineID  INT NOT NULL,
    Quantity    INT NOT NULL CHECK (Quantity > 0),
    UnitPrice   DECIMAL(10,2) NOT NULL CHECK (UnitPrice >= 0),
    Subtotal    DECIMAL(10,2) NOT NULL CHECK (Subtotal >= 0),
    CONSTRAINT FK_OrderItem_Order FOREIGN KEY (OrderID) REFERENCES [Order](OrderID),
    CONSTRAINT FK_OrderItem_Medicine FOREIGN KEY (MedicineID) REFERENCES Medicine(MedicineID)
);

CREATE TABLE Prescription (
    PrescriptionID   INT IDENTITY(1,1) PRIMARY KEY,
    CustomerID       INT NOT NULL,
    PrescriptionFile NVARCHAR(255) NOT NULL,
    UploadDate       DATETIME NOT NULL DEFAULT GETDATE(),
    Status           NVARCHAR(30) NOT NULL DEFAULT 'Pending',
    CONSTRAINT FK_Prescription_Customer FOREIGN KEY (CustomerID) REFERENCES Customer(CustomerID)
);

-- Seed data
INSERT INTO Admin (Username, Password, Email)
VALUES ('admin', 'admin123', 'admin@smartmed.com');

INSERT INTO Customer (FullName, Email, Phone, Address, Password)
VALUES
    ('John Smith', 'john@email.com', '0771234567', '12 Main Street, Colombo', 'customer123'),
    ('Jane Doe', 'jane@email.com', '0779876543', '45 Park Road, Kandy', 'customer123');

INSERT INTO Medicine (MedicineName, Category, Dosage, Price, StockQuantity, Supplier, ExpiryDate, RequiresPrescription)
VALUES
    ('Paracetamol', 'Pain Relief', '500mg', 5.50, 200, 'PharmaCo', '2027-06-30', 0),
    ('Amoxicillin', 'Antibiotic', '250mg', 12.00, 80, 'MediSupply', '2026-12-31', 1),
    ('Ibuprofen', 'Pain Relief', '400mg', 8.75, 150, 'PharmaCo', '2027-03-15', 0),
    ('Vitamin C', 'Supplements', '1000mg', 15.00, 100, 'HealthPlus', '2028-01-20', 0),
    ('Cough Syrup', 'Cold & Flu', '100ml', 9.25, 60, 'MediSupply', '2026-08-10', 0);

INSERT INTO [Order] (CustomerID, OrderDate, Status, TotalAmount)
VALUES (1, DATEADD(DAY, -2, GETDATE()), 'Delivered', 14.25);

INSERT INTO OrderItem (OrderID, MedicineID, Quantity, UnitPrice, Subtotal)
VALUES
    (1, 1, 2, 5.50, 11.00),
    (1, 3, 1, 8.75, 8.75);

UPDATE [Order] SET TotalAmount = 19.75 WHERE OrderID = 1;

INSERT INTO [Order] (CustomerID, OrderDate, Status, TotalAmount)
VALUES (1, DATEADD(DAY, -1, GETDATE()), 'Pending', 15.00);

INSERT INTO OrderItem (OrderID, MedicineID, Quantity, UnitPrice, Subtotal)
VALUES (2, 4, 1, 15.00, 15.00);

GO
