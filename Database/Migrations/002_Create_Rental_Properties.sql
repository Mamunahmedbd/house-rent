/* =====================================================================
   Migration 002 : Create RentalProperties table
   ---------------------------------------------------------------------
   Creates the [dbo].[RentalProperties] table to store rental property details
   (RentalID, Address, RentValue, Status). Safe to re-run (idempotent).

   Target database : HouseRental
   ===================================================================== */
USE HouseRental;
GO

-- ---------------------------------------------------------------
-- Step 1 : Ensure the RentalProperties table exists
-- ---------------------------------------------------------------
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RentalProperties]') AND type IN (N'U'))
BEGIN
    CREATE TABLE [dbo].[RentalProperties] (
        [RentalID]  INT            IDENTITY(1,1) PRIMARY KEY,
        [Address]   NVARCHAR(255)  NOT NULL,
        [RentValue] NVARCHAR(100)  NOT NULL,
        [Status]    NVARCHAR(50)   NOT NULL
    );
END
GO

-- ---------------------------------------------------------------
-- Step 2 : Record this migration in the tracking log
-- ---------------------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM [dbo].[__MigrationLog] WHERE [Version] = '002')
BEGIN
    INSERT INTO [dbo].[__MigrationLog] ([Version], [Description], [FileName])
    VALUES ('002', 'Create RentalProperties table', '002_Create_Rental_Properties.sql');
END
GO

PRINT 'Migration 002 applied: RentalProperties table created.';
GO
