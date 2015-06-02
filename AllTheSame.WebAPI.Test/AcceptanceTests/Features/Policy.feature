Feature: Policy
	In order to see a list of Policies
	As a Community Administrator
	I want to load a list of Policies

@Policy 
#[C]RUD - [Post] :: Create, Check, GetById, Update and Delete Policy, by passing a newly populated Policy
Scenario: Policy--Add, Check, GetById, Update and Delete Policy
	Given the following Policy Add input
		| CommunityId | Description |
		| 1          | SpecFlow    |
	When I call the add Policy Post api endpoint to add a Policy it checks if exists pulls item edits it and deletes it
	Then the add result should be a Policy Id check exists get by id edit and delete with http response returns

@Policys 
#C[R]UD - [Get] :: Retrieve all Policies, without passing anything
Scenario: Policy--Retrieve all Policies
	#Given I am an authenticated user
	When I call the Policy Get api endpoint
	Then the get result should be a list of Policies

@Policy 
#[C]RUD - [Post] :: Create a new Policy, by passing a newly populated Policy
Scenario: Add a Policy
	Given the following Policy Add input
		| CommunityId | Description		|
		| 2          | SpecFlowTest    |
	When I call the add Policy Post api endpoint to add a Policy
	Then the add result should be a Policy Id

#@Policy 
##[C]RUD - [Post] :: Create a new policy, by passing a newly populated policy
#Scenario: Add a policy
#	Given the following Policy Add input
#		| CommunityId | Description |
#		| 17          | SpecFlow    |
#	When I call the add Policy Post api endpoint to add a policy
#	Then the add result should be a Policy Id
#
#@Policys 
##C[R]UD - [Get] :: Retrieve all policies, without passing anything
#Scenario: Retrieve all policies
#	#Given I am an authenticated user
#	When I call the Policy Get api endpoint
#	Then the get result should be a list of policies
#
#@Policy 
##C[R]UD - [Get] :: Retrieve an existing policy, by passing a policy Id
#Scenario: Retrieve a policy by Id
#	Given the following Policy GetById input
#		| Id |
#		| 2  |
#	When I call the Policy Get api endpoint by Id
#	Then the get by id result should be a Policy
#
#@Policy 
##CR[U]D - [Put] :: Update an existing policy, by passing changes populated in policy and its Id
#Scenario: Update a policy
#	Given the following Policy Edit input
#		| Id | CommunityId | Description |
#		| 1  | 17          | SpecFlow    |
#	When I call the edit Policy Put api endpoint to edit a policy
#	Then the edit result should be an updated Policy
#
#@Policy 
##CRU[D] - [Post] :: Delete an existing policy, by passing a populated in policy object
#Scenario: Delete a policy
#	Given the following Policy Delete input
#	#use Id from recently added
#		| Id | 
#		| 0  | 
#	When I call the delete Policy Post api endpoint to delete a policy
#	Then the delete result should be an deleted Policy
#
#@Policy 
##Helper - [Get] :: Check for an existing policy, by passing a policy Id
#Scenario: Check if a policy exists
#	Given the following Policy Id input
#		| Id | 
#		| 2  | 
#	When I call the Policy Exists Get api endpoint by Id to verify if it exists
#	Then the Policy exists result should be bool true or false
