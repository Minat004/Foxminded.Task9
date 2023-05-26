using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models;

public partial class Student
{
    public int Id { get; set; }

    public int? GroupId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;
    
    [NotMapped]
    public SelectList? Groups { get; set; }

    public virtual Group? Group { get; set; }
}
