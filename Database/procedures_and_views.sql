USE HouseRental;
GO

-- 1. Create View for Vacant Houses (Status 'Available')
IF EXISTS (SELECT * FROM sys.views WHERE name = 'vw_AvailableHouses')
    DROP VIEW vw_AvailableHouses;
GO

CREATE VIEW vw_AvailableHouses AS
SELECT 
    HouseID,
    COALESCE(Address, HouseAddress) AS PropertyAddress,
    COALESCE(Area, HouseArea) AS Size,
    RentPrice,
    Status
FROM 
    dbo.HouseInfo
WHERE 
    Status = 'Available' OR IsVacant = 'Yes';
GO

-- 2. Create Stored Procedure for User Login Authentication
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_AuthenticateUser')
    DROP PROCEDURE sp_AuthenticateUser;
GO

CREATE PROCEDURE sp_AuthenticateUser
    @Username NVARCHAR(50),
    @Password NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT UserID, Username, Role, Email, Phone 
    FROM dbo.Users 
    WHERE Username = @Username AND Password = @Password;
END;
GO

-- 3. Create Stored Procedure to Add New User
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_CreateUser')
    DROP PROCEDURE sp_CreateUser;
GO

CREATE PROCEDURE sp_CreateUser
    @Username NVARCHAR(50),
    @Password NVARCHAR(50),
    @Role NVARCHAR(50),
    @Email NVARCHAR(100),
    @Phone NVARCHAR(50),
    @Status INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Check if username exists
    IF EXISTS (SELECT 1 FROM dbo.Users WHERE Username = @Username)
    BEGIN
        SET @Status = -1; -- Username exists
        RETURN;
    END

    INSERT INTO dbo.Users (Username, Password, Role, Email, Phone)
    VALUES (@Username, @Password, @Role, @Email, @Phone);
    
    SET @Status = 1; -- Success
END;
GO

-- 4. Create View for Active Tenants
IF EXISTS (SELECT * FROM sys.views WHERE name = 'vw_ActiveTenants')
    DROP VIEW vw_ActiveTenants;
GO

CREATE VIEW vw_ActiveTenants AS
SELECT 
    TenantID,
    FullName,
    Gender,
    Phone,
    Email,
    IDNumber,
    Occupation,
    EmergencyContact,
    RegisteredDate
FROM 
    dbo.Tenants
WHERE 
    Status = 'Active';
GO

-- 5. Create Stored Procedure to Register a New Tenant
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_CreateTenant')
    DROP PROCEDURE sp_CreateTenant;
GO

CREATE PROCEDURE sp_CreateTenant
    @FullName NVARCHAR(100),
    @Gender NVARCHAR(20),
    @Phone NVARCHAR(30),
    @Email NVARCHAR(100),
    @IDNumber NVARCHAR(50),
    @Occupation NVARCHAR(100),
    @Address NVARCHAR(MAX),
    @EmergencyContact NVARCHAR(30),
    @Status NVARCHAR(30),
    @TenantID INT OUTPUT,
    @Result INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    -- Reject duplicate ID Number (when provided)
    IF @IDNumber IS NOT NULL AND @IDNumber <> ''
       AND EXISTS (SELECT 1 FROM dbo.Tenants WHERE IDNumber = @IDNumber)
    BEGIN
        SET @Result = -1; -- Duplicate ID Number
        RETURN;
    END

    IF @Status IS NULL OR @Status = ''
        SET @Status = 'Active';

    INSERT INTO dbo.Tenants (FullName, Gender, Phone, Email, IDNumber, Occupation, Address, EmergencyContact, Status, RegisteredDate)
    VALUES (@FullName, @Gender, @Phone, @Email, @IDNumber, @Occupation, @Address, @EmergencyContact, @Status, GETDATE());

    SET @TenantID = SCOPE_IDENTITY();
    SET @Result = 1; -- Success
END;
GO
