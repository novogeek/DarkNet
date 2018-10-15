CREATE TABLE [dbo].[Config]
(
	[Id] INT NOT NULL IDENTITY PRIMARY KEY, 
    [category] NVARCHAR(20) NOT NULL, 
    [code] NVARCHAR(10) NOT NULL, 
    [value] NVARCHAR(20) NOT NULL, 
    CONSTRAINT [AK_Config_code] UNIQUE ([Code])
)
