Feature: SignOut
	In order to see a list of SignOuts
	As a Community Administrator
	I want to load a list of SignOuts

#@SignOut 
##[C]RUD - [Post] :: Create, Check, GetById, Update and Delete SignOut, by passing a newly populated SignOut
#Scenario: SignOut--Add, Check, GetById, Update and Delete SignOut
#	Given the following SignOut Add input
#		| Name		  | 
#		| SpecFlow    | 
#	When I call the add SignOut Post api endpoint to add a SignOut it checks if exists pulls item edits it and deletes it
#	Then the add result should be a SignOut Id check exists get by id edit and delete with http response returns

@SignOuts 
#C[R]UD - [Get] :: Retrieve all SignOuts, without passing anything
Scenario: SignOut--Retrieve all SignOuts
	#Given I am an authenticated user
	When I call the SignOut Get api endpoint
	Then the get result should be a list of SignOuts

#@SignOut 
##[C]RUD - [Post] :: Create a new SignOut, by passing a newly populated SignOut
#Scenario: Add a SignOut
#	Given the following SignOut Add input
#		| Name			| 
#		| SpecFlowTest  | 
#	When I call the add SignOut Post api endpoint to add a SignOut
#	Then the add result should be a SignOut Id

#@SignOut 
##[C]RUD - [Post] :: Create a new signOut, by passing a newly populated signOut
#Scenario: Add a signOut
#	Given the following SignOut Add input
#		| FirstName | LastName | Email   | MobileNumber |
#		| Spec      | Flow     | x@y.com | 800-555-1212 |
#	When I call the add SignOut Post api endpoint to add a signOut
#	Then the add result should be a SignOut Id
#
#@SignOuts 
##C[R]UD - [Get] :: Retrieve all signOuts, without passing anything
#Scenario: Retrieve all signOuts
#	#Given I am an authenticated user
#	When I call the SignOut Get api endpoint
#	Then the get result should be a list of signOuts
#
#@SignOut 
##C[R]UD - [Get] :: Retrieve an existing signOut, by passing a signOut Id
#Scenario: Retrieve a signOut by Id
#	Given the following SignOut GetById input
#		| Id |
#		| 2  |
#	When I call the SignOut Get api endpoint by Id
#	Then the get by id result should be a SignOut
#
#@SignOut 
##CR[U]D - [Put] :: Update an existing signOut, by passing changes populated in signOut and its Id
#Scenario: Update a signOut
#	Given the following SignOut Edit input
#		| Id | FirstName | LastName | Email   | MobileNumber |
#		| 2  | Spec      | Flow     | x@y.com | 800-555-1212 |
#	When I call the edit SignOut Put api endpoint to edit a signOut
#	Then the edit result should be an updated SignOut
#
#@SignOut 
##CRU[D] - [Post] :: Delete an existing signOut, by passing a populated in signOut object
#Scenario: Delete a signOut
#	Given the following SignOut Delete input
#	#use Id from recently added
#		| Id | 
#		| 0  | 
#	When I call the delete SignOut Post api endpoint to delete a signOut
#	Then the delete result should be an deleted SignOut
#
#@SignOut 
##Helper - [Get] :: Check for an existing signOut, by passing a signOut Id
#Scenario: Check if a signOut exists
#	Given the following SignOut Id input
#		| Id | 
#		| 2  | 
#	When I call the SignOut Exists Get api endpoint by Id to verify if it exists
#	Then the SignOut exists result should be bool true or false
