CREATE PROCEDURE [dbo].[spFines_Insert]
	@UserId nvarchar(128),
	@FineId int,
	@CreatedBy nvarchar(128)

AS
BEGIN
	INSERT INTO [dbo].[Fines] (UserId, FineId, CreatedBy)
	VALUES (@UserId, @FineId, @CreatedBy)
END