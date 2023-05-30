using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApp.Controllers;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Tests.Controllers.Tests;

public class GroupsControllerTests
{
    private readonly Mock<IService<Group, Student>> _mockGroupService;
    private readonly Mock<IReadable<Course>> _mockCourseService;

    public GroupsControllerTests()
    {
        // Arrange
        _mockGroupService = new Mock<IService<Group, Student>>();
        _mockGroupService.Setup(x => x.GetAll())
            .Returns(MockDataHelper.GetGroups);

        _mockCourseService = new Mock<IReadable<Course>>();
        _mockCourseService.Setup(x => x.GetAll())
            .Returns(MockDataHelper.GetCourses);
    }

    [Fact]
    public void IndexTest()
    {
        // Arrange
        var controller = new GroupsController(_mockGroupService.Object, _mockCourseService.Object);
        
        // Act
        var result = controller.Index();
        
        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<IEnumerable<Group>>(viewResult.ViewData.Model).ToList();
        Assert.Equal(15, model.Count);
    }

    [Theory]
    [InlineData(6, 1)]
    [InlineData(7, 2)]
    [InlineData(7, 3)]
    [InlineData(4, 4)]
    [InlineData(4, 5)]
    [InlineData(12, 13)]
    public void StudentsTest(int count, int groupId)
    {
        // Arrange
        _mockGroupService.Setup(x => x.GetCollection(It.IsAny<int>()))
            .Returns<int>(MockDataHelper.GetStudentsOfGroupById);
        
        var controller = new GroupsController(_mockGroupService.Object, _mockCourseService.Object);
        
        // Act
        var result = controller.Students(groupId);
        
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
    public void EditGetTest(int groupId)
    {
        // Arrange
        var controller = new GroupsController(_mockGroupService.Object, _mockCourseService.Object);
        
        // Act
        var result = controller.Edit(groupId);
        
        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<Group>(viewResult.ViewData.Model);
        Assert.Equal(groupId, model.Id);
    }
    
    [Fact]
    public void EditPostModelStateIsValidTest()
    {
        // Arrange
        var controller = new GroupsController(_mockGroupService.Object, _mockCourseService.Object);

        // Act
        var result = controller.Edit(It.IsAny<Group>());
        
        // Assert
        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Null(redirectToActionResult.ControllerName);
        Assert.Equal("Index", redirectToActionResult.ActionName);
        _mockGroupService.Verify(x => x.Update(It.IsAny<Group>()), Times.Once);
    }
    
    [Fact]
    public void EditPostNotModelStateIsValidTest()
    {
        // Arrange
        var controller = new GroupsController(_mockGroupService.Object, _mockCourseService.Object);
        controller.ModelState.AddModelError("Name", "Required");

        // Act
        var result = controller.Edit(It.IsAny<Group>());
        
        // Assert
        var badRequestResult = Assert.IsType<BadRequestResult>(result);
        Assert.IsType<BadRequestResult>(badRequestResult);
    }

    [Fact]
    public void AddGetTest()
    {
        // Arrange
        var controller = new GroupsController(_mockGroupService.Object, _mockCourseService.Object);
        
        // Act
        var result = controller.Add();
        
        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<Group>(viewResult.ViewData.Model);
        Assert.IsAssignableFrom<Group>(model);
    }

    [Fact]
    public void AddPostModelStateIsValidTest()
    {
        // Arrange
        var controller = new GroupsController(_mockGroupService.Object, _mockCourseService.Object);
        
        // Act
        var result = controller.Add(It.IsAny<Group>());
        
        // Assert
        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Null(redirectToActionResult.ControllerName);
        Assert.Equal("Index", redirectToActionResult.ActionName);
        _mockGroupService.Verify(x => x.Add(It.IsAny<Group>()), Times.Once);
    }

    [Fact]
    public void AddPostNotModelStateIsValidTest()
    {
        // Arrange
        var controller = new GroupsController(_mockGroupService.Object, _mockCourseService.Object);
        controller.ModelState.AddModelError("Name", "Required");

        // Act
        var result = controller.Add(It.IsAny<Group>());
        
        // Assert
        var badRequestResult = Assert.IsType<BadRequestResult>(result);
        Assert.IsType<BadRequestResult>(badRequestResult);
    }

    [Fact]
    public void DeletePostEmptyGroupTest()
    {
        // Arrange
        var controller = new GroupsController(_mockGroupService.Object, _mockCourseService.Object);

        var group = new Group()
        {
            Id = It.IsAny<int>(),
            Students = new List<Student>()
        };
        
        // Act
        var result = controller.Delete(group);
        
        // Assert
        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Null(redirectToActionResult.ControllerName);
        Assert.Equal("Index", redirectToActionResult.ActionName);
        _mockGroupService.Verify(x => x.Delete(group), Times.Once);
    }

    [Fact]
    public void DeletePostNotEmptyGroupTest()
    {
        // Arrange
        var controller = new GroupsController(_mockGroupService.Object, _mockCourseService.Object);
        var group = new Group()
        {
            Id = It.IsAny<int>(),
            Students = MockDataHelper.GetStudents().ToList()
        };

        // Act
        var result = controller.Delete(group);
        
        // Assert
        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Null(redirectToActionResult.ControllerName);
        Assert.Equal("Index", redirectToActionResult.ActionName);
        _mockGroupService.Verify(x => x.Delete(group), Times.Never);
    }
}