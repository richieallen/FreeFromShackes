using System.Collections.Generic;
using Should;
using Swagometer.BlackBox.Specs.Pages;
using Swagometer.BlackBox.Specs.TestData;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Swagometer.BlackBox.Specs.Steps
{
    [Binding]
    class SwagEmSteps
    {
        private readonly TestRunContext _context;
        private readonly SwagEmPage _swagEmPage;

        public SwagEmSteps(TestRunContext context, SwagEmPage swagEmPage)
        {
            _context = context;
            _swagEmPage = swagEmPage;
        }

        [Given(@"the following swag items")]
        public void GivenTheFollowingSwagItems(IEnumerable<TestSwagItem> swagItems)
        {
            _context.SwagItems = swagItems;
        }

        [Given(@"the following meeting attendees")]
        public void GivenTheFollowingMeetingAttendees(IEnumerable<TestAttendee> attendees)
        {
            _context.Attendees = attendees;
        }

        [When(@"I choose to give away swag")]
        public void WhenIChooseToGiveAwaySwag()
        {
            _swagEmPage.GiveAwaySwag();
        }

        [Then(@"the supplier of the swag item should be clearly visible")]
        public void ThenTheSupplierOfTheSwagItemShouldBeClearlyVisible()
        {
            _swagEmPage.ItemSupplierIsVisible().ShouldBeTrue();
        }

        [Then(@"the name of the swag item should be clearly visible")]
        public void ThenTheNameOfTheSwagItemShouldBeClearlyVisible()
        {
            _swagEmPage.ItemNameIsVisible().ShouldBeTrue();
        }

        [Then(@"the name of the winning attendee should be clearly visible")]
        public void ThenTheNameOfTheWinningAttendeeShouldBeClearlyVisible()
        {
            _swagEmPage.AttendeeIsVisible().ShouldBeTrue();
        }

        [Then(@"the attendee should receive the swag item")]
        public void ThenTheAttendeeShouldReceiveTheSwagItem()
        {
            // Do nothing
        }

        [Then(@"the swag item should be removed from the available swag list")]
        public void ThenTheSwagItemShouldBeRemovedFromTheAvailableSwagList()
        {
            // Do nothing
        }


        [StepArgumentTransformation]
        public IEnumerable<TestSwagItem> TransformSwagItems(Table swagItemsTable)
        {
            return swagItemsTable.CreateSet<TestSwagItem>();
        }

        [StepArgumentTransformation]
        public IEnumerable<TestAttendee> TransformAttendeess(Table attendeesTable)
        {
            return attendeesTable.CreateSet<TestAttendee>();
        }
    }
}
