using Xunit;
using ObligatoriskOppgave1.Models;

public class CourseTests
{
    [Fact]
    public void EnrollStudent_ShouldAddStudent()
    {
        var course = new Course
        {
            CourseId = "CS1",
            CourseName = "Testkurs",
            MaxStudents = 2
        };

        var student = new Student
        {
            StudentId = "S1",
            Name = "Ola"
        };

        bool result = course.EnrollStudent(student);

        Assert.True(result);
        Assert.Contains(student, course.Students);
        Assert.Contains(course, student.Courses);
    }

    [Fact]
    public void EnrollStudent_ShouldReturnFalse_WhenStudentIsAlreadyEnrolled()
    {
        var course = new Course
        {
            CourseId = "CS1",
            CourseName = "Testkurs",
            MaxStudents = 2
        };

        var student = new Student
        {
            StudentId = "S1",
            Name = "Ola"
        };

        course.EnrollStudent(student);
        bool result = course.EnrollStudent(student);

        Assert.False(result);
        Assert.Single(course.Students);
    }

    [Fact]
    public void EnrollStudent_ShouldReturnFalse_WhenCourseIsFull()
    {
        var course = new Course
        {
            CourseId = "CS1",
            CourseName = "Testkurs",
            MaxStudents = 1
        };

        var student1 = new Student { StudentId = "S1", Name = "Ola" };
        var student2 = new Student { StudentId = "S2", Name = "Kari" };

        course.EnrollStudent(student1);
        bool result = course.EnrollStudent(student2);

        Assert.False(result);
        Assert.Single(course.Students);
    }
    [Fact]
    public void RemoveStudent_ShouldRemoveStudentFromCourse()
    {
        var course = new Course
        {
            CourseId = "CS1",
            CourseName = "Testkurs",
            MaxStudents = 2
        };

        var student = new Student
        {
            StudentId = "S1",
            Name = "Ola"
        };

        course.EnrollStudent(student);
        bool result = course.RemoveStudent(student);

        Assert.True(result);
        Assert.Empty(course.Students);
    }
}