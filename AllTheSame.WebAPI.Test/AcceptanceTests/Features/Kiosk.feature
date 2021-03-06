﻿Feature: Kiosk
	In order to see a list of Kiosks
	As a Community Administrator
	I want to load a list of Kiosks

@Kiosk 
#[C]RUD - [Post] :: Create, Check, GetById, Update and Delete Kiosk, by passing a newly populated Kiosk
Scenario: Kiosk--Add, Check, GetById, Update and Delete Kiosk
	Given the following Kiosk Add input
		| CommunityId | KioskStatusId | Name         |
		| 1          | 1             | SpecFlowTest |
	When I call the add Kiosk Post api endpoint to add a Kiosk it checks if exists pulls item edits it and deletes it
	Then the add result should be a Kiosk Id check exists get by id edit and delete with http response returns

@Kiosks 
#C[R]UD - [Get] :: Retrieve all Kiosks, without passing anything
Scenario: Kiosk--Retrieve all Kiosks
	#Given I am an authenticated user
	When I call the Kiosk Get api endpoint
	Then the get result should be a list of Kiosks

@Kiosk 
#[C]RUD - [Post] :: Create a new Kiosk, by passing a newly populated Kiosk
Scenario: Add a Kiosk
	Given the following Kiosk Add input
		| CommunityId | KioskStatusId | Name         |
		| 2          | 2             | Test |
	When I call the add Kiosk Post api endpoint to add a Kiosk
	Then the add result should be a Kiosk Id

#@Kiosk 
##[C]RUD - [Post] :: Create a new kiosk, by passing a newly populated kiosk
#Scenario: Add a kiosk
#	Given the following Kiosk Add input
#		| FirstName | LastName | Email   | MobileNumber |
#		| Spec      | Flow     | x@y.com | 800-555-1212 |
#	When I call the add Kiosk Post api endpoint to add a kiosk
#	Then the add result should be a Kiosk Id
#
#@Kiosks 
##C[R]UD - [Get] :: Retrieve all kiosks, without passing anything
#Scenario: Retrieve all kiosks
#	#Given I am an authenticated user
#	When I call the Kiosk Get api endpoint
#	Then the get result should be a list of kiosks
#
#@Kiosk 
##C[R]UD - [Get] :: Retrieve an existing kiosk, by passing a kiosk Id
#Scenario: Retrieve a kiosk by Id
#	Given the following Kiosk GetById input
#		| Id |
#		| 2  |
#	When I call the Kiosk Get api endpoint by Id
#	Then the get by id result should be a Kiosk
#
#@Kiosk 
##CR[U]D - [Put] :: Update an existing kiosk, by passing changes populated in kiosk and its Id
#Scenario: Update a kiosk
#	Given the following Kiosk Edit input
#		| Id | FirstName | LastName | Email   | MobileNumber |
#		| 2  | Spec      | Flow     | x@y.com | 800-555-1212 |
#	When I call the edit Kiosk Put api endpoint to edit a kiosk
#	Then the edit result should be an updated Kiosk
#
#@Kiosk 
##CRU[D] - [Post] :: Delete an existing kiosk, by passing a populated in kiosk object
#Scenario: Delete a kiosk
#	Given the following Kiosk Delete input
#	#use Id from recently added
#		| Id | 
#		| 0  | 
#	When I call the delete Kiosk Post api endpoint to delete a kiosk
#	Then the delete result should be an deleted Kiosk
#
#@Kiosk 
##Helper - [Get] :: Check for an existing kiosk, by passing a kiosk Id
#Scenario: Check if a kiosk exists
#	Given the following Kiosk Id input
#		| Id | 
#		| 2  | 
#	When I call the Kiosk Exists Get api endpoint by Id to verify if it exists
#	Then the Kiosk exists result should be bool true or false
