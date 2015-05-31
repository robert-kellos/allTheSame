Feature: VendorCredDocument
	In order to see a list of vendorCredDocuments
	As a Community Administrator
	I want to load a list of vendorCredDocuments

@VendorCredDocument 
#[C]RUD - [Post] :: Create a new vendorCredDocument, by passing a newly populated vendorCredDocument
Scenario: Add a vendorCredDocument
	Given the following VendorCredDocument Add input
		| FirstName | LastName | Email   | MobileNumber |
		| Spec      | Flow     | x@y.com | 800-555-1212 |
	When I call the add VendorCredDocument Post api endpoint to add a vendorCredDocument
	Then the add result should be a VendorCredDocument Id

@VendorCredDocuments 
#C[R]UD - [Get] :: Retrieve all vendorCredDocuments, without passing anything
Scenario: Retrieve all vendorCredDocuments
	#Given I am an authenticated user
	When I call the VendorCredDocument Get api endpoint
	Then the get result should be a list of vendorCredDocuments

@VendorCredDocument 
#C[R]UD - [Get] :: Retrieve an existing vendorCredDocument, by passing a vendorCredDocument Id
Scenario: Retrieve a vendorCredDocument by Id
	Given the following VendorCredDocument GetById input
		| Id |
		| 2  |
	When I call the VendorCredDocument Get api endpoint by Id
	Then the get by id result should be a VendorCredDocument

@VendorCredDocument 
#CR[U]D - [Put] :: Update an existing vendorCredDocument, by passing changes populated in vendorCredDocument and its Id
Scenario: Update a vendorCredDocument
	Given the following VendorCredDocument Edit input
		| Id | FirstName | LastName | Email   | MobileNumber |
		| 2  | Spec      | Flow     | x@y.com | 800-555-1212 |
	When I call the edit VendorCredDocument Put api endpoint to edit a vendorCredDocument
	Then the edit result should be an updated VendorCredDocument

@VendorCredDocument 
#CRU[D] - [Post] :: Delete an existing vendorCredDocument, by passing a populated in vendorCredDocument object
Scenario: Delete a vendorCredDocument
	Given the following VendorCredDocument Delete input
	#use Id from recently added
		| Id | 
		| 0  | 
	When I call the delete VendorCredDocument Post api endpoint to delete a vendorCredDocument
	Then the delete result should be an deleted VendorCredDocument

@VendorCredDocument 
#Helper - [Get] :: Check for an existing vendorCredDocument, by passing a vendorCredDocument Id
Scenario: Check if a vendorCredDocument exists
	Given the following VendorCredDocument Id input
		| Id | 
		| 2  | 
	When I call the VendorCredDocument Exists Get api endpoint by Id to verify if it exists
	Then the VendorCredDocument exists result should be bool true or false
