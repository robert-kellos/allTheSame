Feature: OrgType
	In order to see a list of OrgTypes
	As a Community Administrator
	I want to load a list of OrgTypes

@OrgType 
#[C]RUD - [Post] :: Create, Check, GetById, Update and Delete OrgType, by passing a newly populated OrgType
Scenario: OrgType--Add, Check, GetById, Update and Delete OrgType
	Given the following OrgType Add input
		| Label | Code |
		| Spec  | Flow | 
	When I call the add OrgType Post api endpoint to add a OrgType it checks if exists pulls item edits it and deletes it
	Then the add result should be a OrgType Id check exists get by id edit and delete with http response returns

@OrgTypes 
#C[R]UD - [Get] :: Retrieve all OrgTypes, without passing anything
Scenario: OrgType--Retrieve all OrgTypes
	#Given I am an authenticated user
	When I call the OrgType Get api endpoint
	Then the get result should be a list of OrgTypes

@OrgType 
#[C]RUD - [Post] :: Create a new OrgType, by passing a newly populated OrgType
Scenario: Add a OrgType
	Given the following OrgType Add input
		| Label		| Code		|
		| SpecTest  | FlowTest	| 
	When I call the add OrgType Post api endpoint to add a OrgType
	Then the add result should be a OrgType Id

#@OrgType 
##[C]RUD - [Post] :: Create a new orgType, by passing a newly populated orgType
#Scenario: Add a orgType
#	Given the following OrgType Add input
		#| Label | Code |
		#| Spec  | Flow | 
#	When I call the add OrgType Post api endpoint to add a orgType
#	Then the add result should be a OrgType Id
#
#@OrgTypes 
##C[R]UD - [Get] :: Retrieve all orgTypes, without passing anything
#Scenario: Retrieve all orgTypes
#	#Given I am an authenticated user
#	When I call the OrgType Get api endpoint
#	Then the get result should be a list of orgTypes
#
#@OrgType 
##C[R]UD - [Get] :: Retrieve an existing orgType, by passing a orgType Id
#Scenario: Retrieve a orgType by Id
#	Given the following OrgType GetById input
#		| Id |
#		| 2  |
#	When I call the OrgType Get api endpoint by Id
#	Then the get by id result should be a OrgType
#
#@OrgType 
##CR[U]D - [Put] :: Update an existing orgType, by passing changes populated in orgType and its Id
#Scenario: Update a orgType
#	Given the following OrgType Edit input
#		| Id | Code | Label | 
#		| 2  | Spec | Flow  |
#	When I call the edit OrgType Put api endpoint to edit a orgType
#	Then the edit result should be an updated OrgType
#
#@OrgType 
##CRU[D] - [Post] :: Delete an existing orgType, by passing a populated in orgType object
#Scenario: Delete a orgType
#	Given the following OrgType Delete input
#	#use Id from recently added
#		| Id | 
#		| 0  | 
#	When I call the delete OrgType Post api endpoint to delete a orgType
#	Then the delete result should be an deleted OrgType
#
#@OrgType 
##Helper - [Get] :: Check for an existing orgType, by passing a orgType Id
#Scenario: Check if a orgType exists
#	Given the following OrgType Id input
#		| Id | 
#		| 2  | 
#	When I call the OrgType Exists Get api endpoint by Id to verify if it exists
#	Then the OrgType exists result should be bool true or false
