Feature: Organization
	In order to see a list of organizations
	As a Community Administrator
	I want to load a list of organizations

#@Organization 
##[C]RUD - [Post] :: Create a new organization, by passing a newly populated organization
#Scenario: Add a organization
#	Given the following Organization Add input
#		| Code | Label | Email   | MobileNumber |
#		| Spec      | Flow     | x@y.com | 800-555-1212 |
#	When I call the add Organization Post api endpoint to add a organization
#	Then the add result should be a Organization Id

@Organizations 
#C[R]UD - [Get] :: Retrieve all organizations, without passing anything
Scenario: Retrieve all organizations
	#Given I am an authenticated user
	When I call the Organization Get api endpoint
	Then the get result should be a list of organizations

@Organization 
#C[R]UD - [Get] :: Retrieve an existing organization, by passing a organization Id
Scenario: Retrieve a organization by Id
	Given the following Organization GetById input
		| Id |
		| 5  |
	When I call the Organization Get api endpoint by Id
	Then the get by id result should be a Organization

@Organization 
#CR[U]D - [Put] :: Update an existing organization, by passing changes populated in organization and its Id
Scenario: Update a organization
	Given the following Organization Edit input
		| Id | Level |
		| 12  | 3	 |
	When I call the edit Organization Put api endpoint to edit a organization
	Then the edit result should be an updated Organization

@Organization 
#CRU[D] - [Post] :: Delete an existing organization, by passing a populated in organization object
Scenario: Delete a organization
	Given the following Organization Delete input
	#use Id from recently added
		| Id | 
		| 0  | 
	When I call the delete Organization Post api endpoint to delete a organization
	Then the delete result should be an deleted Organization

@Organization 
#Helper - [Get] :: Check for an existing organization, by passing a organization Id
Scenario: Check if a organization exists
	Given the following Organization Id input
		| Id | 
		| 12  | 
	When I call the Organization Exists Get api endpoint by Id to verify if it exists
	Then the Organization exists result should be bool true or false
