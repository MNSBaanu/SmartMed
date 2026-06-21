-- Run against an existing SmartMedDB to add discount columns and unique medicine name.
-- Execute in SQL Server Management Studio (F5). Safe to re-run.
USE SmartMedDB;
GO

-- Step 1: Add DiscountPercent column
IF COL_LENGTH('dbo.Medicine', 'DiscountPercent') IS NULL
BEGIN
    ALTER TABLE dbo.Medicine
    ADD DiscountPercent DECIMAL(5,2) NOT NULL
        CONSTRAINT DF_Medicine_DiscountPercent DEFAULT 0;
END
GO

-- Step 2: Add check constraint (separate batch so column is visible)
IF COL_LENGTH('dbo.Medicine', 'DiscountPercent') IS NOT NULL
   AND NOT EXISTS (
       SELECT 1 FROM sys.check_constraints
       WHERE name = 'CK_Medicine_DiscountPercent')
BEGIN
    ALTER TABLE dbo.Medicine
    ADD CONSTRAINT CK_Medicine_DiscountPercent
        CHECK (DiscountPercent >= 0 AND DiscountPercent <= 100);
END
GO

-- Step 3: Add IsOnPromotion column
IF COL_LENGTH('dbo.Medicine', 'IsOnPromotion') IS NULL
BEGIN
    ALTER TABLE dbo.Medicine
    ADD IsOnPromotion BIT NOT NULL
        CONSTRAINT DF_Medicine_IsOnPromotion DEFAULT 0;
END
GO

-- Step 4: Rename duplicate medicine names before unique index (keeps lowest MedicineID unchanged)
;WITH ranked AS (
    SELECT
        MedicineID,
        MedicineName,
        ROW_NUMBER() OVER (
            PARTITION BY LOWER(LTRIM(RTRIM(MedicineName)))
            ORDER BY MedicineID
        ) AS RowNum
    FROM dbo.Medicine
)
UPDATE m
SET MedicineName = LEFT(r.MedicineName, 90) + N' (' + CAST(r.MedicineID AS NVARCHAR(10)) + N')'
FROM dbo.Medicine m
INNER JOIN ranked r ON m.MedicineID = r.MedicineID
WHERE r.RowNum > 1;
GO

-- Step 5: Create unique index on medicine name (skipped if already present)
IF EXISTS (
    SELECT 1 FROM sys.indexes
    WHERE name = N'UQ_Medicine_MedicineName'
      AND object_id = OBJECT_ID(N'dbo.Medicine'))
BEGIN
    PRINT N'Skipped: UQ_Medicine_MedicineName already exists.';
END
ELSE IF EXISTS (
    SELECT 1
    FROM sys.indexes i
    INNER JOIN sys.index_columns ic
        ON i.object_id = ic.object_id AND i.index_id = ic.index_id
    INNER JOIN sys.columns c
        ON c.object_id = ic.object_id AND c.column_id = ic.column_id
    WHERE i.object_id = OBJECT_ID(N'dbo.Medicine')
      AND c.name = N'MedicineName'
      AND i.is_unique = 1)
BEGIN
    PRINT N'Skipped: MedicineName already has a unique index.';
END
ELSE
BEGIN
    CREATE UNIQUE INDEX UQ_Medicine_MedicineName ON dbo.Medicine (MedicineName);
    PRINT N'Created UQ_Medicine_MedicineName.';
END
GO

-- Verify
SELECT COLUMN_NAME, DATA_TYPE
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'Medicine'
ORDER BY ORDINAL_POSITION;
GO
