CREATE PROCEDURE [dbo].[uspGetPostsOfTargetUser]
(
	@loggedInUser nvarchar(20),
	@targetUser nvarchar(20)
)
AS
BEGIN
	SELECT P.username, P.post, P.privacy, P.timestamp FROM posts P
	INNER JOIN friends F on P.privacy=F.PrivacyLevel
	AND F.Username= @targetUser
	AND F.FriendName =@loggedInUser
	AND P.username=@targetUser
END
RETURN 0