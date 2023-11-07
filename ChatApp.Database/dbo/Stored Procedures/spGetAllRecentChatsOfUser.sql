Create Procedure [spGetAllRecentChatsOfUser] @UserId int
AS
Begin
Select * from
    (
        Select
            Profile.FirstName,
            Profile.LastName,
            Profile.UserName,
            Chat.ChatContent,
            Chat.SenderId,
            Chat.RecieverId,
            Chat.CreatedAt,
            ROW_NUMBER() over (Partition By 
            CASE when Chat.SenderId = @UserId then Chat.RecieverId
                 else Chat.SenderId END
            Order by Chat.CreatedAt Desc) As RowNumber
        
        from
            Chats Chat
            Join UserProfiles Profile on Profile.Id = Case When Chat.SenderId = @UserId THEN Chat.RecieverId Else Chat.SenderId END
        where
            Chat.SenderId = @UserId OR Chat.RecieverId = @UserId
    ) As All_Chats
where All_Chats.RowNumber = 1
order by All_Chats.CreatedAt DESC;
End;