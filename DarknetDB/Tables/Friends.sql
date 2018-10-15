CREATE TABLE [dbo].[Friends]
(
	[Id] INT NOT NULL IDENTITY PRIMARY KEY, 
    [Username] NVARCHAR(20) NOT NULL, 
    [FriendName] NVARCHAR(20) NOT NULL, 
    [PrivacyLevel] NVARCHAR(10) NOT NULL, 
    CONSTRAINT [FK_Friends_FnUn] FOREIGN KEY ([FriendName]) REFERENCES [Users]([Username]), 
    CONSTRAINT [FK_Friends_UnUn] FOREIGN KEY ([Username]) REFERENCES [Users]([Username]), 
    CONSTRAINT [FK_Friends_Config] FOREIGN KEY ([PrivacyLevel]) REFERENCES [Config]([Code])
)
