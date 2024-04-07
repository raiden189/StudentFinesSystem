CREATE PROCEDURE [dbo].[spFines_Update]
	@Id int = 0,
	@UserId nvarchar(128),
	@FineId int,
	@Description nvarchar(200),
	@CreatedBy nvarchar(128)
AS
BEGIN
	UPDATE [dbo].[Fines]
	SET UserId = @UserId, FineId = @FineId, CreatedBy = @CreatedBy
	WHERE Id = @Id
END
