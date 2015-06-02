Feature: User
	In order to see a list of Users
	As a Community Administrator
	I want to load a list of Users

#@User 
##[C]RUD - [Post] :: Create, Check, GetById, Update and Delete User, by passing a newly populated User
#Scenario: User--Add, Check, GetById, Update and Delete User
#	Given the following User Add input
#		| PersonId	| Username	  |
#		| 1			| SpecFlow    | 
#	When I call the add User Post api endpoint to add a User it checks if exists pulls item edits it and deletes it
#	Then the add result should be a User Id check exists get by id edit and delete with http response returns
#
#@Users 
##C[R]UD - [Get] :: Retrieve all Users, without passing anything
#Scenario: User--Retrieve all Users
#	#Given I am an authenticated user
#	When I call the User Get api endpoint
#	Then the get result should be a list of Users
#
#@User 
##[C]RUD - [Post] :: Create a new User, by passing a newly populated User
#Scenario: Add a User
#	Given the following User Add input
#		| PersonId	| Username		  |
#		| 2			| SpecFlowTest    | 
#	When I call the add User Post api endpoint to add a User
#	Then the add result should be a User Id

#@User 
##[C]RUD - [Post] :: Create a new user, by passing a newly populated user
#Scenario: Add a user
#	Given the following User Add input
#		| FirstName | LastName | Email   | MobileNumber |
#		| Spec      | Flow     | x@y.com | 800-555-1212 |
#	When I call the add User Post api endpoint to add a user
#	Then the add result should be a User Id
#
#@Users 
##C[R]UD - [Get] :: Retrieve all users, without passing anything
#Scenario: Retrieve all users
#	#Given I am an authenticated user
#	When I call the User Get api endpoint
#	Then the get result should be a list of users
#
#@User 
##C[R]UD - [Get] :: Retrieve an existing user, by passing a user Id
#Scenario: Retrieve a user by Id
#	Given the following User GetById input
#		| Id |
#		| 2  |
#	When I call the User Get api endpoint by Id
#	Then the get by id result should be a User
#
#@User 
##CR[U]D - [Put] :: Update an existing user, by passing changes populated in user and its Id
#Scenario: Update a user
#	Given the following User Edit input
#		| Id | FirstName | LastName | Email   | MobileNumber |
#		| 2  | Spec      | Flow     | x@y.com | 800-555-1212 |
#	When I call the edit User Put api endpoint to edit a user
#	Then the edit result should be an updated User
#
#@User 
##CRU[D] - [Post] :: Delete an existing user, by passing a populated in user object
#Scenario: Delete a user
#	Given the following User Delete input
#	#use Id from recently added
#		| Id | 
#		| 0  | 
#	When I call the delete User Post api endpoint to delete a user
#	Then the delete result should be an deleted User
#
#@User 
##Helper - [Get] :: Check for an existing user, by passing a user Id
#Scenario: Check if a user exists
#	Given the following User Id input
#		| Id | 
#		| 2  | 
#	When I call the User Exists Get api endpoint by Id to verify if it exists
#	Then the User exists result should be bool true or false
