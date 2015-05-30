Feature: Appointment
	In order to see a list of appointments
	As a Community Administrator
	I want to load a list of appointments

@Appointment 
#[C]RUD - [Post] :: Create, Check, GetById, Update and Delete Appointment, by passing a newly populated Appointment
Scenario: Appointment--Add, Check, GetById, Update and Delete Appointment
	Given the following Appointment Add input
		| Description	|
		| SpecFlow Test	|
	When I call the add Appointment Post api endpoint to add a Appointment it checks if exists pulls item edits it and deletes it
	Then the add result should be a Appointment Id check exists get by id edit and delete with http response returns

@Appointments 
#C[R]UD - [Get] :: Retrieve all Appointments, without passing anything
Scenario: Appointment--Retrieve all Appointments
	#Given I am an authenticated user
	When I call the Appointment Get api endpoint
	Then the get result should be a list of Appointments

#@Appointment 
##[C]RUD - [Post] :: Create a new appointment, by passing a newly populated appointment
#Scenario: Add a appointment
#	Given the following Appointment Add input
#		| Description	|
#		| SpecFlow Test	|
#	When I call the add Appointment Post api endpoint to add a appointment
#	Then the add result should be a Appointment Id
#
#@Appointments 
##C[R]UD - [Get] :: Retrieve all appointments, without passing anything
#Scenario: Retrieve all appointments
#	#Given I am an authenticated user
#	When I call the Appointment Get api endpoint
#	Then the get result should be a list of appointments
#
#@Appointment 
##C[R]UD - [Get] :: Retrieve an existing appointment, by passing a appointment Id
#Scenario: Retreive a appointment by Id
#	Given the following Appointment GetById input
#		| Id |
#		| 2  |
#	When I call the Appointment Get api endpoint by Id
#	Then the get by id result should be a Appointment
#
#@Appointment 
##CR[U]D - [Put] :: Update an existing appointment, by passing changes populated in appointment and its Id
#Scenario: Update a appointment
#	Given the following Appointment Edit input
#		| Id | Description      |
#		| 2  | Spec Flow Update |
#	When I call the edit Appointment Put api endpoint to edit a appointment
#	Then the edit result should be an updated Appointment
#
#@Appointment 
##CRU[D] - [Post] :: Delete an existing appointment, by passing a populated in appointment object
#Scenario: Delete a appointment
#	Given the following Appointment Delete input
#	#use Id from recently added
#		| Id | 
#		| 0  | 
#	When I call the delete Appointment Post api endpoint to delete a appointment
#	Then the delete result should be an deleted Appointment
#
#@Appointment 
##Helper - [Get] :: Check for an existing appointment, by passing a appointment Id
#Scenario: Check if a appointment exists
#	Given the following Appointment Id input
#		| Id | 
#		| 2  | 
#	When I call the Appointment Exists Get api endpoint by Id to verify if it exists
#	Then the Appointment exists result should be bool true or false
