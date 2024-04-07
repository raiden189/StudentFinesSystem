CREATE PROCEDURE [dbo].[spUser_Get]
	@Id nvarchar(128)
AS
BEGIN
	SELECT Id, FirstName, MiddleName, LastName, Gender, CreateDate
	FROM [dbo].[User] 
	WHERE Id = @Id
END