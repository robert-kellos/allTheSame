Feature: CommunityWorker
	In order to see a list of communityWorkers
	As a Community Administrator
	I want to load a list of communityWorkers

@CommunityWorker 
#[C]RUD - [Post] :: Create a new communityWorker, by passing a newly populated communityWorker
Scenario: Add a communityWorker
	Given the following CommunityWorker Add input
		| FirstName | LastName | Email   | MobileNumber |
		| Spec      | Flow     | x@y.com | 800-555-1212 |
	When I call the add CommunityWorker Post api endpoint to add a communityWorker
	Then the add result should be a CommunityWorker Id

@CommunityWorkers 
#C[R]UD - [Get] :: Retrieve all communityWorkers, without passing anything
Scenario: Retrieve all communityWorkers
	#Given I am an authenticated user
	When I call the CommunityWorker Get api endpoint
	Then the get result should be a list of communityWorkers

@CommunityWorker 
#C[R]UD - [Get] :: Retrieve an existing communityWorker, by passing a communityWorker Id
Scenario: Retrieve a communityWorker by Id
	Given the following CommunityWorker GetById input
		| Id |
		| 2  |
	When I call the CommunityWorker Get api endpoint by Id
	Then the get by id result should be a CommunityWorker

@CommunityWorker 
#CR[U]D - [Put] :: Update an existing communityWorker, by passing changes populated in communityWorker and its Id
Scenario: Update a communityWorker
	Given the following CommunityWorker Edit input
		| Id | FirstName | LastName | Email   | MobileNumber |
		| 2  | Spec      | Flow     | x@y.com | 800-555-1212 |
	When I call the edit CommunityWorker Put api endpoint to edit a communityWorker
	Then the edit result should be an updated CommunityWorker

@CommunityWorker 
#CRU[D] - [Post] :: Delete an existing communityWorker, by passing a populated in communityWorker object
Scenario: Delete a communityWorker
	Given the following CommunityWorker Delete input
	#use Id from recently added
		| Id | 
		| 0  | 
	When I call the delete CommunityWorker Post api endpoint to delete a communityWorker
	Then the delete result should be an deleted CommunityWorker

@CommunityWorker 
#Helper - [Get] :: Check for an existing communityWorker, by passing a communityWorker Id
Scenario: Check if a communityWorker exists
	Given the following CommunityWorker Id input
		| Id | 
		| 2  | 
	When I call the CommunityWorker Exists Get api endpoint by Id to verify if it exists
	Then the CommunityWorker exists result should be bool true or false
