CREATE PROCEDURE [dbo].[spGetUserById]
	@id int
AS
Begin
	SELECT id, username, firstname, lastname, imagename from UserProfiles
	where @id = UserProfiles.id;
End;

