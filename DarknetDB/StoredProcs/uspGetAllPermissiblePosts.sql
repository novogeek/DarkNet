
CREATE PROCEDURE [dbo].[uspGetAllPermissiblePosts]
(
	@loggedInUser nvarchar(20)
)
AS
BEGIN
	SELECT P.username, P.post, P.privacy, P.timestamp FROM posts P WHERE username=@loggedInUser UNION
	SELECT P.username, P.post, P.privacy, P.timestamp FROM posts P 
	INNER JOIN friends F on P.username in (SELECT f.Username WHERE F.FriendName = @loggedInUser)
	and P.privacy = F.PrivacyLevel
	ORDER By p.timestamp DESC
END
RETURN 0