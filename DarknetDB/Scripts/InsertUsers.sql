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
INSERT INTO [dbo].[Users] ([Username],[Password],[FirstName],[LastName], Mobile, [Address]) VALUES ('john', 'john', 'John', 'Doe', '4998887770', 'Canada')
INSERT INTO [dbo].[Users] ([Username],[Password],[FirstName],[LastName], Mobile, [Address]) VALUES ('bobby', 'bobby', 'Bobby', 'Fischer', '425666777', 'USA')
INSERT INTO [dbo].[Users] ([Username],[Password],[FirstName],[LastName], Mobile, [Address]) VALUES ('mikhail', 'mikhail', 'Mikhail', 'Tal', '2198887767', 'Russia')
INSERT INTO [dbo].[Users] ([Username],[Password],[FirstName],[LastName], Mobile, [Address]) VALUES ('sherlock', 'sherlock', 'Sherlock', 'Holmes', '300310212', 'UK')
GO


