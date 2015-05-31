Feature: Address
	In order to see a list of addresses
	As a Community Administrator
	I want to load a list of addresses


@Address 
#[C]RUD - [Post] :: Create, Check, GetById, Update and Delete Address, by passing a newly populated Address
Scenario: Address--Add, Check, GetById, Update and Delete Address
	Given the following Address Add input
		| Line1    | Line2     | City		  | State	| Country	| PostalCode |
		| Spec Ave | Flow Blvd | NewCity      |  NY     |	USA		|10221		 |
	When I call the add Address Post api endpoint to add a Address it checks if exists pulls item edits it and deletes it
	Then the add result should be a Address Id check exists get by id edit and delete with http response returns

@Addresses 
#C[R]UD - [Get] :: Retrieve all addresses, without passing anything
Scenario: Retrieve all addresses
	#Given I am an authenticated user
	When I call the Address Get api endpoint
	Then the get result should be a list of addresses

#@Address 
##[C]RUD - [Post] :: Create a new address, by passing a newly populated address
#Scenario: Add a address
#	Given the following Address Add input
#		| Line1    | Line2     | City		  | State	| Country	| PostalCode |
#		| Spec Ave | Flow Blvd | NewCity      |  NY     |	USA		|10221		 |
#	When I call the add Address Post api endpoint to add a address
#	Then the add result should be a Address Id
#
#@Addresses 
##C[R]UD - [Get] :: Retrieve all addresses, without passing anything
#Scenario: Retrieve all addresses
#	#Given I am an authenticated user
#	When I call the Address Get api endpoint
#	Then the get result should be a list of addresses
#
#@Address 
##C[R]UD - [Get] :: Retrieve an existing address, by passing a address Id
#Scenario: Retrieve a address by Id
#	Given the following Address GetById input
#		| Id |
#		| 2  |
#	When I call the Address Get api endpoint by Id
#	Then the get by id result should be a Address
#
#@Address 
##CR[U]D - [Put] :: Update an existing address, by passing changes populated in address and its Id
#Scenario: Update a address
#	Given the following Address Edit input
#		| Id | Line1	| Line2		| City		   | State	| Country | PostalCode |
#		| 2	 | Spec Ave | Flow Blvd | NewCity      |  NY    |	USA	  |10221	   |
#	When I call the edit Address Put api endpoint to edit a address
#	Then the edit result should be an updated Address
#
#@Address 
##CRU[D] - [Post] :: Delete an existing address, by passing a populated in address object
#Scenario: Delete a address
#	Given the following Address Delete input
#	#use Id from recently added
#		| Id | 
#		| 0  | 
#	When I call the delete Address Post api endpoint to delete a address
#	Then the delete result should be an deleted Address
#
#@Address 
##Helper - [Get] :: Check for an existing address, by passing a address Id
#Scenario: Check if a address exists
#	Given the following Address Id input
#		| Id | 
#		| 2  | 
#	When I call the Address Exists Get api endpoint by Id to verify if it exists
#	Then the Adress exists result should be bool true or false
