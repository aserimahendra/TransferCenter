-- Migration: add Role column to User table if not exists
-- Adds a SMALLINT column named Role with default 0

IF COL_LENGTH('dbo.[User]', 'Role') IS NULL
BEGIN
    PRINT 'Adding Role column to dbo.[User]';
    ALTER TABLE dbo.[User]
    ADD [Role] smallint NOT NULL CONSTRAINT DF_User_Role DEFAULT (0);
END
ELSE
BEGIN
    PRINT 'Role column already exists on dbo.[User]';
END

-- Set Role = 1 for admin users (safe check). Adjust conditions if your admin user is identified differently.
IF COL_LENGTH('dbo.[User]', 'Role') IS NOT NULL
BEGIN
    PRINT 'Updating admin user role to 1 where applicable';
    UPDATE dbo.[User]
    SET [Role] = 1
    WHERE [Role] <> 1 AND (
        LOWER(ISNULL([LoginId], '')) = 'admin'
        
    );
END
