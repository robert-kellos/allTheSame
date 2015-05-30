Feature: Authenticate
	Verify user credentials 

@mytag
Scenario: User submits valid login credentials
	Given I have entered valid credentials
		| Username			| Password				|
		| super@admin.net	| testpasswordA1_		|
	When I post data to the Token endpoint
	Then the result is an access_token
