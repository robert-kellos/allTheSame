Feature: FamilyMember
	In order to see a list of familyMembers
	As a Community Administrator
	I want to load a list of familyMembers

@FamilyMember 
#[C]RUD - [Post] :: Create a new familyMember, by passing a newly populated familyMember
Scenario: Add a familyMember
	Given the following FamilyMember Add input
		| FirstName | LastName | Email   | MobileNumber |
		| Spec      | Flow     | x@y.com | 800-555-1212 |
	When I call the add FamilyMember Post api endpoint to add a familyMember
	Then the add result should be a FamilyMember Id

@FamilyMembers 
#C[R]UD - [Get] :: Retrieve all familyMembers, without passing anything
Scenario: Retrieve all familyMembers
	#Given I am an authenticated user
	When I call the FamilyMember Get api endpoint
	Then the get result should be a list of familyMembers

@FamilyMember 
#C[R]UD - [Get] :: Retrieve an existing familyMember, by passing a familyMember Id
Scenario: Retreive a familyMember by Id
	Given the following FamilyMember GetById input
		| Id |
		| 2  |
	When I call the FamilyMember Get api endpoint by Id
	Then the get by id result should be a FamilyMember

@FamilyMember 
#CR[U]D - [Put] :: Update an existing familyMember, by passing changes populated in familyMember and its Id
Scenario: Update a familyMember
	Given the following FamilyMember Edit input
		| Id | FirstName | LastName | Email   | MobileNumber |
		| 2  | Spec      | Flow     | x@y.com | 800-555-1212 |
	When I call the edit FamilyMember Put api endpoint to edit a familyMember
	Then the edit result should be an updated FamilyMember

@FamilyMember 
#CRU[D] - [Post] :: Delete an existing familyMember, by passing a populated in familyMember object
Scenario: Delete a familyMember
	Given the following FamilyMember Delete input
	#use Id from recently added
		| Id | 
		| 0  | 
	When I call the delete FamilyMember Post api endpoint to delete a familyMember
	Then the delete result should be an deleted FamilyMember

@FamilyMember 
#Helper - [Get] :: Check for an existing familyMember, by passing a familyMember Id
Scenario: Check if a familyMember exists
	Given the following FamilyMember Id input
		| Id | 
		| 2  | 
	When I call the FamilyMember Exists Get api endpoint by Id to verify if it exists
	Then the FamilyMember exists result should be bool true or false
