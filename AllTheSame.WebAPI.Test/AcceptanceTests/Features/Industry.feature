Feature: Industry
	In order to see a list of Industries
	As a Community Administrator
	I want to load a list of Industries

@Industry 
#[C]RUD - [Post] :: Create, Check, GetById, Update and Delete Industry, by passing a newly populated Industry
Scenario: Industry--Add, Check, GetById, Update and Delete Industry
	Given the following Industry Add input
		| Code		  | Label	|
		| SpecFlow    | test	|
	When I call the add Industry Post api endpoint to add a Industry it checks if exists pulls item edits it and deletes it
	Then the add result should be a Industry Id check exists get by id edit and delete with http response returns

@Industries 
#C[R]UD - [Get] :: Retrieve all Industries, without passing anything
Scenario: Industry--Retrieve all Industries
	#Given I am an authenticated user
	When I call the Industry Get api endpoint
	Then the get result should be a list of Industries

@Industry 
#[C]RUD - [Post] :: Create a new Industry, by passing a newly populated Industry
Scenario: Add a Industry
	Given the following Industry Add input
		| Code			| Label		| 
		| SpecFlowTest  | FlowTest  |
	When I call the add Industry Post api endpoint to add a Industry
	Then the add result should be a Industry Id

#@Industry 
##[C]RUD - [Post] :: Create a new industry, by passing a newly populated industry
#Scenario: Add a industry
#	Given the following Industry Add input
#		| FirstName | LastName | Email   | MobileNumber |
#		| Spec      | Flow     | x@y.com | 800-555-1212 |
#	When I call the add Industry Post api endpoint to add a industry
#	Then the add result should be a Industry Id
#
#@Industrys 
##C[R]UD - [Get] :: Retrieve all industrys, without passing anything
#Scenario: Retrieve all industries
#	#Given I am an authenticated user
#	When I call the Industry Get api endpoint
#	Then the get result should be a list of industries
#
#@Industry 
##C[R]UD - [Get] :: Retrieve an existing industry, by passing a industry Id
#Scenario: Retrieve a industry by Id
#	Given the following Industry GetById input
#		| Id |
#		| 2  |
#	When I call the Industry Get api endpoint by Id
#	Then the get by id result should be a Industry
#
#@Industry 
##CR[U]D - [Put] :: Update an existing industry, by passing changes populated in industry and its Id
#Scenario: Update a industry
#	Given the following Industry Edit input
#		| Id | FirstName | LastName | Email   | MobileNumber |
#		| 2  | Spec      | Flow     | x@y.com | 800-555-1212 |
#	When I call the edit Industry Put api endpoint to edit a industry
#	Then the edit result should be an updated Industry
#
#@Industry 
##CRU[D] - [Post] :: Delete an existing industry, by passing a populated in industry object
#Scenario: Delete a industry
#	Given the following Industry Delete input
#	#use Id from recently added
#		| Id | 
#		| 0  | 
#	When I call the delete Industry Post api endpoint to delete a industry
#	Then the delete result should be an deleted Industry
#
#@Industry 
##Helper - [Get] :: Check for an existing industry, by passing a industry Id
#Scenario: Check if a industry exists
#	Given the following Industry Id input
#		| Id | 
#		| 2  | 
#	When I call the Industry Exists Get api endpoint by Id to verify if it exists
#	Then the Industry exists result should be bool true or false
