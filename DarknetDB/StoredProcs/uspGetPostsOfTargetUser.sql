CREATE PROCEDURE [dbo].[uspGetPostsOfTargetUser]
(
	@loggedInUser nvarchar(20),
	@targetUser nvarchar(20)
)
AS
BEGIN
	SELECT ConCat(U.FirstName, ' ', U.LastName) as name, P.post, C.value as privacy, P.timestamp FROM posts P
	INNER JOIN Config C on P.privacy=C.code and C.category='privacy'
	INNER JOIN friends F on P.privacy=F.PrivacyLevel
	INNER JOIN Users U on P.username=U.Username and P.username=@targetUser 
	AND F.Username= @targetUser
	AND F.FriendName =@loggedInUser
	AND P.username=@targetUser
END
RETURN 0