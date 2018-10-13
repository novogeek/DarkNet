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
INSERT INTO [dbo].[Users] ([Username],[Password],[FirstName],[LastName]) VALUES ('john', 'john', 'John', 'Doe')
INSERT INTO [dbo].[Users] ([Username],[Password],[FirstName],[LastName]) VALUES ('bobby', 'bobby', 'Bobby', 'Fischer')
INSERT INTO [dbo].[Users] ([Username],[Password],[FirstName],[LastName]) VALUES ('mikhail', 'mikhail', 'Mikhail', 'Tal')
INSERT INTO [dbo].[Users] ([Username],[Password],[FirstName],[LastName]) VALUES ('sherlock', 'sherlock', 'Sherlock', 'Holmes')
GO


