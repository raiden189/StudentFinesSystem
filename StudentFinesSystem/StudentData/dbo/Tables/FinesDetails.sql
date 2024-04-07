CREATE TABLE [dbo].[FinesDetails]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
    [Fine] money NOT NULL DEFAULT ((0)),
    [FineName] NVARCHAR(128) NOT NULL, 
    [FineDescription] NVARCHAR(500) NOT NULL,
    [CreateDate] datetime2(7) NOT NULL DEFAULT (getutcdate()),
    [IsActive] BIT NOT NULL DEFAULT 1,
)
