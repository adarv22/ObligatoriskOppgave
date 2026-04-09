namespace ObligatoriskOppgave1.Models;

public class Student : User
{
    public string StudentId { get; set; } = "";
    public List<Course> Courses { get; set; } = new List<Course>();
    public Dictionary<string, string> Grades { get; set; } = new Dictionary<string, string>();

    public Student() 
    {
        Role = UserRole.Student;
    }
}