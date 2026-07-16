/* =====================================================================
   Migration 000 : Create the migration tracking log
   ---------------------------------------------------------------------
   Keeps a record of every migration applied to the HouseRental database
   (similar to Flyway's schema_history).  Future migrations should insert
   a row here once they have run successfully.

   Idempotent : safe to run multiple times.
   ===================================================================== */
USE HouseRental;
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[__MigrationLog]') AND type IN (N'U'))
BEGIN
    CREATE TABLE [dbo].[__MigrationLog] (
        [Id]          INT            IDENTITY(1,1) PRIMARY KEY,
        [Version]     NVARCHAR(50)   NOT NULL UNIQUE,
        [Description] NVARCHAR(255)  NOT NULL,
        [FileName]    NVARCHAR(255)  NULL,
        [AppliedOn]   DATETIME       NOT NULL DEFAULT GETDATE()
    );
END
GO

PRINT 'Migration 000 applied: __MigrationLog ready.';
GO
