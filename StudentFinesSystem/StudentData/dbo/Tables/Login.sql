﻿CREATE TABLE [dbo].[Login]
(
	[LoginId] INT NOT NULL PRIMARY KEY, 
    [Username] NVARCHAR(50) NOT NULL, 
    [Password] NVARCHAR(50) NOT NULL, 
    [Role] NVARCHAR(50) NOT NULL
)
