CREATE PROCEDURE [dbo].[uspAuthenticateUser]
	@username nvarchar(20),
	@password nvarchar(50)
AS
BEGIN
	Declare @result varchar(10) = 'failure'
	IF EXISTS (SELECT 1 FROM dbo.Users where Users.Username=@username and Users.Password=@password)
	BEGIN
		SET @result='success'
	END
	SELECT @result as result
END
RETURN 0
