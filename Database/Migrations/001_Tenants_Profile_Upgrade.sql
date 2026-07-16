/* =====================================================================
   Migration 001 : Upgrade Tenants table to full tenant-profile schema
   ---------------------------------------------------------------------
   Adds tenant profile columns (Gender, ID Number, Occupation, Address,
   Emergency Contact, Status, RegisteredDate) plus a unique index on
   ID Number.  Safe to re-run (fully idempotent).

   Handles BOTH scenarios:
     (1) Tenants table already exists with the OLD layout (4 columns)
         -> ALTER TABLE ADD any missing columns, then back-fill defaults.
     (2) Tenants table does NOT exist
         -> CREATE it with the full layout.

   Target database : HouseRental
   ===================================================================== */
USE HouseRental;
GO

-- ---------------------------------------------------------------
-- Step 1 : Ensure the Tenants table exists (full layout if missing)
-- ---------------------------------------------------------------
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tenants]') AND type IN (N'U'))
BEGIN
    CREATE TABLE [dbo].[Tenants] (
        [TenantID]         INT            IDENTITY(1,1) PRIMARY KEY,
        [FullName]         NVARCHAR(100)  NOT NULL,
        [Gender]           NVARCHAR(20)   NULL,
        [Phone]            NVARCHAR(30)   NOT NULL,
        [Email]            NVARCHAR(100)  NULL,
        [IDNumber]         NVARCHAR(50)   NULL,
        [Occupation]       NVARCHAR(100)  NULL,
        [Address]          NVARCHAR(MAX)  NULL,
        [EmergencyContact] NVARCHAR(30)   NULL,
        [Status]           NVARCHAR(30)   NULL,
        [RegisteredDate]   DATETIME       NULL
    );
END
GO

-- ---------------------------------------------------------------
-- Step 2 : Add missing columns to an existing (old-layout) table
--          COL_LENGTH returns NULL when column (or table) is absent.
-- ---------------------------------------------------------------
IF COL_LENGTH('dbo.Tenants','Gender') IS NULL
BEGIN
    ALTER TABLE [dbo].[Tenants] ADD [Gender] NVARCHAR(20) NULL;
END
GO

IF COL_LENGTH('dbo.Tenants','IDNumber') IS NULL
BEGIN
    ALTER TABLE [dbo].[Tenants] ADD [IDNumber] NVARCHAR(50) NULL;
END
GO

IF COL_LENGTH('dbo.Tenants','Occupation') IS NULL
BEGIN
    ALTER TABLE [dbo].[Tenants] ADD [Occupation] NVARCHAR(100) NULL;
END
GO

IF COL_LENGTH('dbo.Tenants','Address') IS NULL
BEGIN
    ALTER TABLE [dbo].[Tenants] ADD [Address] NVARCHAR(MAX) NULL;
END
GO

IF COL_LENGTH('dbo.Tenants','EmergencyContact') IS NULL
BEGIN
    ALTER TABLE [dbo].[Tenants] ADD [EmergencyContact] NVARCHAR(30) NULL;
END
GO

IF COL_LENGTH('dbo.Tenants','Status') IS NULL
BEGIN
    ALTER TABLE [dbo].[Tenants] ADD [Status] NVARCHAR(30) NULL;
END
GO

IF COL_LENGTH('dbo.Tenants','RegisteredDate') IS NULL
BEGIN
    ALTER TABLE [dbo].[Tenants] ADD [RegisteredDate] DATETIME NULL;
END
GO

-- ---------------------------------------------------------------
-- Step 3 : Back-fill defaults for pre-existing rows
-- ---------------------------------------------------------------
UPDATE [dbo].[Tenants] SET [Status] = 'Active'        WHERE [Status] IS NULL;
GO

UPDATE [dbo].[Tenants] SET [RegisteredDate] = GETDATE() WHERE [RegisteredDate] IS NULL;
GO

-- ---------------------------------------------------------------
-- Step 4 : Unique index on ID Number (filtered to allow NULLs)
-- ---------------------------------------------------------------
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'UX_Tenants_IDNumber' AND object_id = OBJECT_ID(N'[dbo].[Tenants]'))
BEGIN
    CREATE UNIQUE INDEX UX_Tenants_IDNumber ON [dbo].[Tenants]([IDNumber]) WHERE [IDNumber] IS NOT NULL;
END
GO

-- ---------------------------------------------------------------
-- Step 5 : Record this migration in the tracking log
-- ---------------------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM [dbo].[__MigrationLog] WHERE [Version] = '001')
BEGIN
    INSERT INTO [dbo].[__MigrationLog] ([Version], [Description], [FileName])
    VALUES ('001', 'Upgrade Tenants table to full tenant-profile schema', '001_Tenants_Profile_Upgrade.sql');
END
GO

PRINT 'Migration 001 applied: Tenants table upgraded to full profile schema.';
GO
