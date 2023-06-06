using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models;

public partial class Group
{
    public int Id { get; set; }

    public int? CourseId { get; set; }

    [Required]
    [StringLength(10)]
    public string Name { get; set; } = null!;

    [NotMapped]
    public SelectList? Courses { get; set; }

    public virtual Course? Course { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
