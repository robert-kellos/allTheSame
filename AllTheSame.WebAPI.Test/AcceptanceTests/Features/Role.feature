Feature: Role
	In order to see a list of roles
	As a Community Administrator
	I want to load a list of roles

@Role 
#[C]RUD - [Post] :: Create a new role, by passing a newly populated role
Scenario: Add a role
	Given the following Role Add input
		| FirstName | LastName | Email   | MobileNumber |
		| Spec      | Flow     | x@y.com | 800-555-1212 |
	When I call the add Role Post api endpoint to add a role
	Then the add result should be a Role Id

@Roles 
#C[R]UD - [Get] :: Retrieve all roles, without passing anything
Scenario: Retrieve all roles
	#Given I am an authenticated user
	When I call the Role Get api endpoint
	Then the get result should be a list of roles

@Role 
#C[R]UD - [Get] :: Retrieve an existing role, by passing a role Id
Scenario: Retrieve a role by Id
	Given the following Role GetById input
		| Id |
		| 2  |
	When I call the Role Get api endpoint by Id
	Then the get by id result should be a Role

@Role 
#CR[U]D - [Put] :: Update an existing role, by passing changes populated in role and its Id
Scenario: Update a role
	Given the following Role Edit input
		| Id | FirstName | LastName | Email   | MobileNumber |
		| 2  | Spec      | Flow     | x@y.com | 800-555-1212 |
	When I call the edit Role Put api endpoint to edit a role
	Then the edit result should be an updated Role

@Role 
#CRU[D] - [Post] :: Delete an existing role, by passing a populated in role object
Scenario: Delete a role
	Given the following Role Delete input
	#use Id from recently added
		| Id | 
		| 0  | 
	When I call the delete Role Post api endpoint to delete a role
	Then the delete result should be an deleted Role

@Role 
#Helper - [Get] :: Check for an existing role, by passing a role Id
Scenario: Check if a role exists
	Given the following Role Id input
		| Id | 
		| 2  | 
	When I call the Role Exists Get api endpoint by Id to verify if it exists
	Then the Role exists result should be bool true or false
