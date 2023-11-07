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
PRINT N'Creating Table [dbo].[UserConnections]...';


GO
CREATE TABLE [dbo].[UserConnections] (
    [UserId]       INT            NOT NULL,
    [ConnectionId] NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_UserConnections] PRIMARY KEY CLUSTERED ([UserId] ASC, [ConnectionId] ASC)
);


GO
PRINT N'Creating Foreign Key [dbo].[FK_Users_UserConnections]...';


GO
ALTER TABLE [dbo].[UserConnections] WITH NOCHECK
    ADD CONSTRAINT [FK_Users_UserConnections] FOREIGN KEY ([UserId]) REFERENCES [dbo].[UserProfiles] ([id]);


GO
PRINT N'Checking existing data against newly created constraints';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[UserConnections] WITH CHECK CHECK CONSTRAINT [FK_Users_UserConnections];


GO
PRINT N'Update complete.';


GO
