-- Create HouseRental Database if not exists
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'HouseRental')
BEGIN
    CREATE DATABASE HouseRental;
END
GO

USE HouseRental;
GO

-- 1. Create Users Table
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Users] (
        [UserID] INT IDENTITY(1,1) PRIMARY KEY,
        [Username] NVARCHAR(50) NOT NULL UNIQUE,
        [Password] NVARCHAR(50) NOT NULL,
        [Role] NVARCHAR(50) NOT NULL,
        [Email] NVARCHAR(100) NULL,
        [Phone] NVARCHAR(50) NULL
    );
END
GO

-- 2. Create HouseInfo Table
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HouseInfo]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[HouseInfo] (
        [HouseID] NVARCHAR(50) PRIMARY KEY,
        [CategoryID] NVARCHAR(50) NULL,
        [Address] NVARCHAR(255) NULL,
        [HouseAddress] NVARCHAR(255) NULL,
        [Area] NVARCHAR(100) NULL,
        [HouseArea] NVARCHAR(100) NULL,
        [RentPrice] NVARCHAR(100) NULL,
        [Deposit] NVARCHAR(100) NULL,
        [IsVacant] NVARCHAR(50) NULL,
        [Status] NVARCHAR(50) NULL,
        [Introduction] NVARCHAR(MAX) NULL
    );
END
GO

-- 3. Create Tenants Table
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tenants]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Tenants] (
        [TenantID] INT IDENTITY(1,1) PRIMARY KEY,
        [FullName] NVARCHAR(100) NOT NULL,
        [Phone] NVARCHAR(50) NOT NULL,
        [Email] NVARCHAR(100) NULL
    );
END
GO

-- 4. Create RentalProperties Table
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RentalProperties]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[RentalProperties] (
        [RentalID] INT IDENTITY(1,1) PRIMARY KEY,
        [Address] NVARCHAR(255) NOT NULL,
        [RentValue] NVARCHAR(100) NOT NULL,
        [Status] NVARCHAR(50) NOT NULL
    );
END
GO
