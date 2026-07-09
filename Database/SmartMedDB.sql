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
    Password   NVARCHAR(100) NOT NULL,
    IsActive   BIT NOT NULL DEFAULT 1
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
    IsOnPromotion         BIT NOT NULL DEFAULT 0,
    PromotionStartDate    DATE NULL,
    PromotionEndDate      DATE NULL
);

CREATE TABLE [Order] (
    OrderID      INT IDENTITY(1,1) PRIMARY KEY,
    CustomerID   INT NOT NULL,
    OrderDate    DATETIME NOT NULL DEFAULT GETDATE(),
    Status       NVARCHAR(30) NOT NULL,
    TotalAmount  DECIMAL(10,2) NOT NULL CHECK (TotalAmount >= 0),
    PaymentMethod   NVARCHAR(30) NOT NULL DEFAULT 'Cash on Pickup',
    PaymentStatus   NVARCHAR(30) NOT NULL DEFAULT 'Pay on Pickup',
    PaymentReference NVARCHAR(50) NULL,
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
    OrderID          INT NOT NULL,
    PrescriptionFile NVARCHAR(255) NOT NULL,
    UploadDate       DATETIME NOT NULL DEFAULT GETDATE(),
    Status           NVARCHAR(30) NOT NULL DEFAULT 'Pending',
    CONSTRAINT FK_Prescription_Customer FOREIGN KEY (CustomerID) REFERENCES Customer(CustomerID),
    CONSTRAINT FK_Prescription_Order FOREIGN KEY (OrderID) REFERENCES [Order](OrderID)
);

GO
