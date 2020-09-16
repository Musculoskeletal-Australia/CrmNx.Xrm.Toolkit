﻿using System.Threading.Tasks;
using CrmNx.Xrm.Toolkit.Messages;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace CrmNx.Xrm.Toolkit.FunctionalTests.Functional.Messages
{

    public class QueryScheduleTestsBase : IntegrationTestBase
    {
        public QueryScheduleTestsBase(StartupFixture fixture, ITestOutputHelper outputHelper)
            : base(fixture, outputHelper) { }

        [Fact()]
        public async Task Execute_QuerySchedule_When_Resource_Is_CurrentUser_Then_ResultOk()
        {
            var request = new QueryScheduleRequest()
            {
                ResourceId = CrmClient.GetMyCrmUserId(),
                Start = System.DateTime.Now.Date,
                End = System.DateTime.Now.Date.AddDays(1).AddSeconds(-1),
                TimeCodes = new TimeCode[] { TimeCode.Available }
            };

            var response = await CrmClient.ExecuteFunctionAsync<QueryScheduleResponse>(request);

            response.TimeInfos.Should().NotBeNull();
            response.TimeInfos.Should().NotBeEmpty();
        }
    }
}
