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
    Password  NVARCHAR(256) NOT NULL, 
    Email     NVARCHAR(100) NOT NULL
);

CREATE TABLE Customer (
    CustomerID INT IDENTITY(1,1) PRIMARY KEY,
    FullName   NVARCHAR(100) NOT NULL,
    Email      NVARCHAR(100) NOT NULL UNIQUE,
    Phone      NVARCHAR(20)  NOT NULL,
    Address    NVARCHAR(200) NOT NULL,
    Password   NVARCHAR(256) NOT NULL,
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
    PromotionEndDate      DATE NULL,
    Description           NVARCHAR(500) NULL,
    ActiveIngredient      NVARCHAR(200) NULL,
    UsageInstructions     NVARCHAR(500) NULL,
    Warnings              NVARCHAR(500) NULL,
    SideEffects           NVARCHAR(300) NULL,
    PackSize              NVARCHAR(100) NULL
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
    CancellationReason NVARCHAR(500) NULL,
    CONSTRAINT FK_Order_Customer FOREIGN KEY (CustomerID) REFERENCES Customer(CustomerID),
    CONSTRAINT CK_Order_Status CHECK (Status IN ('Pending', 'Ready for Pickup', 'Delivered', 'Cancelled'))
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

CREATE TABLE CartItem (
    CartItemID       INT IDENTITY(1,1) PRIMARY KEY,
    CustomerID       INT NOT NULL,
    MedicineID       INT NOT NULL,
    Quantity         INT NOT NULL CHECK (Quantity > 0),
    PrescriptionPath NVARCHAR(255) NULL,
    AddedAt          DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_CartItem_Customer FOREIGN KEY (CustomerID) REFERENCES Customer(CustomerID),
    CONSTRAINT FK_CartItem_Medicine FOREIGN KEY (MedicineID) REFERENCES Medicine(MedicineID),
    CONSTRAINT UQ_CartItem_Customer_Medicine UNIQUE (CustomerID, MedicineID)
);

GO

-- ---------------------------------------------------------------------------
-- Seed data (demo logins, mixed catalog: meds/wellness only)
-- Passwords stored as PBKDF2-SHA256 (Rfc2898DeriveBytes, 100000 iterations, 32-byte hash).
-- Format: iterations$saltBase64$hashBase64 (see PasswordHasher)
-- Demo plaintext (for login): admin123 / Customer123 / customer123 / Customer123
-- Fixed salts (16 bytes, last byte 1..4) so seeds are reproducible.
-- ---------------------------------------------------------------------------

INSERT INTO Admin (Username, Password, Email)
VALUES (N'admin', N'100000$AAAAAAAAAAAAAAAAAAAAAQ==$yWsCnUmJjkvSHVoBAzJtW4j3Vl7wobUZIS5YvUKjMpU=', N'admin@smartmed.lk');

INSERT INTO Customer (FullName, Email, Phone, Address, Password, IsActive)
VALUES
    (N'Jane Customer', N'customer@gmail.com', N'0771234567', N'12 Hospital Road, Colombo', N'100000$AAAAAAAAAAAAAAAAAAAAAg==$HvBMuEdPKF45iIfPiMZCLS8zO3N8dqtekH+oTdrusOU=', 1),
    (N'John Doe', N'john@email.com', N'0779876543', N'45 Main Street, Kandy', N'100000$AAAAAAAAAAAAAAAAAAAAAw==$uIN7l+CkuW2bTzy1sUyNE9N0CpPbChipRJ5apx7gwgA=', 1),
    (N'Kamal Silva', N'kamal@email.com', N'0712345678', N'8 Lake Road, Galle', N'100000$AAAAAAAAAAAAAAAAAAAABA==$q5T94VvBc3hEHXKKshyh9S+xRXsWmS4sPH3aO8anMYc=', 1);

INSERT INTO Medicine (
    MedicineName, Category, Dosage, Price, StockQuantity, Supplier, ExpiryDate,
    RequiresPrescription, DiscountPercent, IsOnPromotion, PromotionStartDate, PromotionEndDate,
    Description, ActiveIngredient, UsageInstructions, Warnings, SideEffects, PackSize)
VALUES
    (N'Amoxicillin 500mg', N'Antibiotic', N'Capsule', 450.00, 12, N'PharmaCo', DATEADD(MONTH, 2, CAST(GETDATE() AS DATE)),
        1, 5.00, 1, DATEADD(DAY, -7, CAST(GETDATE() AS DATE)), DATEADD(DAY, 23, CAST(GETDATE() AS DATE)),
        N'Broad-spectrum penicillin antibiotic used to treat common bacterial infections.',
        N'Amoxicillin',
        N'Take one capsule every 8 hours with water. Complete the full prescribed course.',
        N'Do not use if allergic to penicillin. May reduce oral contraceptive effectiveness.',
        N'Nausea, diarrhea, or mild skin rash.',
        N'21 capsules'),
    (N'Paracetamol 500mg', N'Analgesic', N'Tablet', 120.00, 85, N'MedSupply', DATEADD(MONTH, 10, CAST(GETDATE() AS DATE)),
        0, 0.00, 0, NULL, NULL,
        N'Pain reliever and fever reducer for mild to moderate pain and fever.',
        N'Paracetamol (Acetaminophen)',
        N'Take 1–2 tablets every 4–6 hours as needed. Do not exceed 8 tablets in 24 hours.',
        N'Do not exceed the recommended dose. Avoid with other paracetamol-containing products.',
        N'Rare rash; overdose can cause serious liver damage.',
        N'20 tablets'),
    (N'Metformin 850mg', N'Antidiabetic', N'Tablet', 380.00, 8, N'HealthLine', DATEADD(DAY, -5, CAST(GETDATE() AS DATE)),
        1, 10.00, 0, NULL, NULL,
        N'Oral antidiabetic medicine that helps control blood sugar in type 2 diabetes.',
        N'Metformin hydrochloride',
        N'Take with meals as directed by your doctor, usually once or twice daily.',
        N'Prescription only. Tell your doctor about kidney problems before use.',
        N'Stomach upset, diarrhea, or metallic taste.',
        N'30 tablets'),
    (N'Vitamin C 500mg', N'Wellness', N'Tablet', 250.00, 40, N'WellLife', DATEADD(MONTH, 8, CAST(GETDATE() AS DATE)),
        0, 5.00, 1, CAST(GETDATE() AS DATE), DATEADD(DAY, 30, CAST(GETDATE() AS DATE)),
        N'Dietary supplement that supports immune health and daily antioxidant intake.',
        N'Ascorbic acid',
        N'Take one tablet daily with food and a glass of water.',
        N'Consult a doctor if you have kidney stones or take blood thinners.',
        N'Mild stomach upset at high doses.',
        N'60 tablets'),
    (N'Multivitamin Tablets', N'Wellness', N'Tablet', 680.00, 25, N'WellLife', DATEADD(MONTH, 6, CAST(GETDATE() AS DATE)),
        0, 0.00, 0, NULL, NULL,
        N'Daily multivitamin and mineral supplement for general wellness support.',
        N'Vitamins A, B-complex, C, D, E; selected minerals',
        N'Take one tablet daily with breakfast.',
        N'Do not exceed one tablet daily. Keep out of reach of children.',
        N'Mild nausea if taken on an empty stomach.',
        N'30 tablets'),
    (N'Hand Sanitizer 500ml', N'Wellness', N'Bottle', 320.00, 50, N'CleanCare', DATEADD(MONTH, 12, CAST(GETDATE() AS DATE)),
        0, 0.00, 0, NULL, NULL,
        N'Alcohol-based hand sanitizer for quick hygiene when soap and water are unavailable.',
        N'Ethyl alcohol 70%',
        N'Apply a palmful to hands and rub until dry. Do not rinse off.',
        N'For external use only. Flammable — keep away from heat and open flame.',
        N'Skin dryness or irritation with frequent use.',
        N'500 ml bottle');

DECLARE @JaneId INT = (SELECT CustomerID FROM Customer WHERE Email = N'customer@gmail.com');
DECLARE @JohnId INT = (SELECT CustomerID FROM Customer WHERE Email = N'john@email.com');
DECLARE @AmoxId INT = (SELECT MedicineID FROM Medicine WHERE MedicineName = N'Amoxicillin 500mg');
DECLARE @ParaId INT = (SELECT MedicineID FROM Medicine WHERE MedicineName = N'Paracetamol 500mg');
DECLARE @VitCId INT = (SELECT MedicineID FROM Medicine WHERE MedicineName = N'Vitamin C 500mg');

INSERT INTO [Order] (CustomerID, OrderDate, Status, TotalAmount, PaymentMethod, PaymentStatus, PaymentReference)
VALUES
    (@JaneId, DATEADD(DAY, -2, GETDATE()), N'Delivered', 1450.00, N'Card', N'Paid', N'CARD-4242'),
    (@JaneId, DATEADD(DAY, -1, GETDATE()), N'Pending', 1250.00, N'Cash on Pickup', N'Pay on Pickup', NULL),
    (@JohnId, DATEADD(DAY, -3, GETDATE()), N'Ready for Pickup', 890.00, N'Cash on Pickup', N'Pay on Pickup', NULL);

DECLARE @Order1 INT = (SELECT OrderID FROM [Order] WHERE CustomerID = @JaneId AND Status = N'Delivered');
DECLARE @Order2 INT = (SELECT OrderID FROM [Order] WHERE CustomerID = @JaneId AND Status = N'Pending');
DECLARE @Order3 INT = (SELECT OrderID FROM [Order] WHERE CustomerID = @JohnId AND Status = N'Ready for Pickup');

INSERT INTO OrderItem (OrderID, MedicineID, Quantity, UnitPrice, Subtotal)
VALUES
    (@Order1, @AmoxId, 2, 427.50, 855.00),
    (@Order1, @ParaId, 1, 120.00, 120.00),
    (@Order1, @VitCId, 2, 237.50, 475.00),
    (@Order2, @AmoxId, 2, 427.50, 855.00),
    (@Order2, @ParaId, 1, 120.00, 120.00),
    (@Order2, @VitCId, 1, 275.00, 275.00),
    (@Order3, @ParaId, 1, 120.00, 120.00),
    (@Order3, @VitCId, 3, 256.67, 770.00);

INSERT INTO Prescription (CustomerID, OrderID, PrescriptionFile, UploadDate, Status)
VALUES (@JaneId, @Order2, N'seed_rx_jane.pdf', DATEADD(DAY, -1, GETDATE()), N'Pending');

GO
