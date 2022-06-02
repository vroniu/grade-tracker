using System.ComponentModel.DataAnnotations;

namespace grade_tracker.Models;

public class Subject
{
    [Key]
    public int SubjectId { get; set; }
    public string? Name { get; set; }
    public List<Student>? Students { get; set; }
    public List<Teacher>? Teachers { get; set; }
}