namespace ObligatoriskOppgave1.Models;

public class Course
{
    public string CourseId { get; set; } = "";
    public string CourseName { get; set; } = "";
    public int Credits { get; set; }
    public int MaxStudents { get; set; }

    public Employee? Teacher { get; set; }

    public List<Student> Students { get; set; } = new List<Student>();
    public List<Book> CurriculumBooks { get; set; } = new List<Book>();

    public bool EnrollStudent(Student student)
    {
        if (Students.Any(s => s.StudentId == student.StudentId))
        {
            return false;
        }

        if (Students.Count >= MaxStudents)
        {
            return false;
        }

        Students.Add(student);

        if (!student.Courses.Any(c => c.CourseId == CourseId))
        {
            student.Courses.Add(this);
        }

        return true;
    }

    public bool RemoveStudent(Student student)
    {
        Student? enrolledStudent = Students.FirstOrDefault(s => s.StudentId == student.StudentId);

        if (enrolledStudent == null)
        {
            return false;
        }

        Students.Remove(enrolledStudent);

        Course? studentCourse = student.Courses.FirstOrDefault(c => c.CourseId == CourseId);
        if (studentCourse != null)
        {
            student.Courses.Remove(studentCourse);
        }

        return true;
    }
}