Feature: CommunityAdmin
	In order to see a list of CommunityAdmin
	As a Community Administrator
	I want to load a list of CommunityAdmin

@CommunityAdmin 
#[C]RUD - [Post] :: Create, Check, GetById, Update and Delete CommunityAdmin, by passing a newly populated CommunityAdmin
Scenario: CommunityAdmin--Add, Check, GetById, Update and Delete CommunityAdmin
	Given the following CommunityAdmin Add input
		| Id   | 
		| 1    | 
	When I call the add CommunityAdmin Post api endpoint to add a CommunityAdmin it checks if exists pulls item edits it and deletes it
	Then the add result should be a CommunityAdmin Id check exists get by id edit and delete with http response returns

@CommunityAdmins 
#C[R]UD - [Get] :: Retrieve all CommunityAdmins, without passing anything
Scenario: CommunityAdmin--Retrieve all CommunityAdmins
	#Given I am an authenticated user
	When I call the CommunityAdmin Get api endpoint
	Then the get result should be a list of communityAdmins

@CommunityAdmin 
#[C]RUD - [Post] :: Create a new CommunityAdmin, by passing a newly populated CommunityAdmin
Scenario: Add a communityAdmin
	Given the following CommunityAdmin Add input
		| Id   | 
		| 1    |
	When I call the add CommunityAdmin Post api endpoint to add a CommunityAdmin
	Then the add result should be a CommunityAdmin Id

#@CommunityAdmins 
##C[R]UD - [Get] :: Retrieve all communityAdmins, without passing anything
#Scenario: Retrieve all communityAdmins
#	#Given I am an authenticated user
#	When I call the CommunityAdmin Get api endpoint
#	Then the get result should be a list of communityAdmins
#
#@CommunityAdmin 
##C[R]UD - [Get] :: Retrieve an existing communityAdmin, by passing a communityAdmin Id
#Scenario: Retrieve a communityAdmin by Id
#	Given the following CommunityAdmin GetById input
#		| Id |
#		| 2  |
#	When I call the CommunityAdmin Get api endpoint by Id
#	Then the get by id result should be a CommunityAdmin
#
#@CommunityAdmin 
##CR[U]D - [Put] :: Update an existing communityAdmin, by passing changes populated in communityAdmin and its Id
#Scenario: Update a communityAdmin
#	Given the following CommunityAdmin Edit input
#		| Id | FirstName | LastName | Email   | MobileNumber |
#		| 2  | Spec      | Flow     | x@y.com | 800-555-1212 |
#	When I call the edit CommunityAdmin Put api endpoint to edit a communityAdmin
#	Then the edit result should be an updated CommunityAdmin
#
#@CommunityAdmin 
##CRU[D] - [Post] :: Delete an existing communityAdmin, by passing a populated in communityAdmin object
#Scenario: Delete a communityAdmin
#	Given the following CommunityAdmin Delete input
#	#use Id from recently added
#		| Id | 
#		| 0  | 
#	When I call the delete CommunityAdmin Post api endpoint to delete a communityAdmin
#	Then the delete result should be an deleted CommunityAdmin
#
#@CommunityAdmin 
##Helper - [Get] :: Check for an existing communityAdmin, by passing a communityAdmin Id
#Scenario: Check if a communityAdmin exists
#	Given the following CommunityAdmin Id input
#		| Id | 
#		| 2  | 
#	When I call the CommunityAdmin Exists Get api endpoint by Id to verify if it exists
#	Then the CommunityAdmin exists result should be bool true or false
