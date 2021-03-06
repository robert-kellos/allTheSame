﻿Feature: Person
	In order to see a list of people
	As a Community Administrator
	I want to load a list of people

@Person 
#[C]RUD - [Post] :: Create, Check, GetById, Update and Delete Person, by passing a newly populated Person
Scenario: Person--Add, Check, GetById, Update and Delete Person
	Given the following Person Add input
		| FirstName | LastName | Email   | MobileNumber |
		| Spec      | Flow     | x@y.com | 800-555-1212 |
	When I call the add Person Post api endpoint to add a Person it checks if exists pulls item edits it and deletes it
	Then the add result should be a Person Id check exists get by id edit and delete with http response returns

@People 
#C[R]UD - [Get] :: Retrieve all People, without passing anything
Scenario: Person--Retrieve all People
	#Given I am an authenticated user
	When I call the Person Get api endpoint
	Then the get result should be a list of People

@Person 
#[C]RUD - [Post] :: Create a new Person, by passing a newly populated Person
Scenario: Add a Person
	Given the following Person Add input
		| FirstName | LastName | Email   | MobileNumber |
		| Spec      | Flow     | x@y.com | 800-555-1212 | 
	When I call the add Person Post api endpoint to add a Person
	Then the add result should be a Person Id



#@Person 
##[C]RUD - [Post] :: Create a new person, by passing a newly populated person
#Scenario: Add a person
#	Given the following Person Add input
		#| FirstName | LastName | Email   | MobileNumber |
		#| Spec      | Flow     | x@y.com | 800-555-1212 |
#	When I call the add Person Post api endpoint to add a person
#	Then the add result should be a Person Id
#
#@People 
##C[R]UD - [Get] :: Retrieve all People, without passing anything
#Scenario: Retrieve all people
#	#Given I am an authenticated user
#	When I call the Person Get api endpoint
#	Then the get result should be a list of people
#
#@Person 
##C[R]UD - [Get] :: Retrieve an existing person, by passing a person Id
#Scenario: Retrieve a person by Id
#	Given the following Person GetById input
#		| Id |
#		| 1  |
#	When I call the Person Get api endpoint by Id
#	Then the get by id result should be a Person
#
#@Person 
##CR[U]D - [Put] :: Update an existing person, by passing changes populated in person and its Id
#Scenario: Update a person
#	Given the following Person Edit input
#		| Id | FirstName | LastName | Email   | MobileNumber |
#		| 1  | Spec      | Flow     | x@y.com | 800-555-1212 |
#	When I call the edit Person Put api endpoint to edit a person
#	Then the edit result should be an updated Person
#
#@Person 
##CRU[D] - [Post] :: Delete an existing person, by passing a populated in person object
#Scenario: Delete a person
#	Given the following Person Delete input
#	#use Id from recently added
#		| Id | 
#		| 0  | 
#	When I call the delete Person Post api endpoint to delete a person
#	Then the delete result should be an deleted Person
#
#@Person 
##Helper - [Get] :: Check for an existing person, by passing a person Id
#Scenario: Check if a person exists
#	Given the following Person Id input
#		| Id | 
#		| 2  | 
#	When I call the Person Exists Get api endpoint by Id to verify if it exists
#	Then the Person exists result should be bool true or false
