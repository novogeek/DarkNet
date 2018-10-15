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

	SELECT U.Username, U.FirstName, U.LastName, C.Value as 'PrivacyLevel' 
	FROM Users U 
		inner join Friends F on U.Username=F.FriendName
		inner join Config C on C.Code=F.PrivacyLevel
	WHERE C.category='privacy' and F.username=@username
END
RETURN 0