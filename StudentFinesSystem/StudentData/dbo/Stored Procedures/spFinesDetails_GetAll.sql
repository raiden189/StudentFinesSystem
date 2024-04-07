CREATE PROCEDURE [dbo].[spFinesDetails_GetAll]
AS
BEGIN
	SELECT Id, Fine, FineName, FineDescription, IsActive
	FROM [dbo].[FinesDetails]
END