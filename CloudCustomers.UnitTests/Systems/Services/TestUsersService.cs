using Moq;
using Moq.Protected;

using CloudCustomers.API.Models;
using CloudCustomers.API.Services;

using CloudCustomers.UnitTests.Fixtures;
using CloudCustomers.UnitTests.Helpers;
using Xunit;
using FluentAssertions;

namespace CloudCustomers.UnitTests.Systems.Services
{
    public class TestUsersService
    {
		[Fact]
		public async Task GetAllUsers_WhenCalled_InvokesHttpGetRequest()
		{
			var expectedResponse = UsersFixture.GetTestUsers();

			var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);
			var httpClient = new HttpClient(handlerMock.Object);

			var sut = new UsersServices(httpClient);

			await sut.GetAllUsers();

			handlerMock
				.Protected()
				.Verify(
				"SendAsync",
				Times.Exactly(1),
				ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
				ItExpr.IsAny<CancellationToken>()
			);

		}

		[Fact]
		public async Task GetAllUsers_WhenCalled_ReturnsListOfUsers()
		{
            var expectedResponse = UsersFixture.GetTestUsers();

            var handlerMock = MockHttpMessageHandler<User>.SetupReturn404(expectedResponse);
            var httpClient = new HttpClient(handlerMock.Object);

            var sut = new UsersServices(httpClient);

			var response = await sut.GetAllUsers();

			response.Should().BeOfType<List<User>>();

        }
    }
}
