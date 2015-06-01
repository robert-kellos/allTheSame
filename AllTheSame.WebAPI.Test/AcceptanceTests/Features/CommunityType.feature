Feature: CommunityType
	In order to see a list of CommunityTypes
	As a CommunityType Administrator
	I want to load a list of CommunityTypes

@CommunityType 
#[C]RUD - [Post] :: Create, Check, GetById, Update and Delete CommunityType, by passing a newly populated CommunityType
Scenario: CommunityType--Add, Check, GetById, Update and Delete CommunityType
	Given the following CommunityType Add input
		| Code		  | Label	|
		| SpecFlow    | test	|
	When I call the add CommunityType Post api endpoint to add a CommunityType it checks if exists pulls item edits it and deletes it
	Then the add result should be a CommunityType Id check exists get by id edit and delete with http response returns

@CommunityTypes 
#C[R]UD - [Get] :: Retrieve all CommunityTypes, without passing anything
Scenario: CommunityType--Retrieve all CommunityTypes
	#Given I am an authenticated user
	When I call the CommunityType Get api endpoint
	Then the get result should be a list of CommunityTypes

@CommunityType 
#[C]RUD - [Post] :: Create a new CommunityType, by passing a newly populated CommunityType
Scenario: Add a CommunityType
	Given the following CommunityType Add input
		| Code			| Label		| 
		| SpecFlowTest  | FlowTest  |
	When I call the add CommunityType Post api endpoint to add a CommunityType
	Then the add result should be a CommunityType Id

#@CommunityTypes 
##C[R]UD - [Get] :: Retrieve all CommunityTypes, without passing anything
#Scenario: Retrieve all CommunityTypes
#	#Given I am an authenticated user
#	When I call the CommunityType Get api endpoint
#	Then the get result should be a list of CommunityTypes
#
#@CommunityType 
##C[R]UD - [Get] :: Retrieve an existing CommunityType, by passing a CommunityType Id
#Scenario: Retrieve a CommunityType by Id
#	Given the following CommunityType GetById input
#		| Id |
#		| 2  |
#	When I call the CommunityType Get api endpoint by Id
#	Then the get by id result should be a CommunityType
#
#@CommunityType 
##CR[U]D - [Put] :: Update an existing CommunityType, by passing changes populated in CommunityType and its Id
#Scenario: Update a CommunityType
#	Given the following CommunityType Edit input
#		| Id | FirstName | LastName | Email   | MobileNumber |
#		| 2  | Spec      | Flow     | x@y.com | 800-555-1212 |
#	When I call the edit CommunityType Put api endpoint to edit a CommunityType
#	Then the edit result should be an updated CommunityType
#
#@CommunityType 
##CRU[D] - [Post] :: Delete an existing CommunityType, by passing a populated in CommunityType object
#Scenario: Delete a CommunityType
#	Given the following CommunityType Delete input
#	#use Id from recently added
#		| Id | 
#		| 0  | 
#	When I call the delete CommunityType Post api endpoint to delete a CommunityType
#	Then the delete result should be an deleted CommunityType
#
#@CommunityType 
##Helper - [Get] :: Check for an existing CommunityType, by passing a CommunityType Id
#Scenario: Check if a CommunityType exists
#	Given the following CommunityType Id input
#		| Id | 
#		| 2  | 
#	When I call the CommunityType Exists Get api endpoint by Id to verify if it exists
#	Then the CommunityType exists result should be bool true or false
