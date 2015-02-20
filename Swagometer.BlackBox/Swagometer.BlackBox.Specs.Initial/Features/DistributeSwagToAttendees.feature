Feature: Distribute Swag To Attendees 1
	In order to ensure swag is distributed to meeting attendees in a fair way
	As a user group organiser
	I want to be able to randomly choose members to receive swag items

Background:
	Given the following swag items
	| Name               | Supplied by |
	| Resharper          | JetBrains   |
	| £10 Amazon Voucher | Spectrum IT |
	And the following meeting attendees
	| First Name | Last Name |
	| Mark       | Jones     |
	| Pete       | Burgess   |

Scenario: Attendee is chosen to receive swag
	When I choose to give away swag
	Then the supplier of the swag item should be clearly visible 
	And the name of the swag item should be clearly visible 
	And the name of the winning attendee should be clearly visible
	And the attendee should receive the swag item
	And the swag item should be removed from the available swag list

Scenario Outline: Only loyal paying members should receive special swag
	Given a swag item <SwagName> with availability <SwagAvailability>
	And an attendee with a status of '<MemberStatus>'
	When I choose to give away swag
	Then the attendee <Eligibility> eligible to win the swag

Examples:
	| SwagName | SuppliedBy            | SwagAvailability | MemberStatus | Eligibility   |
	| XBox     | Developer South Coast | member           | member       | should be     |
	| eBook    | Manning               | non-member       | member       | should be     |
	| XBox     | Developer South Coast | member           | non-member   | should not be |
	| eBook    | Manning               | non-member       | non-member   | should be     |

Scenario: Attendee that has already received swag should not receive more swag
	Given 'Mark Jones' has already received the 'Resharper' swag item
	When I choose to give away swag
	Then 'Mark Jones' should not be chosen to receive any more swag
	And 'Pete Burgess' should receive the '£10 Amazon Voucher' swag item

Scenario: An attendee that declines a swag item should not be chosen to win the same swag item
	Given the 'Resharper' swag item is the only swag left 
	And 'Mark Jones' has previously declined receiving the 'Resharper' swag item
	When I choose to give away swag
	Then 'Mark Jones' should not be chosen to win 
	And 'Pete Burgess' should receive the 'Resharper' swag item