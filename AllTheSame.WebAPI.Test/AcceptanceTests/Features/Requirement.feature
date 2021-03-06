﻿Feature: Requirement
	In order to see a list of Requirements
	As a Community Administrator
	I want to load a list of Requirements

@Requirement 
#[C]RUD - [Post] :: Create, Check, GetById, Update and Delete Requirement, by passing a newly populated Requirement
Scenario: Requirement--Add, Check, GetById, Update and Delete Requirement
	Given the following Requirement Add input
		| CommunityId | RequirementTypeId | Description  |
		| 1          | 1                 | SpecFlowTest |
	When I call the add Requirement Post api endpoint to add a Requirement it checks if exists pulls item edits it and deletes it
	Then the add result should be a Requirement Id check exists get by id edit and delete with http response returns

@Requirements 
#C[R]UD - [Get] :: Retrieve all Requirements, without passing anything
Scenario: Requirement--Retrieve all Requirements
	#Given I am an authenticated user
	When I call the Requirement Get api endpoint
	Then the get result should be a list of Requirements

@Requirement 
#[C]RUD - [Post] :: Create a new Requirement, by passing a newly populated Requirement
Scenario: Add a Requirement
	Given the following Requirement Add input
		| CommunityId | RequirementTypeId | Description  |
		| 2          | 2                 | SpecTest	 |
	When I call the add Requirement Post api endpoint to add a Requirement
	Then the add result should be a Requirement Id
#
#@Requirement 
##[C]RUD - [Post] :: Create a new requirement, by passing a newly populated requirement
#Scenario: Add a requirement
#	Given the following Requirement Add input
#		| CommunityId | RequirementTypeId | 
#		| 17		  | 1				  |
#	When I call the add Requirement Post api endpoint to add a requirement
#	Then the add result should be a Requirement Id
#
#@Requirements 
##C[R]UD - [Get] :: Retrieve all requirements, without passing anything
#Scenario: Retrieve all requirements
#	#Given I am an authenticated user
#	When I call the Requirement Get api endpoint
#	Then the get result should be a list of requirements
#
#@Requirement 
##C[R]UD - [Get] :: Retrieve an existing requirement, by passing a requirement Id
#Scenario: Retrieve a requirement by Id
#	Given the following Requirement GetById input
#		| Id |
#		| 1  |
#	When I call the Requirement Get api endpoint by Id
#	Then the get by id result should be a Requirement
#
#@Requirement 
##CR[U]D - [Put] :: Update an existing requirement, by passing changes populated in requirement and its Id
#Scenario: Update a requirement
#	Given the following Requirement Edit input
#		| Id | CommunityId | RequirementTypeId |
#		| 1  | 17		   | 1				   |
#	When I call the edit Requirement Put api endpoint to edit a requirement
#	Then the edit result should be an updated Requirement
#
#@Requirement 
##CRU[D] - [Post] :: Delete an existing requirement, by passing a populated in requirement object
#Scenario: Delete a requirement
#	Given the following Requirement Delete input
#	#use Id from recently added
#		| Id | 
#		| 0  | 
#	When I call the delete Requirement Post api endpoint to delete a requirement
#	Then the delete result should be an deleted Requirement
#
#@Requirement 
##Helper - [Get] :: Check for an existing requirement, by passing a requirement Id
#Scenario: Check if a requirement exists
#	Given the following Requirement Id input
#		| Id | 
#		| 2  | 
#	When I call the Requirement Exists Get api endpoint by Id to verify if it exists
#	Then the Requirement exists result should be bool true or false
