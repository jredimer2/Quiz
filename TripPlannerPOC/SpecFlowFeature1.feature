Feature: DisplayTrips
	In order to display list of scheduled trips from given source and destination
	As a site user
	I want to be able to enter Source and Destination


@POC
Scenario: A trip request can be executed and results returned

	Given Phileas is planning a trip
	When he executes a trip plan from 'North Sydney Station' to 'Town Hall Station'
	Then a list of steps should be provided
