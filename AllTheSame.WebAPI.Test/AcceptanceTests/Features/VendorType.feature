Feature: VendorType
	In order to see a list of VendorTypes
	As a Community Administrator
	I want to load a list of VendorTypes

@VendorType 
#[C]RUD - [Post] :: Create, Check, GetById, Update and Delete VendorType, by passing a newly populated VendorType
Scenario: VendorType--Add, Check, GetById, Update and Delete VendorType
	Given the following VendorType Add input
		| Code     | Label |
		| SpecFlow | Test  |
	When I call the add VendorType Post api endpoint to add a VendorType it checks if exists pulls item edits it and deletes it
	Then the add result should be a VendorType Id check exists get by id edit and delete with http response returns

@VendorTypes 
#C[R]UD - [Get] :: Retrieve all VendorTypes, without passing anything
Scenario: VendorType--Retrieve all VendorTypes
	#Given I am an authenticated user
	When I call the VendorType Get api endpoint
	Then the get result should be a list of VendorTypes

@VendorType 
#[C]RUD - [Post] :: Create a new VendorType, by passing a newly populated VendorType
Scenario: Add a VendorType
	Given the following VendorType Add input
		| Code			| Label		|
		| SpecFlowTest	| FlowTest	|
	When I call the add VendorType Post api endpoint to add a VendorType
	Then the add result should be a VendorType Id

#@VendorType 
##[C]RUD - [Post] :: Create a new vendorType, by passing a newly populated vendorType
#Scenario: Add a vendorType
#	Given the following VendorType Add input
#		| FirstName | LastName | Email   | MobileNumber |
#		| Spec      | Flow     | x@y.com | 800-555-1212 |
#	When I call the add VendorType Post api endpoint to add a vendorType
#	Then the add result should be a VendorType Id
#
#@VendorTypes 
##C[R]UD - [Get] :: Retrieve all vendorTypes, without passing anything
#Scenario: Retrieve all vendorTypes
#	#Given I am an authenticated user
#	When I call the VendorType Get api endpoint
#	Then the get result should be a list of vendorTypes
#
#@VendorType 
##C[R]UD - [Get] :: Retrieve an existing vendorType, by passing a vendorType Id
#Scenario: Retrieve a vendorType by Id
#	Given the following VendorType GetById input
#		| Id |
#		| 2  |
#	When I call the VendorType Get api endpoint by Id
#	Then the get by id result should be a VendorType
#
#@VendorType 
##CR[U]D - [Put] :: Update an existing vendorType, by passing changes populated in vendorType and its Id
#Scenario: Update a vendorType
#	Given the following VendorType Edit input
#		| Id | FirstName | LastName | Email   | MobileNumber |
#		| 2  | Spec      | Flow     | x@y.com | 800-555-1212 |
#	When I call the edit VendorType Put api endpoint to edit a vendorType
#	Then the edit result should be an updated VendorType
#
#@VendorType 
##CRU[D] - [Post] :: Delete an existing vendorType, by passing a populated in vendorType object
#Scenario: Delete a vendorType
#	Given the following VendorType Delete input
#	#use Id from recently added
#		| Id | 
#		| 0  | 
#	When I call the delete VendorType Post api endpoint to delete a vendorType
#	Then the delete result should be an deleted VendorType
#
#@VendorType 
##Helper - [Get] :: Check for an existing vendorType, by passing a vendorType Id
#Scenario: Check if a vendorType exists
#	Given the following VendorType Id input
#		| Id | 
#		| 2  | 
#	When I call the VendorType Exists Get api endpoint by Id to verify if it exists
#	Then the VendorType exists result should be bool true or false
