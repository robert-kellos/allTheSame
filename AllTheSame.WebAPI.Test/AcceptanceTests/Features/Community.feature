Feature: Community
	In order to see a list of communities
	As a Community Administrator
	I want to load a list of communities

@Community 
#[C]RUD - [Post] :: Create a new community, by passing a newly populated community
Scenario: Add a community
	Given the following Community Add input
		| FirstName | LastName | Email   | MobileNumber |
		| Spec      | Flow     | x@y.com | 800-555-1212 |
	When I call the add Community Post api endpoint to add a community
	Then the add result should be a Community Id

@Communitys 
#C[R]UD - [Get] :: Retrieve all communitys, without passing anything
Scenario: Retrieve all communities
	#Given I am an authenticated user
	When I call the Community Get api endpoint
	Then the get result should be a list of communities

@Community 
#C[R]UD - [Get] :: Retrieve an existing community, by passing a community Id
Scenario: Retreive a community by Id
	Given the following Community GetById input
		| Id |
		| 2  |
	When I call the Community Get api endpoint by Id
	Then the get by id result should be a Community

@Community 
#CR[U]D - [Put] :: Update an existing community, by passing changes populated in community and its Id
Scenario: Update a community
	Given the following Community Edit input
		| Id | FirstName | LastName | Email   | MobileNumber |
		| 2  | Spec      | Flow     | x@y.com | 800-555-1212 |
	When I call the edit Community Put api endpoint to edit a community
	Then the edit result should be an updated Community

@Community 
#CRU[D] - [Post] :: Delete an existing community, by passing a populated in community object
Scenario: Delete a community
	Given the following Community Delete input
	#use Id from recently added
		| Id | 
		| 0  | 
	When I call the delete Community Post api endpoint to delete a community
	Then the delete result should be an deleted Community

@Community 
#Helper - [Get] :: Check for an existing community, by passing a community Id
Scenario: Check if a community exists
	Given the following Community Id input
		| Id | 
		| 2  | 
	When I call the Community Exists Get api endpoint by Id to verify if it exists
	Then the Community exists result should be bool true or false
