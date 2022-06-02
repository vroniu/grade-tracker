using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace grade_tracker.Models;

public class Teacher
{
    [Key]
    public int TeacherId { get; set; }
    
    public string? FirstName { get; set; }

    public String? LastName { get; set; }
    
    public List<Subject>? SubjectsToTeach { get; set; }

    [NotMapped]
    public string FullName
    {
        get
        {
            return $"{FirstName} {LastName}";
        }
    }
}