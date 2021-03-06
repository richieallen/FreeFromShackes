﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Moq;
using Should;
using Swagometer.Controllers;
using Swagometer.Models;
using Swagometer.Tests.TestData;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Swagometer.Tests.Steps
{
    [Binding]
    public class DistributeSwagToAttendeesSteps
    {
        private readonly TestRunContext _context;
        private readonly SwagEmController _swagEmController;

        public DistributeSwagToAttendeesSteps(TestRunContext context)
        {
            _context = context;
            _swagEmController = new SwagEmController(_context.MockAttendeeService.Object, _context.MockSwagService.Object);
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

        [Given(@"I am on the swagometer home page")]
        public void GivenIAmOnTheSwagometerHomePage()
        {
            _context.CurrentActionResult = _swagEmController.Index();
        }

        [Given(@"'(.*)' has already received the '(.*)' swag item")]
        public void GivenHasAlreadyReceivedTheSwagItem(string attendeeName, string swagName)
        {
            var attendee = _context.Attendees.First(a => a.FullName == attendeeName);
            attendee.HasReceivedSwagItem(swagName);

            var swag = _context.SwagItems.First(s => s.Name == swagName);
            swag.HasBeenSwagged = true;
        }

        [Given(@"'(.*)' has previously declined receiving the '(.*)' swag item")]
        public void GivenHasPreviouslyDeclinedReceivingTheSwagItem(string attendeeName, string swagName)
        {
            var attendee = _context.Attendees.First(a => a.FullName == attendeeName);
            attendee.HasDeclinedReceivingSwagItem(swagName);
        }

        [Given(@"the '(.*)' swag item is the only swag left")]
        public void GivenTheSwagItemIsTheOnlySwagLeft(string swagName)
        {
            _context.SwagItems = _context.SwagItems.Where(s => s.Name == swagName).ToList();
        }


        [When(@"I choose to give away swag")]
        public void WhenIChooseToGiveAwaySwag()
        {
            _context.CurrentActionResult = _swagEmController.SwagEm();
        }

        [Then(@"the supplier of the swag item should be clearly visible")]
        public void ThenTheSupplierOfTheSwagItemShouldBeClearlyVisible()
        {
            var model = GetSwagEmResult();
            model.SwagItem.SuppliedBy.ShouldNotBeNull();
        }

        [Then(@"the name of the swag item should be clearly visible")]
        public void ThenTheNameOfTheSwagItemShouldBeClearlyVisible()
        {
            var model = GetSwagEmResult();
            model.SwagItem.Name.ShouldNotBeNull();
        }

        [Then(@"the name of the winning attendee should be clearly visible")]
        public void ThenTheNameOfTheWinningAttendeeShouldBeClearlyVisible()
        {
            var model = GetSwagEmResult();
            model.Attendee.FullName.ShouldNotBeNull();
        }

        [Then(@"'(.*)' should not be chosen to win")]
        public void ThenShouldNotBeChosenToWin(string attendeeName)
        {
            var model = GetSwagEmResult();
            model.Attendee.FullName.ShouldNotEqual(attendeeName);
        }

        private SwagEmResult GetSwagEmResult()
        {
            var model = ((SwagEmResult)((JsonResult)_context.CurrentActionResult).Data);

            model.ShouldNotBeNull();
            return model;
        }


        [Then(@"'(.*)' should not be chosen to receive any more swag")]
        public void ThenShouldNotBeChosenToReceiveAnyMoreSwag(string attendee)
        {
            var model = GetSwagEmResult();

            model.ShouldNotBeNull();
            model.Attendee.FullName.ShouldNotEqual(attendee);
        }

        [Then(@"the attendee should receive the swag item")]
        public void ThenTheAttendeeShouldReceiveTheSwagItem()
        {
            _context.MockAttendeeService.Verify(x => x.AttendeeReceivedSwag(It.IsAny<Attendee>(), 
                                                                            It.IsAny<SwagItem>()));
        }


        [Then(@"'(.*)' should receive the '(.*)' swag item")]
        public void ThenShouldReceiveTheSwagItem(string attendee, string swag)
        {
            _context.MockAttendeeService.Verify(x => x.AttendeeReceivedSwag(It.Is<Attendee>(a => a.FullName == attendee),
                                                                            It.Is<SwagItem>(s => s.Name == swag)));
        }

        [Then(@"the swag item should be removed from the available swag list")]
        public void ThenTheSwagItemShouldBeRemovedFromTheAvailableSwagList()
        {
            var model = GetSwagEmResult();

            var actualSwag = model.SwagItem;
            actualSwag.ShouldNotBeNull();

            _context.MockSwagService.Verify(x => x.MarkAsSwagged(It.Is<SwagItem>(s => s.Name == actualSwag.Name)));
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
