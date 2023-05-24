﻿using WebApp.Data;
using WebApp.Models;

namespace WebApp.Repositories;

public class GroupRepository : IGroupRepository
{
    private readonly UniversityDbContext _context;

    public GroupRepository(UniversityDbContext context)
    {
        _context = context;
    }

    public void Create()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Group> GetAll()
    {
        var groups = _context.Groups!.ToList();
        var courses = _context.Courses!.ToList();
        
        foreach (var group in groups)
        {
            group.Course = courses.FirstOrDefault(x => x.Id == group.CourseId);
            yield return group;
        }
    }

    public Task<IEnumerable<Group>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public void Update(Group course)
    {
        throw new NotImplementedException();
    }

    public void Update(int id)
    {
        throw new NotImplementedException();
    }

    public void Delete(Group course)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }
}