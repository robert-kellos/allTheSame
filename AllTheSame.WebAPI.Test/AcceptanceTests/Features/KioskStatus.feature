Feature: KioskStatus
	In order to see a list of KioskStatuses
	As a Community Administrator
	I want to load a list of KioskStatuses

@KioskStatus 
#[C]RUD - [Post] :: Create, Check, GetById, Update and Delete KioskStatus, by passing a newly populated KioskStatus
Scenario: KioskStatus--Add, Check, GetById, Update and Delete KioskStatus
	Given the following KioskStatus Add input
		| Code		  | Label	|
		| SpecFlow    | test	|
	When I call the add KioskStatus Post api endpoint to add a KioskStatus it checks if exists pulls item edits it and deletes it
	Then the add result should be a KioskStatus Id check exists get by id edit and delete with http response returns

@KioskStatuses 
#C[R]UD - [Get] :: Retrieve all KioskStatuses, without passing anything
Scenario: KioskStatus--Retrieve all KioskStatuses
	#Given I am an authenticated user
	When I call the KioskStatus Get api endpoint
	Then the get result should be a list of KioskStatuses

@KioskStatus 
#[C]RUD - [Post] :: Create a new KioskStatus, by passing a newly populated KioskStatus
Scenario: Add a KioskStatus
	Given the following KioskStatus Add input
		| Code			| Label		| 
		| SpecFlowTest  | FlowTest  |
	When I call the add KioskStatus Post api endpoint to add a KioskStatus
	Then the add result should be a KioskStatus Id

#@KioskStatus 
##[C]RUD - [Post] :: Create a new kioskStatus, by passing a newly populated kioskStatus
#Scenario: Add a kioskStatus
#	Given the following KioskStatus Add input
#		| FirstName | LastName | Email   | MobileNumber |
#		| Spec      | Flow     | x@y.com | 800-555-1212 |
#	When I call the add KioskStatus Post api endpoint to add a kioskStatus
#	Then the add result should be a KioskStatus Id
#
#@KioskStatuss 
##C[R]UD - [Get] :: Retrieve all kioskStatuss, without passing anything
#Scenario: Retrieve all kioskStatuses
#	#Given I am an authenticated user
#	When I call the KioskStatus Get api endpoint
#	Then the get result should be a list of kioskStatuses
#
#@KioskStatus 
##C[R]UD - [Get] :: Retrieve an existing kioskStatus, by passing a kioskStatus Id
#Scenario: Retrieve a kioskStatus by Id
#	Given the following KioskStatus GetById input
#		| Id |
#		| 2  |
#	When I call the KioskStatus Get api endpoint by Id
#	Then the get by id result should be a KioskStatus
#
#@KioskStatus 
##CR[U]D - [Put] :: Update an existing kioskStatus, by passing changes populated in kioskStatus and its Id
#Scenario: Update a kioskStatus
#	Given the following KioskStatus Edit input
#		| Id | FirstName | LastName | Email   | MobileNumber |
#		| 2  | Spec      | Flow     | x@y.com | 800-555-1212 |
#	When I call the edit KioskStatus Put api endpoint to edit a kioskStatus
#	Then the edit result should be an updated KioskStatus
#
#@KioskStatus 
##CRU[D] - [Post] :: Delete an existing kioskStatus, by passing a populated in kioskStatus object
#Scenario: Delete a kioskStatus
#	Given the following KioskStatus Delete input
#	#use Id from recently added
#		| Id | 
#		| 0  | 
#	When I call the delete KioskStatus Post api endpoint to delete a kioskStatus
#	Then the delete result should be an deleted KioskStatus
#
#@KioskStatus 
##Helper - [Get] :: Check for an existing kioskStatus, by passing a kioskStatus Id
#Scenario: Check if a kioskStatus exists
#	Given the following KioskStatus Id input
#		| Id | 
#		| 2  | 
#	When I call the KioskStatus Exists Get api endpoint by Id to verify if it exists
#	Then the KioskStatus exists result should be bool true or false
