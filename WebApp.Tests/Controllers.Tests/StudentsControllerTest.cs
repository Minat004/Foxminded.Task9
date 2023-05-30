using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApp.Controllers;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Tests.Controllers.Tests;

public class StudentsControllerTest
{
    private readonly Mock<IService<Student>> _mockStudentService;
    private readonly Mock<IService<Group, Student>> _mockGroupService;

    public StudentsControllerTest()
    {
        // Arrange
        _mockStudentService = new Mock<IService<Student>>();
        _mockStudentService.Setup(x => x.GetAll())
            .Returns(MockDataHelper.GetStudents);

        _mockGroupService = new Mock<IService<Group, Student>>();
        _mockGroupService.Setup(x => x.GetAll())
            .Returns(MockDataHelper.GetGroups);
    }

    [Fact]
    public void IndexTest()
    {
        // Arrange
        _mockStudentService.Setup(x => x.GetAll())
            .Returns(MockDataHelper.GetStudents());
        
        var controller = new StudentsController(_mockStudentService.Object, _mockGroupService.Object);
        
        // Act
        var result = controller.Index();
        
        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<IEnumerable<Student>>(viewResult.ViewData.Model).ToList();
        Assert.Equal(40, model.Count);
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
    [InlineData(16)]
    [InlineData(17)]
    [InlineData(18)]
    [InlineData(19)]
    [InlineData(20)]
    public void EditGetTest(int studentId)
    {
        // Arrange
        var controller = new StudentsController(_mockStudentService.Object, _mockGroupService.Object);
        
        // Act
        var result = controller.Edit(studentId);
        
        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<Student>(viewResult.ViewData.Model);
        Assert.Equal(studentId, model.Id);
    }
    
    [Fact]
    public void EditPostModelStateIsValidTest()
    {
        // Arrange
        var controller = new StudentsController(_mockStudentService.Object, _mockGroupService.Object);

        // Act
        var result = controller.Edit(It.IsAny<Student>());
        
        // Assert
        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Null(redirectToActionResult.ControllerName);
        Assert.Equal("Index", redirectToActionResult.ActionName);
        _mockStudentService.Verify(x => x.Update(It.IsAny<Student>()), Times.Once);
    }
    
    [Fact]
    public void EditPostNotModelStateIsValidTest()
    {
        // Arrange
        var controller = new StudentsController(_mockStudentService.Object, _mockGroupService.Object);
        controller.ModelState.AddModelError("Name", "Required");

        // Act
        var result = controller.Edit(It.IsAny<Student>());
        
        // Assert
        var badRequestResult = Assert.IsType<BadRequestResult>(result);
        Assert.IsType<BadRequestResult>(badRequestResult);
    }

    [Fact]
    public void AddGetTest()
    {
        // Arrange
        var controller = new StudentsController(_mockStudentService.Object, _mockGroupService.Object);
        
        // Act
        var result = controller.Add();
        
        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<Student>(viewResult.ViewData.Model);
        Assert.IsAssignableFrom<Student>(model);
    }

    [Fact]
    public void AddPostModelStateIsValidTest()
    {
        // Arrange
        var controller = new StudentsController(_mockStudentService.Object, _mockGroupService.Object);
        
        // Act
        var result = controller.Add(It.IsAny<Student>());
        
        // Assert
        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Null(redirectToActionResult.ControllerName);
        Assert.Equal("Index", redirectToActionResult.ActionName);
        _mockStudentService.Verify(x => x.Add(It.IsAny<Student>()), Times.Once);
    }

    [Fact]
    public void AddPostNotModelStateIsValidTest()
    {
        // Arrange
        var controller = new StudentsController(_mockStudentService.Object, _mockGroupService.Object);
        controller.ModelState.AddModelError("FirstName", "Required");
        controller.ModelState.AddModelError("LastName", "Required");

        // Act
        var result = controller.Add(It.IsAny<Student>());
        
        // Assert
        var badRequestResult = Assert.IsType<BadRequestResult>(result);
        Assert.IsType<BadRequestResult>(badRequestResult);
    }

    [Fact]
    public void DeletePostTest()
    {
        // Arrange
        var controller = new StudentsController(_mockStudentService.Object, _mockGroupService.Object);

        // Act
        var result = controller.Delete(It.IsAny<Student>());
        
        // Assert
        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Null(redirectToActionResult.ControllerName);
        Assert.Equal("Index", redirectToActionResult.ActionName);
        _mockStudentService.Verify(x => x.Delete(It.IsAny<Student>()), Times.Once);
    }
}