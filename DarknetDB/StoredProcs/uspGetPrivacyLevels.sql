CREATE PROCEDURE [dbo].[uspGetPrivacyLevels]
AS
	SELECT code, value from Config where category='privacy'
RETURN 0
