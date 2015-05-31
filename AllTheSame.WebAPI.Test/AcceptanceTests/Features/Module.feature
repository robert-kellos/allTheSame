Feature: Module
	In order to see a list of modules
	As a Community Administrator
	I want to load a list of modules

@Module 
#[C]RUD - [Post] :: Create a new module, by passing a newly populated module
Scenario: Add a module
	Given the following Module Add input
		| FirstName | LastName | Email   | MobileNumber |
		| Spec      | Flow     | x@y.com | 800-555-1212 |
	When I call the add Module Post api endpoint to add a module
	Then the add result should be a Module Id

@Modules 
#C[R]UD - [Get] :: Retrieve all modules, without passing anything
Scenario: Retrieve all modules
	#Given I am an authenticated user
	When I call the Module Get api endpoint
	Then the get result should be a list of modules

@Module 
#C[R]UD - [Get] :: Retrieve an existing module, by passing a module Id
Scenario: Retrieve a module by Id
	Given the following Module GetById input
		| Id |
		| 2  |
	When I call the Module Get api endpoint by Id
	Then the get by id result should be a Module

@Module 
#CR[U]D - [Put] :: Update an existing module, by passing changes populated in module and its Id
Scenario: Update a module
	Given the following Module Edit input
		| Id | FirstName | LastName | Email   | MobileNumber |
		| 2  | Spec      | Flow     | x@y.com | 800-555-1212 |
	When I call the edit Module Put api endpoint to edit a module
	Then the edit result should be an updated Module

@Module 
#CRU[D] - [Post] :: Delete an existing module, by passing a populated in module object
Scenario: Delete a module
	Given the following Module Delete input
	#use Id from recently added
		| Id | 
		| 0  | 
	When I call the delete Module Post api endpoint to delete a module
	Then the delete result should be an deleted Module

@Module 
#Helper - [Get] :: Check for an existing module, by passing a module Id
Scenario: Check if a module exists
	Given the following Module Id input
		| Id | 
		| 2  | 
	When I call the Module Exists Get api endpoint by Id to verify if it exists
	Then the Module exists result should be bool true or false
