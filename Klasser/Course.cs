namespace ObligatoriskOppgave1.Models;

public class Course
{
    public string CourseId { get; set; } = "";
    public string CourseName { get; set; } = "";
    public int Credits { get; set; }
    public int MaxStudents { get; set; }

    public List<Student> Students { get; set; } = new List<Student>();

    public bool EnrollStudent(Student student) 
    {
        if (Students.Count < MaxStudents && !Students.Contains(student))
        {
            Students.Add(student);
            student.Courses.Add(this);
            return true;
        }
        return false;
    }
}
    
        