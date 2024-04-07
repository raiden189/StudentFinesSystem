CREATE PROCEDURE [dbo].[spUser_GetAll]
AS
BEGIN
	SELECT Id AS UserId, FirstName, MiddleName, LastName, Gender
	FROM [dbo].[User]
END