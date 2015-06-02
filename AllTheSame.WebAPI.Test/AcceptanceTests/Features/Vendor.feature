Feature: Vendor
	In order to see a list of Vendors
	As a Community Administrator
	I want to load a list of Vendors

@Vendor 
#[C]RUD - [Post] :: Create, Check, GetById, Update and Delete Vendor, by passing a newly populated Vendor
Scenario: Vendor--Add, Check, GetById, Update and Delete Vendor
	Given the following Vendor Add input
		| Name     | OrgId |
		| SpecFlow | 1     |
	When I call the add Vendor Post api endpoint to add a Vendor it checks if exists pulls item edits it and deletes it
	Then the add result should be a Vendor Id check exists get by id edit and delete with http response returns

@Vendors 
#C[R]UD - [Get] :: Retrieve all Vendors, without passing anything
Scenario: Vendor--Retrieve all Vendors
	#Given I am an authenticated user
	When I call the Vendor Get api endpoint
	Then the get result should be a list of Vendors

@Vendor 
#[C]RUD - [Post] :: Create a new Vendor, by passing a newly populated Vendor
Scenario: Add a Vendor
	Given the following Vendor Add input
		| Name			| OrgId		|
		| SpecFlowTest	| 2		| 
	When I call the add Vendor Post api endpoint to add a Vendor
	Then the add result should be a Vendor Id

#@Vendor 
##[C]RUD - [Post] :: Create a new vendor, by passing a newly populated vendor
#Scenario: Add a vendor
#	Given the following Vendor Add input
#		| FirstName | LastName | Email   | MobileNumber |
#		| Spec      | Flow     | x@y.com | 800-555-1212 |
#	When I call the add Vendor Post api endpoint to add a vendor
#	Then the add result should be a Vendor Id
#
#@Vendors 
##C[R]UD - [Get] :: Retrieve all vendors, without passing anything
#Scenario: Retrieve all vendors
#	#Given I am an authenticated user
#	When I call the Vendor Get api endpoint
#	Then the get result should be a list of vendors
#
#@Vendor 
##C[R]UD - [Get] :: Retrieve an existing vendor, by passing a vendor Id
#Scenario: Retrieve a vendor by Id
#	Given the following Vendor GetById input
#		| Id |
#		| 2  |
#	When I call the Vendor Get api endpoint by Id
#	Then the get by id result should be a Vendor
#
#@Vendor 
##CR[U]D - [Put] :: Update an existing vendor, by passing changes populated in vendor and its Id
#Scenario: Update a vendor
#	Given the following Vendor Edit input
#		| Id | FirstName | LastName | Email   | MobileNumber |
#		| 2  | Spec      | Flow     | x@y.com | 800-555-1212 |
#	When I call the edit Vendor Put api endpoint to edit a vendor
#	Then the edit result should be an updated Vendor
#
#@Vendor 
##CRU[D] - [Post] :: Delete an existing vendor, by passing a populated in vendor object
#Scenario: Delete a vendor
#	Given the following Vendor Delete input
#	#use Id from recently added
#		| Id | 
#		| 0  | 
#	When I call the delete Vendor Post api endpoint to delete a vendor
#	Then the delete result should be an deleted Vendor
#
#@Vendor 
##Helper - [Get] :: Check for an existing vendor, by passing a vendor Id
#Scenario: Check if a vendor exists
#	Given the following Vendor Id input
#		| Id | 
#		| 2  | 
#	When I call the Vendor Exists Get api endpoint by Id to verify if it exists
#	Then the Vendor exists result should be bool true or false
