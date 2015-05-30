
/*
	Returns all Permission Records that apply for the given OrganizationID and UserId 
*/
CREATE PROCEDURE [dbo].[spGetOrgPermissions]
	@orgId int,
	@userId int
AS
	DECLARE @leafOrgNodeId hierarchyid
	SELECT @leafOrgNodeId = NodeId FROM Organization WHERE Id = @orgId

	SELECT DISTINCT PermissionId, Code, Label, IsAllowed
	INTO #Temp
		FROM (
			SELECT p.Id as PermissionId, p.Code as Code, p.Label as Label, 1 as IsAllowed
			   From Organization as org
			   JOIN User_Org_Role uor on org.Id = uor.OrgId and uor.UserId = @userId
			   JOIN Role_Permission rp on rp.RoleId = uor.RoleId	   
			   JOIN Permission p on rp.PermissionId = p.Id	   
			WHERE @leafOrgNodeId.IsDescendantOf(org.NodeId) = 1 
			UNION
			 Select p.Id as PermissionId, p.Code as Code, p.Label as Label, uop.IsAllowed as IsAllowed
			   From Organization as org
			   JOIN User_Org_Permission uop on org.Id = uop.OrgId AND uop.UserId = @userId	   
			   JOIN Permission p on uop.PermissionId = p.Id	   
			WHERE @leafOrgNodeId.IsDescendantOf(org.NodeId) = 1 
		) as t

    -- Remove all Permission ID's where there is an explicit Denial for that permission in the ancestor chain
	DELETE FROM #Temp 
	WHERE PermissionId IN (SELECT PermissionId FROM #Temp WHERE IsAllowed = 0)

	SELECT PermissionId, Code, Label FROM #Temp