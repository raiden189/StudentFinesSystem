CREATE PROCEDURE [dbo].[spFines_Get]
	@UserId nvarchar(128)
AS
BEGIN
	SELECT Id, UserId, FineId, CreateDate, CreatedBy
	FROM [dbo].[Fines]
	WHERE UserId = @UserId
END
