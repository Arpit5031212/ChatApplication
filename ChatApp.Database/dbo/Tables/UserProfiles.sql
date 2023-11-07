CREATE TABLE [dbo].[UserProfiles] (
    [id]            INT            IDENTITY (1, 1) NOT NULL,
    [Username]      VARCHAR (50)   NOT NULL,
    [FirstName]     VARCHAR (50)   NOT NULL,
    [LastName]      VARCHAR (50)   NOT NULL,
    [Email]         NVARCHAR (50)  NOT NULL,
    [Password]      NVARCHAR (MAX) NOT NULL,
    [ImageUrl]      NVARCHAR (MAX) NULL,
    [PhoneNumber]   VARCHAR (15)   NOT NULL,
    [DateOfBirth]   DATETIME2 (7)   NULL,
    [Gender]        VARCHAR (10)   NOT NULL,
    [CreatedAt]     DATETIME2 (7)  NOT NULL,
    [CreatedBy]     INT            NOT NULL,
    [LastUpdatedAt] DATETIME2 (7)  NOT NULL,
    [LastUpdatedBy] INT            NOT NULL,
    [ImageName] NVARCHAR(MAX) NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);

