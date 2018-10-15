CREATE PROCEDURE [dbo].[uspGetFriendsList]
(
	@username nvarchar(50)
)
AS
BEGIN
	select U.FirstName, U.LastName, C.Value as 'PrivacyLevel' 
	from Users U 
		inner join Friends F on U.Username=F.FriendName
		inner join Config C on C.Code=F.PrivacyLevel
	where C.category='privacy' and F.username=@username
END
RETURN 0