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
INSERT INTO [dbo].[Config] (Config.category, Config.code, Config.value) VALUES ('privacy', 'pub', 'Public')
INSERT INTO [dbo].[Config] (Config.category, Config.code, Config.value) VALUES ('privacy', 'frn', 'Friends')
INSERT INTO [dbo].[Config] (Config.category, Config.code, Config.value) VALUES ('privacy', 'fam', 'Family')
INSERT INTO [dbo].[Config] (Config.category, Config.code, Config.value) VALUES ('privacy', 'acq', 'Acquaintance')
GO

GO
INSERT INTO [dbo].[Friends] (Username, FriendName, PrivacyLevel) VALUES ('alice', 'bob', 'acq')
INSERT INTO [dbo].[Friends] (Username, FriendName, PrivacyLevel) VALUES ('alice', 'charlie', 'frn')
INSERT INTO [dbo].[Friends] (Username, FriendName, PrivacyLevel) VALUES ('alice', 'doe', 'fam')
INSERT INTO [dbo].[Friends] (Username, FriendName, PrivacyLevel) VALUES ('alice', 'frank', 'frn')
INSERT INTO [dbo].[Friends] (Username, FriendName, PrivacyLevel) VALUES ('alice', 'groot', 'acq')

INSERT INTO [dbo].[Friends] (Username, FriendName, PrivacyLevel) VALUES ('bob', 'alice', 'frn')
INSERT INTO [dbo].[Friends] (Username, FriendName, PrivacyLevel) VALUES ('bob', 'charlie', 'acq')
INSERT INTO [dbo].[Friends] (Username, FriendName, PrivacyLevel) VALUES ('bob', 'doe', 'frn')
INSERT INTO [dbo].[Friends] (Username, FriendName, PrivacyLevel) VALUES ('bob', 'eve', 'frn')
INSERT INTO [dbo].[Friends] (Username, FriendName, PrivacyLevel) VALUES ('bob', 'frank', 'fam')
INSERT INTO [dbo].[Friends] (Username, FriendName, PrivacyLevel) VALUES ('bob', 'groot', 'fam')
GO

GO
INSERT INTO [dbo].[Posts] (posts.username, posts.post, posts.privacy, posts.timestamp) VALUES ('alice', 'Writing a list of random sentences is harder than I initially thought it would be', 'acq', '2018-08-12 20:17')
INSERT INTO [dbo].[Posts] (posts.username, posts.post, posts.privacy, posts.timestamp) VALUES ('alice', 'I think I will buy the red car, or I will lease the blue one.', 'frn', '2018-07-17 07:15')
INSERT INTO [dbo].[Posts] (posts.username, posts.post, posts.privacy, posts.timestamp) VALUES ('alice', 'Should we start class now, or should we wait for everyone to get here?', 'fam', '2018-07-15 20:15:31.840')
INSERT INTO [dbo].[Posts] (posts.username, posts.post, posts.privacy, posts.timestamp) VALUES ('alice', 'I really want to go to work, but I am too sick to drive.', 'acq', '2018-06-14 20:15:31.840')

INSERT INTO [dbo].[Posts] (posts.username, posts.post, posts.privacy, posts.timestamp) VALUES ('bob', 'I currently have 4 windows open up… and I don’t know why.', 'acq', '2018-05-12 20:15:31.840')
INSERT INTO [dbo].[Posts] (posts.username, posts.post, posts.privacy, posts.timestamp) VALUES ('bob', 'Italy is my favorite country; in fact, I plan to spend two weeks there next year.', 'frn', '2018-08-12 20:15:31.840')
INSERT INTO [dbo].[Posts] (posts.username, posts.post, posts.privacy, posts.timestamp) VALUES ('bob', 'The sky is clear; the stars are twinkling.', 'fam', '2018-10-17 20:15:31.840')
INSERT INTO [dbo].[Posts] (posts.username, posts.post, posts.privacy, posts.timestamp) VALUES ('bob', 'Malls are great places to shop; I can find everything I need under one roof.', 'frn', '2018-04-13 20:15:31.840')

INSERT INTO [dbo].[Posts] (posts.username, posts.post, posts.privacy, posts.timestamp) VALUES ('eve', 'I am happy to take your donation; any amount will be greatly appreciated.', 'frn', '2018-6-14 20:15:31.840')
INSERT INTO [dbo].[Posts] (posts.username, posts.post, posts.privacy, posts.timestamp) VALUES ('eve', 'Christmas is coming!', 'frn', '2018-07-07 20:15:31.840')
INSERT INTO [dbo].[Posts] (posts.username, posts.post, posts.privacy, posts.timestamp) VALUES ('eve', 'Abstraction is often one floor above you.', 'frn', '2018-05-04 20:15:31.840')
INSERT INTO [dbo].[Posts] (posts.username, posts.post, posts.privacy, posts.timestamp) VALUES ('eve', 'Tom got a small piece of pie.', 'frn', '2018-04-03 20:17')

GO