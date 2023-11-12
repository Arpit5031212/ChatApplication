CREATE TABLE [dbo].[Chats]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[ChatType] int NULL,
	[ChatContent] varchar(MAX) Not null,
	[SenderId] int NOT NULL,
	[RecieverId] int NOT NULL,
	[RepliedTo] int NULL,
	[CreatedAt] DateTime2(7) NOT NULL,
	[UpdatedAt] DateTime2(7) NOT NULL,
	[IsDeletedBySender] bit NOT NULL Default 0,
	[IsDeletedByReciever] bit NOT NULL Default 0

	CONSTRAINT FK_SenderChats FOREIGN KEY (SenderId) REFERENCES [dbo].[UserProfiles](Id),
	CONSTRAINT FK_RecieverChats FOREIGN KEY (RecieverId) REFERENCES [dbo].[UserProfiles](Id),
	CONSTRAINT FK_Replies FOREIGN KEY (RepliedTo) REFERENCES [dbo].[Chats](Id),
)
