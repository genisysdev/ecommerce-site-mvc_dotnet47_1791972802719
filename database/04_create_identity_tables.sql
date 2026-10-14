-- 04_create_identity_tables.sql
-- ASP.NET Identity 2.0 schema (AspNet* tables) for the User Account slice.
-- Includes the custom FullName column on AspNetUsers (ApplicationUser.FullName).
-- Run against LegacyEcommerceDb after 01-03.
USE LegacyEcommerceDb;
GO

IF OBJECT_ID('dbo.AspNetUserRoles','U')  IS NOT NULL DROP TABLE dbo.AspNetUserRoles;
IF OBJECT_ID('dbo.AspNetUserClaims','U') IS NOT NULL DROP TABLE dbo.AspNetUserClaims;
IF OBJECT_ID('dbo.AspNetUserLogins','U') IS NOT NULL DROP TABLE dbo.AspNetUserLogins;
IF OBJECT_ID('dbo.AspNetRoles','U')      IS NOT NULL DROP TABLE dbo.AspNetRoles;
IF OBJECT_ID('dbo.AspNetUsers','U')      IS NOT NULL DROP TABLE dbo.AspNetUsers;
GO

CREATE TABLE dbo.AspNetUsers (
    Id                   NVARCHAR(128)  NOT NULL PRIMARY KEY,
    Email                NVARCHAR(256)  NULL,
    EmailConfirmed       BIT            NOT NULL DEFAULT(0),
    PasswordHash         NVARCHAR(MAX)  NULL,
    SecurityStamp        NVARCHAR(MAX)  NULL,
    PhoneNumber          NVARCHAR(MAX)  NULL,
    PhoneNumberConfirmed BIT            NOT NULL DEFAULT(0),
    TwoFactorEnabled     BIT            NOT NULL DEFAULT(0),
    LockoutEndDateUtc    DATETIME       NULL,
    LockoutEnabled       BIT            NOT NULL DEFAULT(0),
    AccessFailedCount    INT            NOT NULL DEFAULT(0),
    UserName             NVARCHAR(256)  NOT NULL,
    FullName             NVARCHAR(MAX)  NULL
);
GO
CREATE UNIQUE INDEX UserNameIndex ON dbo.AspNetUsers (UserName);
GO

CREATE TABLE dbo.AspNetRoles (
    Id   NVARCHAR(128) NOT NULL PRIMARY KEY,
    Name NVARCHAR(256) NOT NULL
);
GO
CREATE UNIQUE INDEX RoleNameIndex ON dbo.AspNetRoles (Name);
GO

CREATE TABLE dbo.AspNetUserRoles (
    UserId NVARCHAR(128) NOT NULL,
    RoleId NVARCHAR(128) NOT NULL,
    PRIMARY KEY (UserId, RoleId),
    CONSTRAINT FK_AspNetUserRoles_AspNetUsers FOREIGN KEY (UserId) REFERENCES dbo.AspNetUsers(Id) ON DELETE CASCADE,
    CONSTRAINT FK_AspNetUserRoles_AspNetRoles FOREIGN KEY (RoleId) REFERENCES dbo.AspNetRoles(Id) ON DELETE CASCADE
);
GO

CREATE TABLE dbo.AspNetUserClaims (
    Id         INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    UserId     NVARCHAR(128) NOT NULL,
    ClaimType  NVARCHAR(MAX) NULL,
    ClaimValue NVARCHAR(MAX) NULL,
    CONSTRAINT FK_AspNetUserClaims_AspNetUsers FOREIGN KEY (UserId) REFERENCES dbo.AspNetUsers(Id) ON DELETE CASCADE
);
GO

CREATE TABLE dbo.AspNetUserLogins (
    LoginProvider NVARCHAR(128) NOT NULL,
    ProviderKey   NVARCHAR(128) NOT NULL,
    UserId        NVARCHAR(128) NOT NULL,
    PRIMARY KEY (LoginProvider, ProviderKey, UserId),
    CONSTRAINT FK_AspNetUserLogins_AspNetUsers FOREIGN KEY (UserId) REFERENCES dbo.AspNetUsers(Id) ON DELETE CASCADE
);
GO
