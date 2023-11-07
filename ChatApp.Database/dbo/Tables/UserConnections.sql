CREATE TABLE [dbo].[UserConnections]
(
	[UserId] INT NOT NULL,
	[ConnectionId] nvarchar(128) NOT NULL,
	CONSTRAINT PK_UserConnections PRIMARY KEY(UserId, ConnectionId),
	CONSTRAINT FK_Users_UserConnections FOREIGN KEY (UserId) REFERENCES UserProfiles(id)
)
