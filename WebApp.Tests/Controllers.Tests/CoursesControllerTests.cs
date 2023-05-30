using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApp.Controllers;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Tests.Controllers.Tests;

public class CoursesControllerTests
{
    private readonly CoursesController _controller;
    public CoursesControllerTests()
    {
        // Arrange
        var mockService = new Mock<IReadable<Course, Group>>();
        mockService.Setup(x => x.GetAll())
            .Returns(MockDataHelper.GetCourses);
        mockService.Setup(x => x.GetCollection(It.IsAny<int>()))
            .Returns<int>(MockDataHelper.GetGroupsOfCourseById);
        
        _controller = new CoursesController(mockService.Object);
    }

    [Fact]
    public void IndexTest()
    {
        // Act
        var result = _controller.Index();
        
        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<IEnumerable<Course>>(viewResult.ViewData.Model).ToList();
        Assert.Equal(5, model.Count);
    }

    [Theory]
    [InlineData(3, 1)]
    [InlineData(3, 2)]
    [InlineData(3, 3)]
    [InlineData(3, 4)]
    [InlineData(3, 5)]
    public void GroupsTest(int count, int courseId)
    {
        // Act
        var result = _controller.Groups(courseId);
        
        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<IEnumerable<Group>>(viewResult.ViewData.Model).ToList();
        Assert.Equal(count, model.Count);
    }
}