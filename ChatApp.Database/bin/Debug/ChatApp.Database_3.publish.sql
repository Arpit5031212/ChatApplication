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
PRINT N'Creating Table [dbo].[UserProfiles]...';


GO
CREATE TABLE [dbo].[UserProfiles] (
    [id]            INT            IDENTITY (1, 1) NOT NULL,
    [Username]      VARCHAR (50)   NOT NULL,
    [FirstName]     VARCHAR (50)   NOT NULL,
    [LastName]      VARCHAR (50)   NOT NULL,
    [Email]         NVARCHAR (50)  NOT NULL,
    [Password]      NVARCHAR (MAX) NOT NULL,
    [ImageUrl]      NVARCHAR (MAX) NULL,
    [PhoneNumber]   VARCHAR (15)   NOT NULL,
    [DateOfBirth]   DATETIME2 (7)  NULL,
    [Gender]        VARCHAR (10)   NOT NULL,
    [CreatedAt]     DATETIME2 (7)  NOT NULL,
    [CreatedBy]     INT            NOT NULL,
    [LastUpdatedAt] DATETIME2 (7)  NOT NULL,
    [LastUpdatedBy] INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);


GO
PRINT N'Update complete.';


GO