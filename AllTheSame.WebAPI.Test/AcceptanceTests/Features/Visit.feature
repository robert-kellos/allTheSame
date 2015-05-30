Feature: Visit
	In order to see a list of visits
	As a Community Administrator
	I want to load a list of visits

@Visit 
#[C]RUD - [Post] :: Create a new visit, by passing a newly populated visit
Scenario: Add a visit
	Given the following Visit Add input
		| FirstName | LastName | Email   | MobileNumber |
		| Spec      | Flow     | x@y.com | 800-555-1212 |
	When I call the add Visit Post api endpoint to add a visit
	Then the add result should be a Visit Id

@Visits 
#C[R]UD - [Get] :: Retrieve all visits, without passing anything
Scenario: Retrieve all visits
	#Given I am an authenticated user
	When I call the Visit Get api endpoint
	Then the get result should be a list of visits

@Visit 
#C[R]UD - [Get] :: Retrieve an existing visit, by passing a visit Id
Scenario: Retreive a visit by Id
	Given the following Visit GetById input
		| Id |
		| 2  |
	When I call the Visit Get api endpoint by Id
	Then the get by id result should be a Visit

@Visit 
#CR[U]D - [Put] :: Update an existing visit, by passing changes populated in visit and its Id
Scenario: Update a visit
	Given the following Visit Edit input
		| Id | FirstName | LastName | Email   | MobileNumber |
		| 2  | Spec      | Flow     | x@y.com | 800-555-1212 |
	When I call the edit Visit Put api endpoint to edit a visit
	Then the edit result should be an updated Visit

@Visit 
#CRU[D] - [Post] :: Delete an existing visit, by passing a populated in visit object
Scenario: Delete a visit
	Given the following Visit Delete input
	#use Id from recently added
		| Id | 
		| 0  | 
	When I call the delete Visit Post api endpoint to delete a visit
	Then the delete result should be an deleted Visit

@Visit 
#Helper - [Get] :: Check for an existing visit, by passing a visit Id
Scenario: Check if a visit exists
	Given the following Visit Id input
		| Id | 
		| 2  | 
	When I call the Visit Exists Get api endpoint by Id to verify if it exists
	Then the Visit exists result should be bool true or false
