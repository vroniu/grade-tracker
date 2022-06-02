using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace grade_tracker.Models;

public class Student
{
    [Key]
    public int StudentId { get; set; }
    
    public string? FirstName { get; set; }

    public String? LastName { get; set; }
    
    public List<Subject>? SubjectsToLearn { get; set; }

    [NotMapped]
    public string FullName
    {
        get
        {
            return $"{FirstName} {LastName}";
        }
    }
}