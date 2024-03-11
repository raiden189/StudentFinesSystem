CREATE TABLE [dbo].[Fines]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Fine] MONEY NOT NULL DEFAULT 0, 
    [Description] NVARCHAR(200) NOT NULL, 
    [CreateDate] DATETIME2 NOT NULL DEFAULT getutcdate(), 
    [LastModified] DATETIME2 NOT NULL DEFAULT getutcdate(), 
    [CreatedBy] NVARCHAR(50) NOT NULL
)
