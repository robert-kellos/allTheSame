Feature: Visitor
	In order to see a list of visitors
	As a Community Administrator
	I want to load a list of visitors

@Visitor 
#[C]RUD - [Post] :: Create a new visitor, by passing a newly populated visitor
Scenario: Add a visitor
	Given the following Visitor Add input
		| FirstName | LastName | Email   | MobileNumber |
		| Spec      | Flow     | x@y.com | 800-555-1212 |
	When I call the add Visitor Post api endpoint to add a visitor
	Then the add result should be a Visitor Id

@Visitors 
#C[R]UD - [Get] :: Retrieve all visitors, without passing anything
Scenario: Retrieve all visitors
	#Given I am an authenticated user
	When I call the Visitor Get api endpoint
	Then the get result should be a list of visitors

@Visitor 
#C[R]UD - [Get] :: Retrieve an existing visitor, by passing a visitor Id
Scenario: Retrieve a visitor by Id
	Given the following Visitor GetById input
		| Id |
		| 2  |
	When I call the Visitor Get api endpoint by Id
	Then the get by id result should be a Visitor

@Visitor 
#CR[U]D - [Put] :: Update an existing visitor, by passing changes populated in visitor and its Id
Scenario: Update a visitor
	Given the following Visitor Edit input
		| Id | FirstName | LastName | Email   | MobileNumber |
		| 2  | Spec      | Flow     | x@y.com | 800-555-1212 |
	When I call the edit Visitor Put api endpoint to edit a visitor
	Then the edit result should be an updated Visitor

@Visitor 
#CRU[D] - [Post] :: Delete an existing visitor, by passing a populated in visitor object
Scenario: Delete a visitor
	Given the following Visitor Delete input
	#use Id from recently added
		| Id | 
		| 0  | 
	When I call the delete Visitor Post api endpoint to delete a visitor
	Then the delete result should be an deleted Visitor

@Visitor 
#Helper - [Get] :: Check for an existing visitor, by passing a visitor Id
Scenario: Check if a visitor exists
	Given the following Visitor Id input
		| Id | 
		| 2  | 
	When I call the Visitor Exists Get api endpoint by Id to verify if it exists
	Then the Visitor exists result should be bool true or false
