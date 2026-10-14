-- 01_create_database.sql
-- Creates LegacyEcommerceDb and the SQL-auth login the app uses.
-- Run against .\SQLEXPRESS with Windows auth, in SSMS or sqlcmd.

IF DB_ID('LegacyEcommerceDb') IS NULL
BEGIN
    CREATE DATABASE LegacyEcommerceDb;
END
GO

USE LegacyEcommerceDb;
GO

-- Server-level login (requires Mixed Mode auth enabled on the instance)
IF NOT EXISTS (SELECT 1 FROM sys.server_principals WHERE name = 'legacy_app_user')
BEGIN
    CREATE LOGIN legacy_app_user WITH PASSWORD = 'Legacy!Pass123',
        CHECK_POLICY = OFF;
END
GO

-- Database-level user mapped to the login
IF NOT EXISTS (SELECT 1 FROM sys.database_principals WHERE name = 'legacy_app_user')
BEGIN
    CREATE USER legacy_app_user FOR LOGIN legacy_app_user;
    ALTER ROLE db_owner ADD MEMBER legacy_app_user;
END
GO
