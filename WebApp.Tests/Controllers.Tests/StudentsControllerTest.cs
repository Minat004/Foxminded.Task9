using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using WebApp.Controllers;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Tests.Controllers.Tests;

public class StudentsControllerTest
{
    private readonly Mock<IService<Student>> _mockStudentService;
    private readonly Mock<IGroupService<Group>> _mockGroupService;
    private readonly Mock<ICancelable> _mockCancelService;

    public StudentsControllerTest()
    {
        // Arrange
        _mockStudentService = new Mock<IService<Student>>();
        _mockStudentService.Setup(x => x.GetAllAsync())
            .ReturnsAsync(MockDataHelper.GetStudents);

        _mockGroupService = new Mock<IGroupService<Group>>();
        _mockGroupService.Setup(x => x.GetAllAsync())
            .ReturnsAsync(MockDataHelper.GetGroups);
        
        _mockCancelService = new Mock<ICancelable>();
    }

    [Fact]
    public async Task IndexAsyncTest()
    {
        // Arrange
        var controller = new StudentsController(_mockStudentService.Object, _mockGroupService.Object, _mockCancelService.Object);
        
        // Act
        var result = await controller.IndexAsync();
        
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
    public async Task EditAsyncGetTest(int studentId)
    {
        // Arrange
        var controller = new StudentsController(_mockStudentService.Object, _mockGroupService.Object, _mockCancelService.Object);
        
        // Act
        var result = await controller.EditAsync(studentId);
        
        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<Student>(viewResult.ViewData.Model);
        Assert.Equal(studentId, model.Id);
        _mockCancelService.Verify(x => x.ViewDataReferer(It.IsAny<ViewDataDictionary>(), It.IsAny<HttpRequest>()), Times.Once);
    }
    
    [Fact]
    public async Task EditAsyncPostModelStateIsValidTest()
    {
        // Arrange
        var controller = new StudentsController(_mockStudentService.Object, _mockGroupService.Object, _mockCancelService.Object);

        // Act
        var result = await controller.EditAsync(It.IsAny<Student>());
        
        // Assert
        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Null(redirectToActionResult.ControllerName);
        Assert.Equal("Index", redirectToActionResult.ActionName);
        _mockStudentService.Verify(x => x.UpdateAsync(It.IsAny<Student>()), Times.Once);
    }
    
    [Fact]
    public async Task EditAsyncPostNotModelStateIsValidTest()
    {
        // Arrange
        var controller = new StudentsController(_mockStudentService.Object, _mockGroupService.Object, _mockCancelService.Object);
        controller.ModelState.AddModelError("Name", "Required");

        // Act
        var result = await controller.EditAsync(It.IsAny<Student>());
        
        // Assert
        var badRequestResult = Assert.IsType<BadRequestResult>(result);
        Assert.IsType<BadRequestResult>(badRequestResult);
    }

    [Fact]
    public async Task AddAsyncGetTest()
    {
        // Arrange
        var controller = new StudentsController(_mockStudentService.Object, _mockGroupService.Object, _mockCancelService.Object);
        
        // Act
        var result = await controller.AddAsync();
        
        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<Student>(viewResult.ViewData.Model);
        Assert.IsAssignableFrom<Student>(model);
        _mockCancelService.Verify(x => x.ViewDataReferer(It.IsAny<ViewDataDictionary>(), It.IsAny<HttpRequest>()), Times.Once);
    }

    [Fact]
    public async Task AddAsyncPostModelStateIsValidTest()
    {
        // Arrange
        var controller = new StudentsController(_mockStudentService.Object, _mockGroupService.Object, _mockCancelService.Object);
        
        // Act
        var result = await controller.AddAsync(It.IsAny<Student>());
        
        // Assert
        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Null(redirectToActionResult.ControllerName);
        Assert.Equal("Index", redirectToActionResult.ActionName);
        _mockStudentService.Verify(x => x.AddAsync(It.IsAny<Student>()), Times.Once);
    }

    [Fact]
    public async Task AddAsyncPostNotModelStateIsValidTest()
    {
        // Arrange
        var controller = new StudentsController(_mockStudentService.Object, _mockGroupService.Object, _mockCancelService.Object);
        controller.ModelState.AddModelError("FirstName", "Required");
        controller.ModelState.AddModelError("LastName", "Required");

        // Act
        var result = await controller.AddAsync(It.IsAny<Student>());
        
        // Assert
        var badRequestResult = Assert.IsType<BadRequestResult>(result);
        Assert.IsType<BadRequestResult>(badRequestResult);
    }

    [Fact]
    public async Task DeleteAsyncPostTest()
    {
        // Arrange
        var controller = new StudentsController(_mockStudentService.Object, _mockGroupService.Object, _mockCancelService.Object);

        // Act
        var result = await controller.DeleteAsync(It.IsAny<Student>());
        
        // Assert
        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Null(redirectToActionResult.ControllerName);
        Assert.Equal("Index", redirectToActionResult.ActionName);
        _mockStudentService.Verify(x => x.DeleteAsync(It.IsAny<Student>()), Times.Once);
    }
}