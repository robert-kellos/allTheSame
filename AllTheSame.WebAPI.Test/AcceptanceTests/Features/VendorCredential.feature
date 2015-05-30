Feature: VendorCredential
	In order to see a list of vendorCredentials
	As a Community Administrator
	I want to load a list of vendorCredentials

@VendorCredential 
#[C]RUD - [Post] :: Create a new vendorCredential, by passing a newly populated vendorCredential
Scenario: Add a vendorCredential
	Given the following VendorCredential Add input
		| FirstName | LastName | Email   | MobileNumber |
		| Spec      | Flow     | x@y.com | 800-555-1212 |
	When I call the add VendorCredential Post api endpoint to add a vendorCredential
	Then the add result should be a VendorCredential Id

@VendorCredentials 
#C[R]UD - [Get] :: Retrieve all vendorCredentials, without passing anything
Scenario: Retrieve all vendorCredentials
	#Given I am an authenticated user
	When I call the VendorCredential Get api endpoint
	Then the get result should be a list of vendorCredentials

@VendorCredential 
#C[R]UD - [Get] :: Retrieve an existing vendorCredential, by passing a vendorCredential Id
Scenario: Retreive a vendorCredential by Id
	Given the following VendorCredential GetById input
		| Id |
		| 2  |
	When I call the VendorCredential Get api endpoint by Id
	Then the get by id result should be a VendorCredential

@VendorCredential 
#CR[U]D - [Put] :: Update an existing vendorCredential, by passing changes populated in vendorCredential and its Id
Scenario: Update a vendorCredential
	Given the following VendorCredential Edit input
		| Id | FirstName | LastName | Email   | MobileNumber |
		| 2  | Spec      | Flow     | x@y.com | 800-555-1212 |
	When I call the edit VendorCredential Put api endpoint to edit a vendorCredential
	Then the edit result should be an updated VendorCredential

@VendorCredential 
#CRU[D] - [Post] :: Delete an existing vendorCredential, by passing a populated in vendorCredential object
Scenario: Delete a vendorCredential
	Given the following VendorCredential Delete input
	#use Id from recently added
		| Id | 
		| 0  | 
	When I call the delete VendorCredential Post api endpoint to delete a vendorCredential
	Then the delete result should be an deleted VendorCredential

@VendorCredential 
#Helper - [Get] :: Check for an existing vendorCredential, by passing a vendorCredential Id
Scenario: Check if a vendorCredential exists
	Given the following VendorCredential Id input
		| Id | 
		| 2  | 
	When I call the VendorCredential Exists Get api endpoint by Id to verify if it exists
	Then the VendorCredential exists result should be bool true or false
