CREATE PROCEDURE [dbo].[uspRegisterUser]
(
	@username nvarchar(20),
	@password nvarchar(50),
	@firstName nvarchar(50),
	@lastName nvarchar(50),
	@mobile nvarchar(15),
	@address nvarchar(100)
)
AS
BEGIN
	Declare @result VARCHAR(10) = 'failure'
	IF NOT EXISTS (Select 1 from Users where Users.Username=@username)
	BEGIN
		INSERT INTO Users (Username, [Password], FirstName, LastName, Mobile, [Address])
		VALUES
		(
			@username, @password, @firstName, @lastName, @mobile, @address
		)
		SET @result='success'
	END
	SELECT @result as Result
END