CREATE PROCEDURE spOrganizationMoveSubTree(@oldParentId int, @newParentId int )
AS
BEGIN
DECLARE @nold hierarchyid, @nnew hierarchyid
SELECT @nold = NodeId FROM Organization WHERE Id = @oldParentId ;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
BEGIN TRANSACTION
SELECT @nnew = NodeId FROM Organization WHERE Id = @newParentId ;

SELECT @nnew = @nnew.GetDescendant(max(NodeId), NULL) 
FROM Organization WHERE NodeId.GetAncestor(1)=@nnew ;

UPDATE Organization 
SET NodeId = NodeId.GetReparentedValue(@nold, @nnew)
WHERE NodeId.IsDescendantOf(@nold) = 1 ;

COMMIT TRANSACTION
END ;
GO