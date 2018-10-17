CREATE PROCEDURE [dbo].[uspGetPosts]
(
	@loggedInUser nvarchar(20),
	@targetUser nvarchar(20)
)
AS
BEGIN
	SELECT P.username, P.post, P.privacy, P.timestamp FROM posts P WHERE username=@targetUser and P.privacy = 
	(SELECT F.PrivacyLevel FROM friends F WHERE F.username=@targetUser and F.FriendName=@loggedInUser)
END
RETURN 0