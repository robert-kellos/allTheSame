Feature: Industry
	In order to see a list of industries
	As a Community Administrator
	I want to load a list of industries

@Industry 
#[C]RUD - [Post] :: Create a new industry, by passing a newly populated industry
Scenario: Add a industry
	Given the following Industry Add input
		| FirstName | LastName | Email   | MobileNumber |
		| Spec      | Flow     | x@y.com | 800-555-1212 |
	When I call the add Industry Post api endpoint to add a industry
	Then the add result should be a Industry Id

@Industrys 
#C[R]UD - [Get] :: Retrieve all industrys, without passing anything
Scenario: Retrieve all industries
	#Given I am an authenticated user
	When I call the Industry Get api endpoint
	Then the get result should be a list of industries

@Industry 
#C[R]UD - [Get] :: Retrieve an existing industry, by passing a industry Id
Scenario: Retrieve a industry by Id
	Given the following Industry GetById input
		| Id |
		| 2  |
	When I call the Industry Get api endpoint by Id
	Then the get by id result should be a Industry

@Industry 
#CR[U]D - [Put] :: Update an existing industry, by passing changes populated in industry and its Id
Scenario: Update a industry
	Given the following Industry Edit input
		| Id | FirstName | LastName | Email   | MobileNumber |
		| 2  | Spec      | Flow     | x@y.com | 800-555-1212 |
	When I call the edit Industry Put api endpoint to edit a industry
	Then the edit result should be an updated Industry

@Industry 
#CRU[D] - [Post] :: Delete an existing industry, by passing a populated in industry object
Scenario: Delete a industry
	Given the following Industry Delete input
	#use Id from recently added
		| Id | 
		| 0  | 
	When I call the delete Industry Post api endpoint to delete a industry
	Then the delete result should be an deleted Industry

@Industry 
#Helper - [Get] :: Check for an existing industry, by passing a industry Id
Scenario: Check if a industry exists
	Given the following Industry Id input
		| Id | 
		| 2  | 
	When I call the Industry Exists Get api endpoint by Id to verify if it exists
	Then the Industry exists result should be bool true or false
