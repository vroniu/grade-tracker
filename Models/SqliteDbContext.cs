using Microsoft.EntityFrameworkCore;

namespace grade_tracker.Models;

public class SqliteDbContext : DbContext
{
    public SqliteDbContext(DbContextOptions<SqliteDbContext> options): base(options)
    {
        
    }

    public DbSet<Student>? Students { get; set; }
    
    public DbSet<Grade>? Grades { get; set; }
    
    public DbSet<Teacher>? Teachers { get; set; }
    
    public DbSet<Subject>? Subjects { get; set; }
}