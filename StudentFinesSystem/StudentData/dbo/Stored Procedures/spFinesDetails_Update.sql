CREATE PROCEDURE [dbo].[spFinesDetails_Update]
	@Id int,
	@Fine money,
	@FineName nvarchar(128),
	@FineDescription nvarchar(500)
AS
BEGIN
	UPDATE [dbo].[FinesDetails] SET Fine = @Fine, FineName = @FineName, FineDescription = @FineDescription
	WHERE Id = @Id
END
