
Create Procedure spSearchUsers @search nvarchar(20), @currentUser int
As
Begin
Select id, username, firstname, lastname, imagename from UserProfiles
WHERE id != @currentUser 
    AND (username LIKE '%' + @search + '%' 
    OR FirstName LIKE '%' + @search + '%' 
    OR LastName LIKE '%' + @search + '%')
End;