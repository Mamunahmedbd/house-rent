USE HouseRental;
GO

-- Seed Default Admin User if table is empty
IF NOT EXISTS (SELECT 1 FROM [dbo].[Users] WHERE [Username] = 'admin')
BEGIN
    INSERT INTO [dbo].[Users] ([Username], [Password], [Role], [Email], [Phone])
    VALUES ('admin', '1234', 'Admin', 'admin@rental.com', '1234567890');
END

-- Seed sample Manager and regular User
IF NOT EXISTS (SELECT 1 FROM [dbo].[Users] WHERE [Username] = 'manager1')
BEGIN
    INSERT INTO [dbo].[Users] ([Username], [Password], [Role], [Email], [Phone])
    VALUES ('manager1', '1234', 'Manager', 'manager1@rental.com', '0987654321');
END

IF NOT EXISTS (SELECT 1 FROM [dbo].[Users] WHERE [Username] = 'user1')
BEGIN
    INSERT INTO [dbo].[Users] ([Username], [Password], [Role], [Email], [Phone])
    VALUES ('user1', '1234', 'User', 'user1@rental.com', '1122334455');
END

-- Seed sample HouseInfo data
IF NOT EXISTS (SELECT 1 FROM [dbo].[HouseInfo] WHERE [HouseID] = 'H101')
BEGIN
    INSERT INTO [dbo].[HouseInfo] ([HouseID], [CategoryID], [Address], [HouseAddress], [Area], [HouseArea], [RentPrice], [Deposit], [IsVacant], [Status], [Introduction])
    VALUES ('H101', 'C1', '123 Elegant St', '123 Elegant St', '1200 sqft', '1200 sqft', '1500', '1500', 'No', 'Available', 'Spacious 2 BHK close to downtown.');
END

IF NOT EXISTS (SELECT 1 FROM [dbo].[HouseInfo] WHERE [HouseID] = 'H102')
BEGIN
    INSERT INTO [dbo].[HouseInfo] ([HouseID], [CategoryID], [Address], [HouseAddress], [Area], [HouseArea], [RentPrice], [Deposit], [IsVacant], [Status], [Introduction])
    VALUES ('H102', 'C2', '456 Luxury Ave', '456 Luxury Ave', '1800 sqft', '1800 sqft', '2500', '2500', 'Yes', 'Rented', 'Luxury penthouse with skyline view.');
END

-- Seed sample Tenants data (guarded by IDNumber, the natural unique key)
IF NOT EXISTS (SELECT 1 FROM [dbo].[Tenants] WHERE [IDNumber] = 'NID-1001')
BEGIN
    INSERT INTO [dbo].[Tenants] ([FullName], [Gender], [Phone], [Email], [IDNumber], [Occupation], [Address], [EmergencyContact], [Status], [RegisteredDate])
    VALUES ('John Doe', 'Male', '555-0199', 'john.doe@example.com', 'NID-1001', 'Software Engineer', '123 Maple Street, Springfield', '555-0100', 'Active', '2024-01-15');
END

IF NOT EXISTS (SELECT 1 FROM [dbo].[Tenants] WHERE [IDNumber] = 'NID-1002')
BEGIN
    INSERT INTO [dbo].[Tenants] ([FullName], [Gender], [Phone], [Email], [IDNumber], [Occupation], [Address], [EmergencyContact], [Status], [RegisteredDate])
    VALUES ('Jane Smith', 'Female', '555-0144', 'jane.smith@example.com', 'NID-1002', 'Doctor', '456 Oak Avenue, Riverdale', '555-0111', 'Active', '2024-02-20');
END

IF NOT EXISTS (SELECT 1 FROM [dbo].[Tenants] WHERE [IDNumber] = 'NID-1003')
BEGIN
    INSERT INTO [dbo].[Tenants] ([FullName], [Gender], [Phone], [Email], [IDNumber], [Occupation], [Address], [EmergencyContact], [Status], [RegisteredDate])
    VALUES ('Michael Brown', 'Male', '555-0177', 'michael.brown@example.com', 'NID-1003', 'Teacher', '789 Pine Road, Lakeside', '555-0122', 'Moved Out', '2023-09-05');
END
