using System;
using System.Collections.Generic;
using Swagometer.BlackBox.Specs.TestData;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Swagometer.BlackBox.Specs.Steps
{
    [Binding]
    public class DistributeSwagToAttendeesSteps
    {
        private readonly TestRunContext _context;
        private IEnumerable<TestSwagItem> _swagItems;

        public DistributeSwagToAttendeesSteps(TestRunContext context)
        {
            _context = context;
        }

        [Given(@"the following swag items")]
        public void GivenTheFollowingSwagItems(IEnumerable<TestSwagItem> swagtItems)
        {
            _swagItems = swagtItems;
        }

        [Given(@"the following meeting attendees")]
        public void GivenTheFollowingMeetingAttendees(Table table)
        {
            ScenarioContext.Current.Pending();
        }

        [StepArgumentTransformation]
        public IEnumerable<TestSwagItem> TransformSwagItem(Table swagItemsTable)
        {
            return swagItemsTable.CreateSet<TestSwagItem>();
        }
    }
}
