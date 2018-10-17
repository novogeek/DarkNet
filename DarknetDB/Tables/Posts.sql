CREATE TABLE [dbo].[Posts]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [username] NVARCHAR(20) NOT NULL, 
    [post] NVARCHAR(MAX) NOT NULL, 
    [privacy] NVARCHAR(10) NOT NULL, 
    [timestamp] DATETIME NOT NULL DEFAULT getdate(), 
    CONSTRAINT [FK_Posts_Users] FOREIGN KEY ([Username]) REFERENCES [Users]([Username])    
)
