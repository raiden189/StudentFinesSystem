CREATE TABLE [dbo].[Fines]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
    [UserId] NVARCHAR(128) NOT NULL,
    [FineId] INT NOT NULL DEFAULT 0, 
    [CreateDate] DATETIME2 NOT NULL DEFAULT (getutcdate()), 
    [LastModified] DATETIME2 NOT NULL DEFAULT (getutcdate()), 
    [CreatedBy] NVARCHAR(128) NOT NULL
)
