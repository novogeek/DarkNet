CREATE PROCEDURE [dbo].[uspGetUserDetails]
(
	@username nvarchar(50)
)
AS
BEGIN
	SELECT 
		Users.FirstName, Users.LastName, Users.Mobile, Users.Address
	FROM Users
	WHERE
		Users.Username = @username
END
RETURN 0