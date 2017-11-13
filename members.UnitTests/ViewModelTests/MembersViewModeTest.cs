using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using members.Core.Models;
using members.Core.Services.Network;
using members.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Test.Core;
using NUnit.Framework;
using FluentAssertions;
using Moq;
using Acr.UserDialogs;

namespace members.UnitTests.ViewModelTests
{
    [TestFixture()]
    public class MembersViewModeTest : MvxIoCSupportingTest
    {
        [SetUp]
        public void Init()
        {
            base.Setup();
        }

        [Test()]
        public async Task FirstCallToServiceToGetData()
        {
            // arrange
            var mockDialogs = new Mock<IUserDialogs>();
            var membersViewModel = new MembersViewModel(new MockAPIService(), mockDialogs.Object);

            // act
            await membersViewModel.NewSearch(1);

            // assert
            membersViewModel.Members.Count.ShouldBeEquivalentTo(MockAPIService.LIMIT_PER_PAGE, 
                                                                because: "the mock data will only return max limit");
        }

        [Test()]
        public async Task SearchToFilterMembersByEmail()
        {
            // arrange
            var mockDialogs = new Mock<IUserDialogs>();
            var membersViewModel = new MembersViewModel(new MockAPIService(), mockDialogs.Object);

            // act
            await membersViewModel.SearchMembers(MockAPIService.TEST_EMAIL);

            // assert
            membersViewModel.Members.Count.ShouldBeEquivalentTo((MockAPIService.LIMIT_PER_PAGE), 
                                                                because: "should be more then page limit");
        }

        [Test()]
        public async Task SearchToFilterMembersByEmailAndPaging()
        {
            // arrange
            var mockDialogs = new Mock<IUserDialogs>();
            var membersViewModel = new MembersViewModel(new MockAPIService(), mockDialogs.Object);
            await membersViewModel.SearchMembers(MockAPIService.TEST_EMAIL);
            await membersViewModel.NextPage();

            // act
            await membersViewModel.NextPage();

            // assert
            membersViewModel.Members.Count.ShouldBeEquivalentTo((MockAPIService.NUM_USERS / 2),
                                                                because: "Half of the users sould have that email");
        }

        [Test()]
        public async Task ResettingDataAfterSearchIsComplete()
        {
            // arrange
            var mockDialogs = new Mock<IUserDialogs>();
            var membersViewModel = new MembersViewModel(new MockAPIService(), mockDialogs.Object);
            await membersViewModel.SearchMembers(MockAPIService.TEST_EMAIL);

            // act
            await membersViewModel.EndSearch();

            // assert
            membersViewModel.Members.Count.ShouldBeEquivalentTo(MockAPIService.LIMIT_PER_PAGE,
                                                                because: "the data should reset back to first original page");
        }

        [Test()]
        public async Task RequestingAnotherPageOfResults()
        {
            // arrange
            var mockDialogs = new Mock<IUserDialogs>();
            var membersViewModel = new MembersViewModel(new MockAPIService(), mockDialogs.Object);
            await membersViewModel.NewSearch(1);

            // act
            await membersViewModel.NextPage();

            // assert
            membersViewModel.Members.Count.ShouldBeEquivalentTo(MockAPIService.LIMIT_PER_PAGE * 2,
                                                                because: "the data should have two pages");
        }

        [Test()]
        public async Task RequestingAnotherPageOfResultsOnLastPageLeft()
        {
            // arrange
            var mockDialogs = new Mock<IUserDialogs>();
            var membersViewModel = new MembersViewModel(new MockAPIService(), mockDialogs.Object);
            await membersViewModel.NewSearch(1);
            await membersViewModel.NextPage();
            await membersViewModel.NextPage();
            await membersViewModel.NextPage();

            // act
            await membersViewModel.NextPage();

            // assert
            membersViewModel.Members.Count.ShouldBeEquivalentTo(MockAPIService.NUM_USERS,
                                                                because: "all data should be retrieved");
        }

        [Test()]
        public async Task RequestingAnotherPageOfResultsWheThereAreNoMorePages()
        {
            // arrange
            var mockDialogs = new Mock<IUserDialogs>();
            var membersViewModel = new MembersViewModel(new MockAPIService(), mockDialogs.Object);
            await membersViewModel.NewSearch(1);
            await membersViewModel.NextPage();
            await membersViewModel.NextPage();
            await membersViewModel.NextPage();
            await membersViewModel.NextPage();

            // act
            await membersViewModel.NextPage();

            // assert
            membersViewModel.Members.Count.ShouldBeEquivalentTo(MockAPIService.NUM_USERS,
                                                               because: "all data should be retrieved and no more");
        }

        public class MockAPIService : IAPIService
        {
            public const int NUM_USERS = 220;
            public const int LIMIT_PER_PAGE = 50;
            public const string TEST_EMAIL = "changeUnitTest@gmail.com";

            /// <summary>
            /// Generate some fake data of members
            /// </summary>
            /// <returns>The members.</returns>
            /// <param name="page">Page.</param>
            /// <param name="email">Email.</param>
            /// <param name="firstName">First name.</param>
            /// <param name="surname">Surname.</param>
            public Task<ResponseWrapper<List<Member>>> GetMembers(int page, string email = null,
                                                           string firstName = null, string surname = null)
            {
                var list = new List<Member>();
                for (int i = 0; i < NUM_USERS; i++)
                {
                    var member = new Member();
                    member.FirstName = "Test";
                    member.LastName = "User";
                    member.Email = (i%2 == 0) ? "testing123@gmail.com" : TEST_EMAIL;
                    list.Add(member);
                }

                // filter if email supplied like web
                if (!string.IsNullOrEmpty(email))
                {
                    list = list.FindAll((Member obj) => obj.Email == email);
                }

                int startingPos = (LIMIT_PER_PAGE * page) - 50;
                int range = Math.Min(list.Count - startingPos, LIMIT_PER_PAGE);
                var pageList = list.GetRange(startingPos, range);

                bool partialContent = ((startingPos + LIMIT_PER_PAGE) < list.Count);

                return Task.FromResult(new ResponseWrapper<List<Member>>(pageList, partialContent));
            }
        }
    }
}