﻿Feature: VendorWorker
	In order to see a list of VendorWorkers
	As a Community Administrator
	I want to load a list of VendorWorkers

@VendorWorker 
#[C]RUD - [Post] :: Create, Check, GetById, Update and Delete VendorWorker, by passing a newly populated VendorWorker
Scenario: VendorWorker--Add, Check, GetById, Update and Delete VendorWorker
	Given the following VendorWorker Add input
		| PersonId | VendorId | VendorTypeId |
		| 1        | 1        | 1            | 
	When I call the add VendorWorker Post api endpoint to add a VendorWorker it checks if exists pulls item edits it and deletes it
	Then the add result should be a VendorWorker Id check exists get by id edit and delete with http response returns

@VendorWorkers 
#C[R]UD - [Get] :: Retrieve all VendorWorkers, without passing anything
Scenario: VendorWorker--Retrieve all VendorWorkers
	#Given I am an authenticated user
	When I call the VendorWorker Get api endpoint
	Then the get result should be a list of VendorWorkers

@VendorWorker 
#[C]RUD - [Post] :: Create a new VendorWorker, by passing a newly populated VendorWorker
Scenario: Add a VendorWorker
	Given the following VendorWorker Add input
		| PersonId | VendorId | VendorTypeId |
		| 2        | 1        | 2            | 
	When I call the add VendorWorker Post api endpoint to add a VendorWorker
	Then the add result should be a VendorWorker Id

#@VendorWorker 
##[C]RUD - [Post] :: Create a new vendorWorker, by passing a newly populated vendorWorker
#Scenario: Add a vendorWorker
#	Given the following VendorWorker Add input
#		| FirstName | LastName | Email   | MobileNumber |
#		| Spec      | Flow     | x@y.com | 800-555-1212 |
#	When I call the add VendorWorker Post api endpoint to add a VendorWorker
#	Then the add result should be a VendorWorker Id
#
#@VendorWorkers 
##C[R]UD - [Get] :: Retrieve all vendorWorkers, without passing anything
#Scenario: Retrieve all vendorWorkers
#	#Given I am an authenticated user
#	When I call the VendorWorker Get api endpoint
#	Then the get result should be a list of VendorWorkers
#
#@VendorWorker 
##C[R]UD - [Get] :: Retrieve an existing vendorWorker, by passing a vendorWorker Id
#Scenario: Retrieve a vendorWorker by Id
#	Given the following VendorWorker GetById input
#		| Id |
#		| 2  |
#	When I call the VendorWorker Get api endpoint by Id
#	Then the get by id result should be a VendorWorker
#
#@VendorWorker 
##CR[U]D - [Put] :: Update an existing vendorWorker, by passing changes populated in vendorWorker and its Id
#Scenario: Update a vendorWorker
#	Given the following VendorWorker Edit input
#		| Id | FirstName | LastName | Email   | MobileNumber |
#		| 2  | Spec      | Flow     | x@y.com | 800-555-1212 |
#	When I call the edit VendorWorker Put api endpoint to edit a vendorWorker
#	Then the edit result should be an updated VendorWorker
#
#@VendorWorker 
##CRU[D] - [Post] :: Delete an existing vendorWorker, by passing a populated in vendorWorker object
#Scenario: Delete a vendorWorker
#	Given the following VendorWorker Delete input
#	#use Id from recently added
#		| Id | 
#		| 0  | 
#	When I call the delete VendorWorker Post api endpoint to delete a vendorWorker
#	Then the delete result should be an deleted VendorWorker
#
#@VendorWorker 
##Helper - [Get] :: Check for an existing vendorWorker, by passing a vendorWorker Id
#Scenario: Check if a vendorWorker exists
#	Given the following VendorWorker Id input
#		| Id | 
#		| 2  | 
#	When I call the VendorWorker Exists Get api endpoint by Id to verify if it exists
#	Then the VendorWorker exists result should be bool true or false
