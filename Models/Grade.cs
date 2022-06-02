using System.ComponentModel.DataAnnotations;

namespace grade_tracker.Models;

public class Grade
{
    [Key]
    public int GradeId { get; set; }
    public int AssignedGrade { get; set; }
    public string? AdditionalComment { get; set; }
    public int SubjectId { get; set; }
    public Subject? Subject { get; set; }
    public int StudentId { get; set; }
    public Student? Student { get; set; }
    public int TeacherId { get; set; }
    public Teacher? Teacher { get; set; }
}