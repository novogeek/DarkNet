
CREATE PROCEDURE [dbo].[uspGetAllPermissiblePosts]
(
	@loggedInUser nvarchar(20)
)
AS
BEGIN
	SELECT ConCat(U.FirstName, ' ', U.LastName) as name, P.post, C.value as privacy, P.timestamp FROM posts P 
	INNER JOIN Users U on P.username=U.Username and P.username=@loggedInUser 
	INNER JOIN Config C on P.privacy=C.code and C.category='privacy'
	UNION
	SELECT ConCat(U.FirstName, ' ', U.LastName) as name, P.post, C.value as privacy, P.timestamp FROM posts P 
	INNER JOIN Users U on P.username=U.Username
	INNER JOIN Config C on P.privacy=C.code and C.category='privacy'
	INNER JOIN friends F on P.username in (SELECT f.Username WHERE F.FriendName = @loggedInUser)
	and P.privacy = F.PrivacyLevel
	ORDER By p.timestamp DESC
END
RETURN 0