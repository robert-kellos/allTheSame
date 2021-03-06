﻿Feature: RequirementType
	In order to see a list of RequirementTypes
	As a Community Administrator
	I want to load a list of RequirementTypes

@RequirementType 
#[C]RUD - [Post] :: Create, Check, GetById, Update and Delete RequirementType, by passing a newly populated RequirementType
Scenario: RequirementType--Add, Check, GetById, Update and Delete RequirementType
	Given the following RequirementType Add input
		| Code | Label |
		| Spec | Flow  | 
	When I call the add RequirementType Post api endpoint to add a RequirementType it checks if exists pulls item edits it and deletes it
	Then the add result should be a RequirementType Id check exists get by id edit and delete with http response returns

@RequirementTypes 
#C[R]UD - [Get] :: Retrieve all RequirementTypes, without passing anything
Scenario: RequirementType--Retrieve all RequirementTypes
	#Given I am an authenticated user
	When I call the RequirementType Get api endpoint
	Then the get result should be a list of RequirementTypes

@RequirementType 
#[C]RUD - [Post] :: Create a new RequirementType, by passing a newly populated RequirementType
Scenario: Add a RequirementType
	Given the following RequirementType Add input
		| Code | Label		|
		| Spec | FlowTest   | 
	When I call the add RequirementType Post api endpoint to add a RequirementType
	Then the add result should be a RequirementType Id

#@RequirementType 
##[C]RUD - [Post] :: Create a new requirementType, by passing a newly populated requirementType
#Scenario: Add a requirementType
#	Given the following RequirementType Add input
#		| Code | Label |
#		| Spec | Flow  |
#	When I call the add RequirementType Post api endpoint to add a RequirementType
#	Then the add result should be a RequirementType Id
#
#@RequirementTypes 
##C[R]UD - [Get] :: Retrieve all requirementTypes, without passing anything
#Scenario: Retrieve all requirementTypes
#	#Given I am an authenticated user
#	When I call the RequirementType Get api endpoint
#	Then the get result should be a list of requirementTypes
#
#@RequirementType 
##C[R]UD - [Get] :: Retrieve an existing requirementType, by passing a requirementType Id
#Scenario: Retrieve a requirementType by Id
#	Given the following RequirementType GetById input
#		| Id |
#		| 2  |
#	When I call the RequirementType Get api endpoint by Id
#	Then the get by id result should be a RequirementType
#
#@RequirementType 
##CR[U]D - [Put] :: Update an existing requirementType, by passing changes populated in requirementType and its Id
#Scenario: Update a requirementType
#	Given the following RequirementType Edit input
#		| Id | Code | Label |
#		| 1  | Spec | Flow  |
#	When I call the edit RequirementType Put api endpoint to edit a requirementType
#	Then the edit result should be an updated RequirementType
#
#@RequirementType 
##CRU[D] - [Post] :: Delete an existing requirementType, by passing a populated in requirementType object
#Scenario: Delete a requirementType
#	Given the following RequirementType Delete input
#	#use Id from recently added
#		| Id | 
#		| 0  | 
#	When I call the delete RequirementType Post api endpoint to delete a requirementType
#	Then the delete result should be an deleted RequirementType
#
#@RequirementType 
##Helper - [Get] :: Check for an existing requirementType, by passing a requirementType Id
#Scenario: Check if a requirementType exists
#	Given the following RequirementType Id input
#		| Id | 
#		| 2  | 
#	When I call the RequirementType Exists Get api endpoint by Id to verify if it exists
#	Then the RequirementType exists result should be bool true or false
