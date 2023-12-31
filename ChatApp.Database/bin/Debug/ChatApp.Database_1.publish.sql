﻿/*
Deployment script for ChatApp

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "ChatApp"
:setvar DefaultFilePrefix "ChatApp"
:setvar DefaultDataPath "C:\Users\Arpit\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\"
:setvar DefaultLogPath "C:\Users\Arpit\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\"

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
/*
The column [dbo].[UserProfile].[Username] on table [dbo].[UserProfile] must be added, but the column has no default value and does not allow NULL values. If the table contains data, the ALTER script will not work. To avoid this issue you must either: add a default value to the column, mark it as allowing NULL values, or enable the generation of smart-defaults as a deployment option.
*/

IF EXISTS (select top 1 1 from [dbo].[UserProfile])
    RAISERROR (N'Rows were detected. The schema update is terminating because data loss might occur.', 16, 127) WITH NOWAIT

GO
PRINT N'Starting rebuilding table [dbo].[UserProfile]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_UserProfile] (
    [id]            INT            IDENTITY (1, 1) NOT NULL,
    [Username]      VARCHAR (50)   NOT NULL,
    [FirstName]     VARCHAR (50)   NOT NULL,
    [LastName]      VARCHAR (50)   NOT NULL,
    [Email]         NVARCHAR (50)  NOT NULL,
    [Password]      NVARCHAR (MAX) NOT NULL,
    [ImageUrl]      NVARCHAR (MAX) NULL,
    [PhoneNumber]   VARCHAR (15)   NOT NULL,
    [DateOfBirth]   DATE           NULL,
    [Gender]        VARCHAR (10)   NOT NULL,
    [CreatedAt]     DATETIME2 (7)  NOT NULL,
    [CreatedBy]     INT            NOT NULL,
    [LastUpdatedAt] DATETIME2 (7)  NOT NULL,
    [LastUpdatedBy] INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[UserProfile])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_UserProfile] ON;
        INSERT INTO [dbo].[tmp_ms_xx_UserProfile] ([id], [FirstName], [LastName], [Email], [Password], [ImageUrl], [PhoneNumber], [DateOfBirth], [Gender], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
        SELECT   [id],
                 [FirstName],
                 [LastName],
                 [Email],
                 [Password],
                 [ImageUrl],
                 [PhoneNumber],
                 [DateOfBirth],
                 [Gender],
                 [CreatedAt],
                 [CreatedBy],
                 [LastUpdatedAt],
                 [LastUpdatedBy]
        FROM     [dbo].[UserProfile]
        ORDER BY [id] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_UserProfile] OFF;
    END

DROP TABLE [dbo].[UserProfile];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_UserProfile]', N'UserProfile';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Update complete.';


GO
