/*
	Outputs 1 if the user has permission for the given org and permission code
	OUtputs 0 if the is no joining data to the permission or if user was explicitly denined that permission
*/
CREATE PROCEDURE [dbo].[spHasPermission]
	@orgId int,
	@userId int,
	@permissionCode char(50),
	@hasPermission bit OUTPUT
AS
	DECLARE @leafOrgNodeId hierarchyid
	SELECT @leafOrgNodeId = NodeId FROM Organization WHERE Id = @orgId

	SELECT TOP 1 @hasPermission = IsAllowed
	FROM (
		SELECT 1 as IsAllowed, org.Level as OrgLevel
			From Organization as org
			JOIN User_Org_Role uor on org.Id = uor.OrgId and uor.UserId = @userId
			JOIN Role_Permission rp on rp.RoleId = uor.RoleId	   
			JOIN Permission p on rp.PermissionId = p.Id	   
		WHERE @leafOrgNodeId.IsDescendantOf(org.NodeId) = 1 AND p.Code = @permissionCode
		UNION
			Select uop.IsAllowed as IsAllowed, org.Level as OrgLevel
			From Organization as org
			JOIN User_Org_Permission uop on org.Id = uop.OrgId AND uop.UserId = @userId	   
			JOIN Permission p on uop.PermissionId = p.Id	   
		WHERE @leafOrgNodeId.IsDescendantOf(org.NodeId) = 1 AND p.Code = @permissionCode
	) as t
	ORDER By OrgLevel Desc

	-- If not record found has permission is false
	IF @hasPermission IS NULL SELECT @hasPermission = 0	

RETURN @hasPermission -- Set return as well to give developer the choice
