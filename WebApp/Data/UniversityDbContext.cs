namespace WebApp.Data;

public partial class UniversityDbContext : DbContext
{
    public UniversityDbContext(DbContextOptions<UniversityDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Course>? Courses { get; set; }

    public virtual DbSet<Group>? Groups { get; set; }

    public virtual DbSet<Student>? Students { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__COURSES__3214EC27CCBD0248");

            entity.ToTable("COURSES");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NAME");
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__GROUPS__3214EC27B9D9CF4D");

            entity.ToTable("GROUPS");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CourseId).HasColumnName("COURSE_ID");
            entity.Property(e => e.Name)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("NAME");

            entity.HasOne(d => d.Course).WithMany(p => p.Groups)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK__GROUPS__COURSE_I__4BAC3F29");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__STUDENTS__3214EC273809DBFF");

            entity.ToTable("STUDENTS");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FIRST_NAME");
            entity.Property(e => e.GroupId).HasColumnName("GROUP_ID");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("LAST_NAME");

            entity.HasOne(d => d.Group).WithMany(p => p.Students)
                .HasForeignKey(d => d.GroupId)
                .HasConstraintName("FK__STUDENTS__GROUP___5CD6CB2B");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}