CREATE PROCEDURE [dbo].[spUser_Insert]
	@UserId nvarchar(128),
	@FirstName nvarchar(50),
	@MiddleName nvarchar(50),
	@LastName nvarchar(50),
	@Gender nvarchar(50)
AS
BEGIN
	INSERT INTO [dbo].[User] (Id, FirstName, MiddleName, LastName, Gender)
	VALUES (@UserId, @FirstName, @MiddleName, @LastName, @Gender)
END
