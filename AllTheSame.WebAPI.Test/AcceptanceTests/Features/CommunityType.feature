Feature: CommunityType
	In order to see a list of communityTypes
	As a Community Administrator
	I want to load a list of communityTypes

@CommunityType 
#[C]RUD - [Post] :: Create a new communityType, by passing a newly populated communityType
Scenario: Add a communityType
	Given the following CommunityType Add input
		| FirstName | LastName | Email   | MobileNumber |
		| Spec      | Flow     | x@y.com | 800-555-1212 |
	When I call the add CommunityType Post api endpoint to add a communityType
	Then the add result should be a CommunityType Id

@CommunityTypes 
#C[R]UD - [Get] :: Retrieve all communityTypes, without passing anything
Scenario: Retrieve all communityTypes
	#Given I am an authenticated user
	When I call the CommunityType Get api endpoint
	Then the get result should be a list of communityTypes

@CommunityType 
#C[R]UD - [Get] :: Retrieve an existing communityType, by passing a communityType Id
Scenario: Retreive a communityType by Id
	Given the following CommunityType GetById input
		| Id |
		| 2  |
	When I call the CommunityType Get api endpoint by Id
	Then the get by id result should be a CommunityType

@CommunityType 
#CR[U]D - [Put] :: Update an existing communityType, by passing changes populated in communityType and its Id
Scenario: Update a communityType
	Given the following CommunityType Edit input
		| Id | FirstName | LastName | Email   | MobileNumber |
		| 2  | Spec      | Flow     | x@y.com | 800-555-1212 |
	When I call the edit CommunityType Put api endpoint to edit a communityType
	Then the edit result should be an updated CommunityType

@CommunityType 
#CRU[D] - [Post] :: Delete an existing communityType, by passing a populated in communityType object
Scenario: Delete a communityType
	Given the following CommunityType Delete input
	#use Id from recently added
		| Id | 
		| 0  | 
	When I call the delete CommunityType Post api endpoint to delete a communityType
	Then the delete result should be an deleted CommunityType

@CommunityType 
#Helper - [Get] :: Check for an existing communityType, by passing a communityType Id
Scenario: Check if a communityType exists
	Given the following CommunityType Id input
		| Id | 
		| 2  | 
	When I call the CommunityType Exists Get api endpoint by Id to verify if it exists
	Then the CommunityType exists result should be bool true or false
