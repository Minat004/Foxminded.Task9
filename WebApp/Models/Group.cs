namespace WebApp.Models;

public partial class Group
{
    public int Id { get; set; }

    public int? CourseId { get; set; }

    public string Name { get; set; } = null!;

    public virtual Course? Course { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
