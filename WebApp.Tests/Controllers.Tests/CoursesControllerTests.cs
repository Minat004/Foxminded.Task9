using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApp.Controllers;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Tests.Controllers.Tests;

public class CoursesControllerTests
{
    private readonly Mock<ICourseService<Course>> _mockCourseService;
    
    public CoursesControllerTests()
    {
        // Arrange
        _mockCourseService = new Mock<ICourseService<Course>>();
        _mockCourseService.Setup(x => x.GetAllAsync())
            .ReturnsAsync(MockDataHelper.GetCourses);
    }

    [Fact]
    public async Task IndexAsyncTest()
    {
        // Arrange
        var controller = new CoursesController(_mockCourseService.Object);
        
        // Act
        var result = await controller.IndexAsync();
        
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
    public async Task GroupsAsyncTest(int count, int courseId)
    {
        // Arrange
        _mockCourseService.Setup(x => x.GetCourseGroupsAsync(It.IsAny<int>()))
            .ReturnsAsync(MockDataHelper.GetGroupsOfCourseById(It.IsAny<int>()));
        
        var controller = new CoursesController(_mockCourseService.Object);
        
        // Act
        var result = await controller.GroupsAsync(courseId);
        
        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<IEnumerable<Group>>(viewResult.ViewData.Model).ToList();
        Assert.Equal(count, model.Count);
    }
}