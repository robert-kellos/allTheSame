Feature: AppointmentType
	In order to see a list of appointmentTypes
	As a Community Administrator
	I want to load a list of appointmentTypes

@AppointmentType 
#[C]RUD - [Post] :: Create, Check, GetById, Update and Delete AppointmentType, by passing a newly populated AppointmentType
Scenario: AppointmentType--Add, Check, GetById, Update and Delete AppointmentType
	Given the following AppointmentType Add input
		| Code		  | Label	|
		| SpecFlow    | test		|
	When I call the add AppointmentType Post api endpoint to add a AppointmentType it checks if exists pulls item edits it and deletes it
	Then the add result should be a AppointmentType Id check exists get by id edit and delete with http response returns

@AppointmentTypes 
#C[R]UD - [Get] :: Retrieve all AppointmentTypes, without passing anything
Scenario: AppointmentType--Retrieve all AppointmentTypes
	#Given I am an authenticated user
	When I call the AppointmentType Get api endpoint
	Then the get result should be a list of AppointmentTypes

#@AppointmentType 
##[C]RUD - [Post] :: Create a new appointmentType, by passing a newly populated appointmentType
#Scenario: Add a appointmentType
#	Given the following AppointmentType Add input
#		| Code			| Label				| 
#		| SpecFlowCode  | SpecFlowLabel     |
#	When I call the add AppointmentType Post api endpoint to add a appointmentType
#	Then the add result should be a AppointmentType Id
#
#@AppointmentTypes 
##C[R]UD - [Get] :: Retrieve all appointmentTypes, without passing anything
#Scenario: Retrieve all appointmentTypes
#	#Given I am an authenticated user
#	When I call the AppointmentType Get api endpoint
#	Then the get result should be a list of appointmentTypes
#
#@AppointmentType 
##C[R]UD - [Get] :: Retrieve an existing appointmentType, by passing a appointmentType Id
#Scenario: Retrieve a appointmentType by Id
#	Given the following AppointmentType GetById input
#		| Id |
#		| 2  |
#	When I call the AppointmentType Get api endpoint by Id
#	Then the get by id result should be a AppointmentType
#
#@AppointmentType 
##CR[U]D - [Put] :: Update an existing appointmentType, by passing changes populated in appointmentType and its Id
#Scenario: Update a appointmentType
#	Given the following AppointmentType Edit input
#		| Id | Code					| Label				| 
#		| 2  | SpecFlowCode_Update  | SpecFlowLabel     |
#	When I call the edit AppointmentType Put api endpoint to edit a appointmentType
#	Then the edit result should be an updated AppointmentType
#
#@AppointmentType 
##CRU[D] - [Post] :: Delete an existing appointmentType, by passing a populated in appointmentType object
#Scenario: Delete a appointmentType
#	Given the following AppointmentType Delete input
#	#use Id from recently added
#		| Id | 
#		| 0  | 
#	When I call the delete AppointmentType Post api endpoint to delete a appointmentType
#	Then the delete result should be an deleted AppointmentType
#
#@AppointmentType 
##Helper - [Get] :: Check for an existing appointmentType, by passing a appointmentType Id
#Scenario: Check if a appointmentType exists
#	Given the following AppointmentType Id input
#		| Id | 
#		| 2  | 
#	When I call the AppointmentType Exists Get api endpoint by Id to verify if it exists
#	Then the AppointmentType exists result should be bool true or false
