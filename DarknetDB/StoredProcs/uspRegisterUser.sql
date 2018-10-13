CREATE PROCEDURE [dbo].[uspRegisterUser]
(
	@username nvarchar(20),
	@password nvarchar(50),
	@firstName nvarchar(50),
	@lastName nvarchar(50)
)
AS
BEGIN
	Declare @result VARCHAR(10)
	IF NOT EXISTS (Select 1 from Users where Users.Username=@username)
	BEGIN
		INSERT INTO Users 
		VALUES
		(
			@username, @password, @firstName, @lastName
		)
		SET @result='Success'
	END
	SELECT @result as Result
END