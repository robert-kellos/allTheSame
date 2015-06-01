Feature: Resident
	In order to see a list of Residents
	As a Community Administrator
	I want to load a list of Residents

@Resident 
#[C]RUD - [Post] :: Create, Check, GetById, Update and Delete Resident, by passing a newly populated Resident
Scenario: Resident--Add, Check, GetById, Update and Delete Resident
	Given the following Resident Add input
		| CommunityId | PersonId |
		| 17          | 1        | 
	When I call the add Resident Post api endpoint to add a Resident it checks if exists pulls item edits it and deletes it
	Then the add result should be a Resident Id check exists get by id edit and delete with http response returns

@Residents 
#C[R]UD - [Get] :: Retrieve all Residents, without passing anything
Scenario: Resident--Retrieve all Residents
	#Given I am an authenticated user
	When I call the Resident Get api endpoint
	Then the get result should be a list of Residents

@Resident 
#[C]RUD - [Post] :: Create a new Resident, by passing a newly populated Resident
Scenario: Add a Resident
	Given the following Resident Add input
		| CommunityId | PersonId |
		| 17          | 2        |
	When I call the add Resident Post api endpoint to add a Resident
	Then the add result should be a Resident Id

#
#@Resident 
##[C]RUD - [Post] :: Create a new resident, by passing a newly populated resident
#Scenario: Add a resident
#	Given the following Resident Add input
#		| FirstName | LastName | Email   | MobileNumber |
#		| Spec      | Flow     | x@y.com | 800-555-1212 |
#	When I call the add Resident Post api endpoint to add a resident
#	Then the add result should be a Resident Id
#
#@Residents 
##C[R]UD - [Get] :: Retrieve all residents, without passing anything
#Scenario: Retrieve all residents
#	#Given I am an authenticated user
#	When I call the Resident Get api endpoint
#	Then the get result should be a list of residents
#
#@Resident 
##C[R]UD - [Get] :: Retrieve an existing resident, by passing a resident Id
#Scenario: Retrieve a resident by Id
#	Given the following Resident GetById input
#		| Id |
#		| 2  |
#	When I call the Resident Get api endpoint by Id
#	Then the get by id result should be a Resident
#
#@Resident 
##CR[U]D - [Put] :: Update an existing resident, by passing changes populated in resident and its Id
#Scenario: Update a resident
#	Given the following Resident Edit input
#		| Id | FirstName | LastName | Email   | MobileNumber |
#		| 2  | Spec      | Flow     | x@y.com | 800-555-1212 |
#	When I call the edit Resident Put api endpoint to edit a resident
#	Then the edit result should be an updated Resident
#
#@Resident 
##CRU[D] - [Post] :: Delete an existing resident, by passing a populated in resident object
#Scenario: Delete a resident
#	Given the following Resident Delete input
#	#use Id from recently added
#		| Id | 
#		| 0  | 
#	When I call the delete Resident Post api endpoint to delete a resident
#	Then the delete result should be an deleted Resident
#
#@Resident 
##Helper - [Get] :: Check for an existing resident, by passing a resident Id
#Scenario: Check if a resident exists
#	Given the following Resident Id input
#		| Id | 
#		| 2  | 
#	When I call the Resident Exists Get api endpoint by Id to verify if it exists
#	Then the Resident exists result should be bool true or false
