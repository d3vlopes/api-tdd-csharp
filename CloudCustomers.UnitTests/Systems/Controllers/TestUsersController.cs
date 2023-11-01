using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

using Xunit;
using Moq;

using CloudCustomers.API.Controllers;
using CloudCustomers.API.Services;
using CloudCustomers.API.Models;
using CloudCustomers.UnitTests.Fixtures;

namespace CloudCustomers.UnitTests.Systems.Controllers;

public class TestUsersController
{
    private (UsersController sut, Mock<IUsersService> mockService) MakeSut()
    {
        var mockService = new Mock<IUsersService>();
        var sut = new UsersController(mockService.Object);

        return (sut, mockService);
    }

    [Fact]
    public async Task Get_OnSuccess_ReturnsStatusCode200()
    {
        var factory = MakeSut();

        var (sut, mockService) = factory;

        mockService
           .Setup(service => service.GetAllUsers())
           .ReturnsAsync(UsersFixture.GetTestUsers());

        var response = await sut.Get();

        var objectResult = (OkObjectResult)response;

        objectResult.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task Get_OnSuccess_InvokesUserServiceExactlyOnce()
    {
        var factory = MakeSut();

        var (sut, mockService) = factory;

        mockService
            .Setup(service => service.GetAllUsers())
            .ReturnsAsync(new List<User>());


        var response = await sut.Get();

        mockService.Verify(service => service.GetAllUsers(), Times.Once());
    }

    [Fact]
    public async Task Get_OnSuccess_ReturnsListOfUsers()
    {
        var factory = MakeSut();

        var (sut, mockService) = factory;

        mockService
            .Setup(service => service.GetAllUsers())
            .ReturnsAsync(UsersFixture.GetTestUsers());

        var response = await sut.Get();


        response.Should().BeOfType<OkObjectResult>();

        var objectResult = (OkObjectResult)response;

        objectResult.Value.Should().BeOfType<List<User>>();

    }

    [Fact]
    public async Task Get_OnNoUsersFound_ReturnNotFound()
    {
        var factory = MakeSut();

        var (sut, mockService) = factory;

        mockService
            .Setup(service => service.GetAllUsers())
            .ReturnsAsync(new List<User>());

        var response = await sut.Get();

        response.Should().BeOfType<NotFoundResult>();
    }
}