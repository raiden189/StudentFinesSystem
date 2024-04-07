CREATE PROCEDURE [dbo].[spFines_Delete]
	@UserId nvarchar(128),
	@FineId int,
	@Id int

AS
BEGIN
	DELETE FROM [dbo].[Fines] WHERE UserId = @UserId AND FineId = @FineId AND Id = @Id
END
