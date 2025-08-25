-- Drop database if exists
IF EXISTS (SELECT name FROM sys.databases WHERE name = N'PricingDB')
BEGIN
    DROP DATABASE PricingDB;
END
GO

-- Create Database
CREATE DATABASE PricingDB;
GO
USE PricingDB;
GO

-- =====================
-- Drop old tables if exist
-- =====================
IF OBJECT_ID('dbo.ProductDiffs', 'U') IS NOT NULL DROP TABLE ProductDiffs;
IF OBJECT_ID('dbo.MarkerPrices', 'U') IS NOT NULL DROP TABLE MarkerPrices;
IF OBJECT_ID('dbo.Markers', 'U') IS NOT NULL DROP TABLE Markers;
IF OBJECT_ID('dbo.Products', 'U') IS NOT NULL DROP TABLE Products;
GO

-- =====================
-- Table: Products
-- =====================
CREATE TABLE Products (
    ProductId INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(255),
    BasePrice DECIMAL(18,2) NOT NULL DEFAULT 0.00,
    IsActive BIT NOT NULL DEFAULT 1
);
GO

-- =====================
-- Table: Markers
-- =====================
CREATE TABLE Markers (
    MarkerId INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(255),
    IsActive BIT NOT NULL DEFAULT 1
);
GO

-- =====================
-- Table: MarkerPrices
-- =====================
CREATE TABLE MarkerPrices (
    MarkerPriceId INT IDENTITY(1,1) PRIMARY KEY,
    MarkerId INT NOT NULL,
    Value DECIMAL(18,2) NOT NULL,
    ValidFrom DATE NOT NULL,
    ValidTo DATE NULL,                       -- NULL means still valid
    IsActive BIT NOT NULL DEFAULT 1,
    CONSTRAINT fk_marker FOREIGN KEY (MarkerId) REFERENCES Markers(MarkerId)
);
GO

-- =====================
-- Table: ProductDiffs
-- =====================
CREATE TABLE ProductDiffs (
    ProductDiffId INT IDENTITY(1,1) PRIMARY KEY,
    ProductId INT NOT NULL,
    MarkerId INT NOT NULL,
    DiffValue DECIMAL(18,2) NOT NULL DEFAULT 0.00,
    IsActive BIT NOT NULL DEFAULT 1,
    CONSTRAINT fk_product FOREIGN KEY (ProductId) REFERENCES Products(ProductId),
    CONSTRAINT fk_marker_diff FOREIGN KEY (MarkerId) REFERENCES Markers(MarkerId)
);
GO

-- =====================
-- Sample Data
-- =====================

-- Products
INSERT INTO Products (Name, Description, BasePrice) VALUES
('Laptop', 'High-end gaming laptop', 50000.00),
('Smartphone', 'Latest Android smartphone', 25000.00),
('Tablet', '10-inch Android tablet', 15000.00),
('Headphones', 'Wireless noise-cancelling headphones', 8000.00),
('Smartwatch', 'Fitness tracking smartwatch', 12000.00),
('Monitor', '27-inch 4K monitor', 25000.00),
('Keyboard', 'Mechanical gaming keyboard', 5000.00),
('Mouse', 'Wireless gaming mouse', 3000.00),
('Printer', 'Color laser printer', 18000.00),
('Camera', 'DSLR camera with kit lens', 35000.00),
('Speaker', 'Bluetooth portable speaker', 6000.00),
('Router', 'Wi-Fi 6 router', 4000.00),
('External SSD', '1TB portable SSD', 9000.00),
('Webcam', '4K webcam with microphone', 7000.00),
('Microphone', 'USB condenser microphone', 5500.00),
('Game Console', 'Latest gaming console', 30000.00),
('VR Headset', 'Virtual reality headset', 22000.00),
('Drone', '4K camera drone', 28000.00),
('Projector', 'HD home theater projector', 32000.00),
('Smart Home Hub', 'Home automation controller', 4500.00),
('Fitness Tracker', 'Basic activity tracker', 2500.00),
('Power Bank', '20000mAh power bank', 3500.00);
GO

-- Markers
INSERT INTO Markers (Name, Description) VALUES
('Tax', 'Applicable tax'),
('Discount', 'Seasonal discount'),
('Shipping', 'Shipping charges'),
('Import Duty', 'Customs import duty'),
('Service Fee', 'Handling service fee'),
('Warranty', 'Extended warranty cost'),
('Installation', 'Professional installation'),
('Gift Wrap', 'Gift wrapping service');
GO

-- MarkerPrices
INSERT INTO MarkerPrices (MarkerId, Value, ValidFrom, ValidTo) VALUES
(1, 18.00, '2025-01-01', '2025-06-30'),
(1, 20.00, '2025-07-01', NULL),
(2, -2000.00, '2025-01-01', '2025-03-31'),
(2, -1500.00, '2025-04-01', '2025-06-30'),
(2, -1000.00, '2025-07-01', '2025-09-30'),
(2, -2500.00, '2025-10-01', '2025-12-31'),
(3, 500.00, '2025-01-01', '2025-03-31'),
(3, 600.00, '2025-04-01', '2025-06-30'),
(3, 550.00, '2025-07-01', '2025-09-30'),
(3, 700.00, '2025-10-01', '2025-12-31'),
(4, 10.00, '2025-01-01', '2025-06-30'),
(4, 12.00, '2025-07-01', NULL),
(5, 200.00, '2025-01-01', '2025-06-30'),
(5, 250.00, '2025-07-01', NULL),
(6, 1000.00, '2025-01-01', '2025-06-30'),
(6, 1200.00, '2025-07-01', NULL),
(7, 1500.00, '2025-01-01', '2025-06-30'),
(7, 1800.00, '2025-07-01', NULL),
(8, 100.00, '2025-01-01', '2025-06-30'),
(8, 120.00, '2025-07-01', '2025-09-30'),
(8, 150.00, '2025-10-01', '2025-12-31'),
(1, 15.00, '2024-01-01', '2024-12-31'),
(1, 22.00, '2026-01-01', '2026-06-30');
GO

-- ProductDiffs
INSERT INTO ProductDiffs (ProductId, MarkerId, DiffValue) VALUES
(1, 2, -3000.00),
(1, 3, 800.00),
(1, 6, 1500.00),
(2, 3, 1000.00),
(2, 2, -500.00),
(3, 2, -800.00),
(3, 3, 400.00),
(4, 5, 50.00),
(5, 6, 800.00),
(6, 7, 2000.00),
(7, 2, -200.00),
(8, 2, -100.00),
(9, 3, 1200.00),
(10, 4, 15.00),
(10, 7, 2500.00),
(11, 2, -300.00),
(14, 5, 100.00),
(15, 2, -150.00),
(16, 8, 200.00),
(19, 7, 3000.00);
GO
