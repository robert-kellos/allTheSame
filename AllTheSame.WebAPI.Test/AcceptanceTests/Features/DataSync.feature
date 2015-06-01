Feature: DataSync
	In order to see a list of DataSyncs
	As a Community Administrator
	I want to load a list of DataSyncs

@DataSync 
#[C]RUD - [Post] :: Create, Check, GetById, Update and Delete DataSync, by passing a newly populated DataSync
Scenario: DataSync--Add, Check, GetById, Update and Delete DataSync
	Given the following DataSync Add input
		| KioskId |
		| 2		  |
	When I call the add DataSync Post api endpoint to add a DataSync it checks if exists pulls item edits it and deletes it
	Then the add result should be a DataSync Id check exists get by id edit and delete with http response returns

@DataSyncs 
#C[R]UD - [Get] :: Retrieve all DataSyncs, without passing anything
Scenario: DataSync--Retrieve all DataSyncs
	#Given I am an authenticated user
	When I call the DataSync Get api endpoint
	Then the get result should be a list of DataSyncs

@DataSync 
#[C]RUD - [Post] :: Create a new DataSync, by passing a newly populated DataSync
Scenario: Add a DataSync
	Given the following DataSync Add input
		| KioskId |
		| 3		  |
	When I call the add DataSync Post api endpoint to add a DataSync
	Then the add result should be a DataSync Id

#@DataSync 
##[C]RUD - [Post] :: Create a new DataSync, by passing a newly populated DataSync
#Scenario: Add a DataSync
#	Given the following DataSync Add input
#		| FirstName | LastName | Email   | MobileNumber |
#		| Spec      | Flow     | x@y.com | 800-555-1212 |
#	When I call the add DataSync Post api endpoint to add a dataSync
#	Then the add result should be a DataSync Id
#
#@DataSyncs 
##C[R]UD - [Get] :: Retrieve all dataSyncs, without passing anything
#Scenario: Retrieve all dataSyncs
#	#Given I am an authenticated user
#	When I call the DataSync Get api endpoint
#	Then the get result should be a list of DataSyncs
#
#@DataSync 
##C[R]UD - [Get] :: Retrieve an existing dataSync, by passing a dataSync Id
#Scenario: Retrieve a dataSync by Id
#	Given the following DataSync GetById input
#		| Id |
#		| 2  |
#	When I call the DataSync Get api endpoint by Id
#	Then the get by id result should be a DataSync
#
#@DataSync 
##CR[U]D - [Put] :: Update an existing dataSync, by passing changes populated in dataSync and its Id
#Scenario: Update a dataSync
#	Given the following DataSync Edit input
#		| Id | FirstName | LastName | Email   | MobileNumber |
#		| 2  | Spec      | Flow     | x@y.com | 800-555-1212 |
#	When I call the edit DataSync Put api endpoint to edit a dataSync
#	Then the edit result should be an updated DataSync
#
#@DataSync 
##CRU[D] - [Post] :: Delete an existing dataSync, by passing a populated in dataSync object
#Scenario: Delete a dataSync
#	Given the following DataSync Delete input
#	#use Id from recently added
#		| Id | 
#		| 0  | 
#	When I call the delete DataSync Post api endpoint to delete a dataSync
#	Then the delete result should be an deleted DataSync
#
#@DataSync 
##Helper - [Get] :: Check for an existing dataSync, by passing a dataSync Id
#Scenario: Check if a dataSync exists
#	Given the following DataSync Id input
#		| Id | 
#		| 2  | 
#	When I call the DataSync Exists Get api endpoint by Id to verify if it exists
#	Then the DataSync exists result should be bool true or false
