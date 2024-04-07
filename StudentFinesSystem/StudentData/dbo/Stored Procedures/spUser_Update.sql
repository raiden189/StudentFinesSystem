CREATE PROCEDURE [dbo].[spUser_Update]
	@UserId nvarchar(128),
	@FirstName nvarchar(50),
	@MiddleName nvarchar(50),
	@LastName nvarchar(50),
	@Gender nvarchar(50)
AS
BEGIN
	UPDATE [dbo].[User] SET FirstName = @FirstName, MiddleName = @MiddleName, LastName = @LastName, Gender = @Gender
	WHERE Id = @UserId
END
