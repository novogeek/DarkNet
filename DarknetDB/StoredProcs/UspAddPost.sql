CREATE PROCEDURE [dbo].[UspAddPost]
(
	@username nvarchar(20),
	@privacy nvarchar(10),
	@post nvarchar(MAX)
)
AS
BEGIN
	Declare @result VARCHAR(10) = 'failure'
	BEGIN
		INSERT INTO Posts ([username], [post], [privacy])
		VALUES
		(
			@username, @privacy, @post
		)
		SET @result='success'
	END
	SELECT @result as Result
END