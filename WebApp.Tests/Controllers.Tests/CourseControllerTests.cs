using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApp.Controllers;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Tests.Controllers.Tests;

public class CourseControllerTests
{
    [Fact]
    public void IndexTest()
    {
        // Assert
        var mockService = new Mock<IReadable<Course, Group>>();
        mockService.Setup(x => x.GetAll())
            .Returns(GetCourses);
        var controller = new CoursesController(mockService.Object);
        
        // Act
        var result = controller.Index();
        
        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<IEnumerable<Course>>(viewResult.ViewData.Model).ToList();
        Assert.Equal(5, model.Count);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void GroupsTest(int courseId)
    {
        // Assert
        var mockService = new Mock<IReadable<Course, Group>>();
        mockService.Setup(x => x.GetCollection(courseId))
            .Returns(GetGroupsOfCourseById(courseId));
        var controller = new CoursesController(mockService.Object);
        
        // Act
        var result = controller.Groups(courseId);
        
        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<IEnumerable<Group>>(viewResult.ViewData.Model).ToList();
        Assert.Equal(3, model.Count);
    }

    private static IEnumerable<Course> GetCourses()
    {
        var courses = new List<Course>
        {
            new()
            {
                Id = 1,
                Name = "Space Engineering",
                Description = "Department of Space Engineering"
            },
            new()
            {
                Id = 2,
                Name = "Aircraft",
                Description = "Department of Aircraft Control Systems"
            },
            new()
            {
                Id = 3,
                Name = "Chemical Engineering",
                Description = "Faculty of Chemical Engineering"
            },
            new()
            {
                Id = 4,
                Name = "Machine Design",
                Description = "Department of Machine Design"
            },
            new()
            {
                Id = 5,
                Name = "Space Engineering",
                Description = "Electronic Engineering Department"
            }
        };

        return courses;
    }

    private static IEnumerable<Group> GetGroups()
    {
        var groups = new List<Group>
        {
            new()
            {
                Id = 1,
                CourseId = 1,
                Name = "SE-01"
            },
            new()
            {
                Id = 2,
                CourseId = 1,
                Name = "SE-02"
            },
            new()
            {
                Id = 3,
                CourseId = 1,
                Name = "SE-03"
            },
            new()
            {
                Id = 4,
                CourseId = 2,
                Name = "AC-012"
            },
            new()
            {
                Id = 5,
                CourseId = 2,
                Name = "AC-02"
            },
            new()
            {
                Id = 6,
                CourseId = 2,
                Name = "AC-03"
            },
            new()
            {
                Id = 7,
                CourseId = 3,
                Name = "CE-01"
            },
            new()
            {
                Id = 8,
                CourseId = 3,
                Name = "CE-02"
            },
            new()
            {
                Id = 9,
                CourseId = 3,
                Name = "CE-03"
            },
            new()
            {
                Id = 10,
                CourseId = 5,
                Name = "SR-01"
            },
            new()
            {
                Id = 11,
                CourseId = 5,
                Name = "SR-02"
            },
            new()
            {
                Id = 12,
                CourseId = 5,
                Name = "SR-03"
            },
            new()
            {
                Id = 13,
                CourseId = 4,
                Name = "DC-01"
            },
            new()
            {
                Id = 14,
                CourseId = 4,
                Name = "DC-02"
            },
            new()
            {
                Id = 15,
                CourseId = 4,
                Name = "DC-03"
            }
        };

        return groups;
    }

    private static IEnumerable<Group> GetGroupsOfCourseById(int courseId)
    {
        var groups = GetGroups();

        return groups.Where(x => x.CourseId == courseId);
    }
}