CREATE PROCEDURE [dbo].[spFinesDetails_Insert]
	@Fine money,
	@FineName nvarchar(128),
	@FineDescription nvarchar(500)
AS
BEGIN
	INSERT INTO [dbo].[FinesDetails] (Fine, FineName, FineDescription)
	VALUES (@Fine, @FineName, @FineDescription)
END
