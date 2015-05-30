CREATE PROCEDURE [dbo].[spOrganizationAdd]
	@parentOrgId int = NULL ,
	@name NVARCHAR(50) = NULL,
	@newId int OUTPUT
AS
BEGIN
	DECLARE @mOrgNode hierarchyid, @lc hierarchyid
	IF @parentOrgId IS NULL 
		SELECT @mOrgNode = hierarchyid::GetRoot()
	ELSE
		SELECT @mOrgNode = NodeId
		FROM Organization
		WHERE Id = @parentOrgId

   SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
   BEGIN TRANSACTION
      SELECT @lc = max(NodeId) 
      FROM Organization
      WHERE NodeId.GetAncestor(1) =@mOrgNode ;

      INSERT Organization (NodeId, Name)
      VALUES(@mOrgNode.GetDescendant(@lc, NULL), @name)
	  SELECT @newId = SCOPE_IDENTITY()
   COMMIT
END 
GO
