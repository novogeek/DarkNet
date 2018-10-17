/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

USE [DarknetDB]
GO
INSERT INTO [dbo].[Users] ([Username],[Password],[FirstName],[LastName], Mobile, [Address]) VALUES ('alice', 'alice', 'Alice' , 'Long', '4998887770', 'Canada')
INSERT INTO [dbo].[Users] ([Username],[Password],[FirstName],[LastName], Mobile, [Address]) VALUES ('bob', 'bob', 'Bob', 'Turner', '425666777', 'USA')
INSERT INTO [dbo].[Users] ([Username],[Password],[FirstName],[LastName], Mobile, [Address]) VALUES ('charlie', 'charlie', 'Charlie', 'Cox', '2198887767', 'Russia')
INSERT INTO [dbo].[Users] ([Username],[Password],[FirstName],[LastName], Mobile, [Address]) VALUES ('doe', 'doe', 'Doe', 'Taylor', '300310212', 'United Kingdom')
INSERT INTO [dbo].[Users] ([Username],[Password],[FirstName],[LastName], Mobile, [Address]) VALUES ('eve', 'eve', 'Eve', 'Smith', '300310212', 'Unknown')
INSERT INTO [dbo].[Users] ([Username],[Password],[FirstName],[LastName], Mobile, [Address]) VALUES ('frank', 'frank', 'Frank', 'Clark', '22335622', 'China')
INSERT INTO [dbo].[Users] ([Username],[Password],[FirstName],[LastName], Mobile, [Address]) VALUES ('groot', 'groot', 'Groot', 'Moore', '76543222', 'Tokyo')

GO

GO
INSERT INTO [dbo].[Config] (Config.category, Config.code, Config.value) VALUES ('privacy', 'acq', 'Acquaintance')
INSERT INTO [dbo].[Config] (Config.category, Config.code, Config.value) VALUES ('privacy', 'frn', 'Friends')
INSERT INTO [dbo].[Config] (Config.category, Config.code, Config.value) VALUES ('privacy', 'fam', 'Family')
GO

GO
INSERT INTO [dbo].[Friends] (Username, FriendName, PrivacyLevel) VALUES ('alice', 'bob', 'acq')
INSERT INTO [dbo].[Friends] (Username, FriendName, PrivacyLevel) VALUES ('alice', 'charlie', 'frn')
INSERT INTO [dbo].[Friends] (Username, FriendName, PrivacyLevel) VALUES ('alice', 'doe', 'fam')
INSERT INTO [dbo].[Friends] (Username, FriendName, PrivacyLevel) VALUES ('alice', 'eve', 'acq')
INSERT INTO [dbo].[Friends] (Username, FriendName, PrivacyLevel) VALUES ('alice', 'frank', 'frn')
INSERT INTO [dbo].[Friends] (Username, FriendName, PrivacyLevel) VALUES ('alice', 'groot', 'acq')

INSERT INTO [dbo].[Friends] (Username, FriendName, PrivacyLevel) VALUES ('bob', 'alice', 'frn')
INSERT INTO [dbo].[Friends] (Username, FriendName, PrivacyLevel) VALUES ('bob', 'charlie', 'acq')
INSERT INTO [dbo].[Friends] (Username, FriendName, PrivacyLevel) VALUES ('bob', 'doe', 'frn')
INSERT INTO [dbo].[Friends] (Username, FriendName, PrivacyLevel) VALUES ('bob', 'eve', 'frn')
INSERT INTO [dbo].[Friends] (Username, FriendName, PrivacyLevel) VALUES ('bob', 'frank', 'fam')
INSERT INTO [dbo].[Friends] (Username, FriendName, PrivacyLevel) VALUES ('bob', 'groot', 'fam')
GO
