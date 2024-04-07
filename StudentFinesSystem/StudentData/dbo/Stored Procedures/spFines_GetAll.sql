CREATE PROCEDURE [dbo].[spFines_GetAll]

AS
BEGIN
	SELECT UserId, FineId, CreateDate, CreatedBy
	FROM [dbo].[Fines]
END
