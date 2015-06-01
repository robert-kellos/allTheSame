Feature: AlertType
	In order to see a list of AlertTypes
	As a Community Administrator
	I want to load a list of AlertTypes

@AlertType 
#[C]RUD - [Post] :: Create, Check, GetById, Update and Delete AlertType, by passing a newly populated AlertType
Scenario: AlertType--Add, Check, GetById, Update and Delete AlertType
	Given the following AlertType Add input
		| Code		  | FormatText	|
		| SpecFlow    | test		|
	When I call the add AlertType Post api endpoint to add a AlertType it checks if exists pulls item edits it and deletes it
	Then the add result should be a AlertType Id check exists get by id edit and delete with http response returns

@AlertTypes 
#C[R]UD - [Get] :: Retrieve all AlertTypes, without passing anything
Scenario: AlertType--Retrieve all AlertTypes
	#Given I am an authenticated user
	When I call the AlertType Get api endpoint
	Then the get result should be a list of AlertTypes

@AlertType 
#[C]RUD - [Post] :: Create a new alertType, by passing a newly populated alertType
Scenario: Add a AlertType
	Given the following AlertType Add input
		| Code			| FormatText |
		| SpecFlow      | test	 |
	When I call the add AlertType Post api endpoint to add a AlertType
	Then the add result should be a AlertType Id

#@AlertTypes 
##C[R]UD - [Get] :: Retrieve all alertTypes, without passing anything
#Scenario: Retrieve all alertTypes
#	#Given I am an authenticated user
#	When I call the AlertType Get api endpoint
#	Then the get result should be a list of alertTypes
#
#@AlertType 
##C[R]UD - [Get] :: Retrieve an existing alertType, by passing a alertType Id
#Scenario: Retrieve a alertType by Id
#	Given the following AlertType GetById input
#		| Id |
#		| 3  |
#	When I call the AlertType Get api endpoint by Id
#	Then the get by id result should be a AlertType
#
#@AlertType 
##CR[U]D - [Put] :: Update an existing alertType, by passing changes populated in alertType and its Id
#Scenario: Update a alertType
#	Given the following AlertType Edit input
#		| Id | Code				| FormatText |
#		| 3  | SpecFlow_Update  | test	 |
#	When I call the edit AlertType Put api endpoint to edit a alertType
#	Then the edit result should be an updated AlertType
#
#@AlertType 
##CRU[D] - [Post] :: Delete an existing alertType, by passing a populated in alertType object
#Scenario: Delete a alertType
#	Given the following AlertType Delete input
#	#use Id from recently added
#		| Id | 
#		| 0  | 
#	When I call the delete AlertType Post api endpoint to delete a alertType
#	Then the delete result should be an deleted AlertType
#
#@AlertType 
##Helper - [Get] :: Check for an existing alertType, by passing a alertType Id
#Scenario: Check if a alertType exists
#	Given the following AlertType Id input
#		| Id | 
#		| 3  | 
#	When I call the AlertType Exists Get api endpoint by Id to verify if it exists
#	Then the AlertType exists result should be bool true or false
