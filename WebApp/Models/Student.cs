﻿namespace WebApp.Models;

public partial class Student
{
    public int Id { get; set; }

    public int? GroupId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public virtual Group? Group { get; set; }
}
