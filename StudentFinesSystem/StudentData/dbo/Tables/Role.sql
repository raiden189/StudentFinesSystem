CREATE TABLE [dbo].[Role]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [RoleKey] NVARCHAR(50) NOT NULL, 
    [RoleValue] NVARCHAR(50) NOT NULL, 
    [CreateDate] DATETIME2 NOT NULL DEFAULT getutcdate(),
    [LastModified] DATETIME2 NOT NULL DEFAULT getutcdate()
)
