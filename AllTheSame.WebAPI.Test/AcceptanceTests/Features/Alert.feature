Feature: Alert
	In order to see a list of alerts
	As a Community Administrator
	I want to load a list of alerts

@Alert 
#[C]RUD - [Post] :: Create, Check, GetById, Update and Delete alert, by passing a newly populated alert
Scenario: Alert--Add, Check, GetById, Update and Delete alert
	Given the following Alert Add input
		| Description | AlertTypeId |
		| SpecFlow    | 1           |
	When I call the add Alert Post api endpoint to add a alert it checks if exists pulls item edits it and deletes it
	Then the add result should be a Alert Id check exists get by id edit and delete with http response returns

@Alerts 
#C[R]UD - [Get] :: Retrieve all alerts, without passing anything
Scenario: Alert--Retrieve all alerts
	#Given I am an authenticated user
	When I call the Alert Get api endpoint
	Then the get result should be a list of alerts
#
#@Alert 
##C[R]UD - [Get] :: Retrieve an existing alert, by passing a alert Id
#Scenario: Retrieve a alert by Id
#	Given the following Alert GetById input
#		| Id |
#		| 3  |
#	When I call the Alert Get api endpoint by Id
#	Then the get by id result should be a Alert
#
#@Alert 
##CR[U]D - [Put] :: Update an existing alert, by passing changes populated in alert and its Id
#Scenario: Update a alert
#	Given the following Alert Edit input
#		| Id | Description		|
#		| 3  | SpecFlow_Update  |
#	When I call the edit Alert Put api endpoint to edit a alert
#	Then the edit result should be an updated Alert
#
#@Alert 
##CRU[D] - [Post] :: Delete an existing alert, by passing a populated in alert object
#Scenario: Delete a alert
#	Given the following Alert Delete input
#	#use Id from recently added
#		| Id | 
#		| 0  | 
#	When I call the delete Alert Post api endpoint to delete a alert
#	Then the delete result should be an deleted Alert
#
#@Alert 
##Helper - [Get] :: Check for an existing alert, by passing a alert Id
#Scenario: Check if a alert exists
#	Given the following Alert Id input
#		| Id | 
#		| 3  | 
#	When I call the Alert Exists Get api endpoint by Id to verify if it exists
#	Then the Alert exists result should be bool true or false
