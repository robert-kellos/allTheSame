Feature: VendorAdmin
	In order to see a list of VendorAdmins
	As a Community Administrator
	I want to load a list of VendorAdmins

@VendorAdmin 
#[C]RUD - [Post] :: Create, Check, GetById, Update and Delete VendorAdmin, by passing a newly populated VendorAdmin
Scenario: VendorAdmin--Add, Check, GetById, Update and Delete VendorAdmin
	Given the following VendorAdmin Add input
		| PersonId | VendorId |
		| 1        | 1        |
	When I call the add VendorAdmin Post api endpoint to add a VendorAdmin it checks if exists pulls item edits it and deletes it
	Then the add result should be a VendorAdmin Id check exists get by id edit and delete with http response returns

@VendorAdmins 
#C[R]UD - [Get] :: Retrieve all VendorAdmins, without passing anything
Scenario: VendorAdmin--Retrieve all VendorAdmins
	#Given I am an authenticated user
	When I call the VendorAdmin Get api endpoint
	Then the get result should be a list of VendorAdmins

@VendorAdmin 
#[C]RUD - [Post] :: Create a new VendorAdmin, by passing a newly populated VendorAdmin
Scenario: Add a VendorAdmin
	Given the following VendorAdmin Add input
		| PersonId | VendorId |
		| 2        | 2        |
	When I call the add VendorAdmin Post api endpoint to add a VendorAdmin
	Then the add result should be a VendorAdmin Id

#@VendorAdmin 
##[C]RUD - [Post] :: Create a new vendorAdmin, by passing a newly populated vendorAdmin
#Scenario: Add a vendorAdmin
#	Given the following VendorAdmin Add input
#		| FirstName | LastName | Email   | MobileNumber |
#		| Spec      | Flow     | x@y.com | 800-555-1212 |
#	When I call the add VendorAdmin Post api endpoint to add a vendorAdmin
#	Then the add result should be a VendorAdmin Id
#
#@VendorAdmins 
##C[R]UD - [Get] :: Retrieve all vendorAdmins, without passing anything
#Scenario: Retrieve all vendorAdmins
#	#Given I am an authenticated user
#	When I call the VendorAdmin Get api endpoint
#	Then the get result should be a list of vendorAdmins
#
#@VendorAdmin 
##C[R]UD - [Get] :: Retrieve an existing vendorAdmin, by passing a vendorAdmin Id
#Scenario: Retrieve a vendorAdmin by Id
#	Given the following VendorAdmin GetById input
#		| Id |
#		| 2  |
#	When I call the VendorAdmin Get api endpoint by Id
#	Then the get by id result should be a VendorAdmin
#
#@VendorAdmin 
##CR[U]D - [Put] :: Update an existing vendorAdmin, by passing changes populated in vendorAdmin and its Id
#Scenario: Update a vendorAdmin
#	Given the following VendorAdmin Edit input
#		| Id | FirstName | LastName | Email   | MobileNumber |
#		| 2  | Spec      | Flow     | x@y.com | 800-555-1212 |
#	When I call the edit VendorAdmin Put api endpoint to edit a vendorAdmin
#	Then the edit result should be an updated VendorAdmin
#
#@VendorAdmin 
##CRU[D] - [Post] :: Delete an existing vendorAdmin, by passing a populated in vendorAdmin object
#Scenario: Delete a vendorAdmin
#	Given the following VendorAdmin Delete input
#	#use Id from recently added
#		| Id | 
#		| 0  | 
#	When I call the delete VendorAdmin Post api endpoint to delete a vendorAdmin
#	Then the delete result should be an deleted VendorAdmin
#
#@VendorAdmin 
##Helper - [Get] :: Check for an existing vendorAdmin, by passing a vendorAdmin Id
#Scenario: Check if a vendorAdmin exists
#	Given the following VendorAdmin Id input
#		| Id | 
#		| 2  | 
#	When I call the VendorAdmin Exists Get api endpoint by Id to verify if it exists
#	Then the VendorAdmin exists result should be bool true or false
