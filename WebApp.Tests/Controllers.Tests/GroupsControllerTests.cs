using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApp.Controllers;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Tests.Controllers.Tests;

public class GroupsControllerTests
{
    private readonly Mock<IGroupService<Group>> _mockGroupService;
    private readonly Mock<ICourseService<Course>> _mockCourseService;
    private readonly Mock<ICancelable> _mockCancelService;

    public GroupsControllerTests()
    {
        // Arrange
        _mockGroupService = new Mock<IGroupService<Group>>();
        _mockGroupService.Setup(x => x.GetAllAsync())
            .ReturnsAsync(MockDataHelper.GetGroups);

        _mockCourseService = new Mock<ICourseService<Course>>();
        _mockCourseService.Setup(x => x.GetAllAsync())
            .ReturnsAsync(MockDataHelper.GetCourses);

        _mockCancelService = new Mock<ICancelable>();
    }

    [Fact]
    public async Task IndexAsyncTest()
    {
        // Arrange
        var controller = new GroupsController(_mockGroupService.Object, _mockCourseService.Object, _mockCancelService.Object);
        
        // Act
        var result = await controller.IndexAsync();
        
        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<IEnumerable<Group>>(viewResult.ViewData.Model).ToList();
        Assert.Equal(15, model.Count);
    }

    [Theory]
    [InlineData(6, 1)]
    // [InlineData(7, 2)]
    // [InlineData(7, 3)]
    // [InlineData(4, 4)]
    // [InlineData(4, 5)]
    // [InlineData(12, 13)]
    public async Task StudentsAsyncTest(int count, int groupId)
    {
        // Arrange
        _mockGroupService.Setup(x => x.GetGroupStudentsAsync(It.IsAny<int>()))
            .ReturnsAsync(MockDataHelper.GetStudentsOfGroupById(It.IsAny<int>()));
        
        var controller = new GroupsController(_mockGroupService.Object, _mockCourseService.Object, _mockCancelService.Object);
        
        // Act
        var result = await controller.StudentsAsync(groupId);
        
        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<IEnumerable<Student>>(viewResult.ViewData.Model).ToList();
        Assert.Equal(count, model.Count);
    }
    
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(6)]
    [InlineData(7)]
    [InlineData(8)]
    [InlineData(9)]
    [InlineData(10)]
    [InlineData(11)]
    [InlineData(12)]
    [InlineData(13)]
    [InlineData(14)]
    [InlineData(15)]
    public async Task EditAsyncGetTest(int groupId)
    {
        // Arrange
        var controller = new GroupsController(_mockGroupService.Object, _mockCourseService.Object, _mockCancelService.Object);
        
        // Act
        var result = await controller.EditAsync(groupId);
        
        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<Group>(viewResult.ViewData.Model);
        Assert.Equal(groupId, model.Id);
    }
    
    [Fact]
    public async Task EditAsyncPostModelStateIsValidTest()
    {
        // Arrange
        var controller = new GroupsController(_mockGroupService.Object, _mockCourseService.Object, _mockCancelService.Object);

        // Act
        var result = await controller.EditAsync(It.IsAny<Group>());
        
        // Assert
        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Null(redirectToActionResult.ControllerName);
        Assert.Equal("Index", redirectToActionResult.ActionName);
        _mockGroupService.Verify(x => x.UpdateAsync(It.IsAny<Group>()), Times.Once);
    }
    
    [Fact]
    public async Task EditAsyncPostNotModelStateIsValidTest()
    {
        // Arrange
        var controller = new GroupsController(_mockGroupService.Object, _mockCourseService.Object, _mockCancelService.Object);
        controller.ModelState.AddModelError("Name", "Required");

        // Act
        var result = await controller.EditAsync(It.IsAny<Group>());
        
        // Assert
        var badRequestResult = Assert.IsType<BadRequestResult>(result);
        Assert.IsType<BadRequestResult>(badRequestResult);
    }

    [Fact]
    public async Task AddAsyncGetTest()
    {
        // Arrange
        var controller = new GroupsController(_mockGroupService.Object, _mockCourseService.Object, _mockCancelService.Object);
        
        // Act
        var result = await controller.AddAsync();
        
        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<Group>(viewResult.ViewData.Model);
        Assert.IsAssignableFrom<Group>(model);
    }

    [Fact]
    public async Task AddAsyncPostModelStateIsValidTest()
    {
        // Arrange
        var controller = new GroupsController(_mockGroupService.Object, _mockCourseService.Object, _mockCancelService.Object);
        
        // Act
        var result = await controller.AddAsync(It.IsAny<Group>());
        
        // Assert
        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Null(redirectToActionResult.ControllerName);
        Assert.Equal("Index", redirectToActionResult.ActionName);
        _mockGroupService.Verify(x => x.AddAsync(It.IsAny<Group>()), Times.Once);
    }

    [Fact]
    public async Task AddAsyncPostNotModelStateIsValidTest()
    {
        // Arrange
        var controller = new GroupsController(_mockGroupService.Object, _mockCourseService.Object, _mockCancelService.Object);
        controller.ModelState.AddModelError("Name", "Required");

        // Act
        var result = await controller.AddAsync(It.IsAny<Group>());
        
        // Assert
        var badRequestResult = Assert.IsType<BadRequestResult>(result);
        Assert.IsType<BadRequestResult>(badRequestResult);
    }

    [Fact]
    public async Task DeleteAsyncPostEmptyGroupTest()
    {
        // Arrange
        var controller = new GroupsController(_mockGroupService.Object, _mockCourseService.Object, _mockCancelService.Object);

        var group = new Group()
        {
            Id = It.IsAny<int>(),
            Students = new List<Student>()
        };
        
        // Act
        var result = await controller.DeleteAsync(group);
        
        // Assert
        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Null(redirectToActionResult.ControllerName);
        Assert.Equal("Index", redirectToActionResult.ActionName);
        _mockGroupService.Verify(x => x.DeleteAsync(group), Times.Once);
    }

    [Fact]
    public async Task DeletePostNotEmptyGroupTest()
    {
        // Arrange
        var controller = new GroupsController(_mockGroupService.Object, _mockCourseService.Object, _mockCancelService.Object);
        var group = new Group()
        {
            Id = It.IsAny<int>(),
            Students = MockDataHelper.GetStudents().ToList()
        };

        // Act
        var result = await controller.DeleteAsync(group);
        
        // Assert
        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Null(redirectToActionResult.ControllerName);
        Assert.Equal("Index", redirectToActionResult.ActionName);
        _mockGroupService.Verify(x => x.DeleteAsync(group), Times.Never);
    }
}