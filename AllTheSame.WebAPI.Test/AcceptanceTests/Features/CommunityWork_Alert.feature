Feature: CommunityWorker_Alert
	In order to see a list of CommunityWorker_Alerts
	As a Community Administrator
	I want to load a list of CommunityWorker_Alerts

@CommunityWorker_Alert 
#[C]RUD - [Post] :: Create, Check, GetById, Update and Delete CommunityType, by passing a newly populated CommunityWorker_Alert
Scenario: CommunityWorker_Alert--Add, Check, GetById, Update and Delete CommunityType
	Given the following CommunityWorker_Alert Add input
		| IsRead	| 
		| true		| 
	When I call the add CommunityWorker_Alert Post api endpoint to add a CommunityWorker_Alert it checks if exists pulls item edits it and deletes it
	Then the add result should be a CommunityWorker_Alert Id check exists get by id edit and delete with http response returns

@CommunityWorker_Alerts 
#C[R]UD - [Get] :: Retrieve all CommunityWorker_Alerts, without passing anything
Scenario: CommunityWorker_Alert--Retrieve all CommunityWorker_Alerts
	#Given I am an authenticated user
	When I call the CommunityWorker_Alert Get api endpoint
	Then the get result should be a list of CommunityWorker_Alerts

@CommunityWorker_Alert 
#[C]RUD - [Post] :: Create a new CommunityWorker_Alert, by passing a newly populated CommunityWorker_Alert
Scenario: Add a CommunityWorker_Alert
	Given the following CommunityWorker_Alert Add input
		| IsRead	| 
		| true		| 
	When I call the add CommunityWorker_Alert Post api endpoint to add a CommunityWorker_Alert
	Then the add result should be a CommunityWorker_Alert Id

#@CommunityWorker_Alert 
##[C]RUD - [Post] :: Create a new communityWorker_Alert, by passing a newly populated communityWorker_Alert
#Scenario: Add a communityWorker_Alert
#	Given the following CommunityWorker_Alert Add input
#		| FirstName | LastName | Email   | MobileNumber |
#		| Spec      | Flow     | x@y.com | 800-555-1212 |
#	When I call the add CommunityWorker_Alert Post api endpoint to add a CommunityWorker_Alert
#	Then the add result should be a CommunityWorker_Alert Id
#
#@CommunityWorker_Alerts 
##C[R]UD - [Get] :: Retrieve all communityWorker_Alerts, without passing anything
#Scenario: Retrieve all communityWorker_Alerts
#	#Given I am an authenticated user
#	When I call the CommunityWorker_Alert Get api endpoint
#	Then the get result should be a list of communityWorker_Alerts
#
#@CommunityWorker_Alert 
##C[R]UD - [Get] :: Retrieve an existing communityWorker_Alert, by passing a communityWorker_Alert Id
#Scenario: Retrieve a communityWorker_Alert by Id
#	Given the following CommunityWorker_Alert GetById input
#		| Id |
#		| 2  |
#	When I call the CommunityWorker_Alert Get api endpoint by Id
#	Then the get by id result should be a CommunityWorker_Alert
#
#@CommunityWorker_Alert 
##CR[U]D - [Put] :: Update an existing communityWorker_Alert, by passing changes populated in communityWorker_Alert and its Id
#Scenario: Update a communityWorker_Alert
#	Given the following CommunityWorker_Alert Edit input
#		| Id | FirstName | LastName | Email   | MobileNumber |
#		| 2  | Spec      | Flow     | x@y.com | 800-555-1212 |
#	When I call the edit CommunityWorker_Alert Put api endpoint to edit a communityWorker_Alert
#	Then the edit result should be an updated CommunityWorker_Alert
#
#@CommunityWorker_Alert 
##CRU[D] - [Post] :: Delete an existing communityWorker_Alert, by passing a populated in communityWorker_Alert object
#Scenario: Delete a communityWorker_Alert
#	Given the following CommunityWorker_Alert Delete input
#	#use Id from recently added
#		| Id | 
#		| 0  | 
#	When I call the delete CommunityWorker_Alert Post api endpoint to delete a communityWorker_Alert
#	Then the delete result should be an deleted CommunityWorker_Alert
#
#@CommunityWorker_Alert 
##Helper - [Get] :: Check for an existing communityWorker_Alert, by passing a communityWorker_Alert Id
#Scenario: Check if a communityWorker_Alert exists
#	Given the following CommunityWorker_Alert Id input
#		| Id | 
#		| 2  | 
#	When I call the CommunityWorker_Alert Exists Get api endpoint by Id to verify if it exists
#	Then the CommunityWorker_Alert exists result should be bool true or false
