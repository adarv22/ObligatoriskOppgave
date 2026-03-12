using System;
using System.Linq;
using ObligatoriskOppgave1.Models;
using System.Collections.Generic;


//Lister 
List<Course> courses = new List<Course>();
List<Student> students = new List<Student>();
List<Employee> employees = new List<Employee>();
Library library = new Library();


//Testdata
students.Add(new Student
{
    Name = "Ola Nordmann",
    Email = "ola@uia.no",
    StudentId = "1234"
});

students.Add(new Student
{
    Name = "Kari Nordmann",
    Email = "kari@uia.no",
    StudentId = "5678"
});

employees.Add(new Employee
{
    Name = "Per Hansen",
    Email = "perH@uia.no",
    EmployeeId = "E123",
    Position = "Lærer",
    Department = "Informatikk"
});

students.Add(new ExchangeStudent
{
    Name = "Anna Svensson",
    Email = "anna@uia.no",
    StudentId = "9012",
    HomeUniversity = "Stockholms Universitet",
    Country = "Sverige",
    FromDate = new DateTime(2026, 1, 15),
    ToDate = new DateTime(2026, 6, 15)
});

courses.Add(new Course
{
    CourseId = "CS101",
    CourseName = "Introduksjon til programmering",
    Credits = 10,
    MaxStudents = 30
});

courses.Add(new Course
{
    CourseId = "CS102",
    CourseName = "Objektorientert programmering",
    Credits = 10,
    MaxStudents = 25
});

library.AddBook(new Book
{
    BookId = "B001",
    Title = "C# Programming",
    Author = "John Doe",
    Year = 2020,
    Copies = 5
});

Console.WriteLine("Universitetssystem starter...");

//Meny 
static void PrintMenu()
{
    Console.WriteLine("\nVelg en handling:");
    Console.WriteLine("1. Opprett kurs");
    Console.WriteLine("2. Meld student til/av kurs");
    Console.WriteLine("3. Print kurs og studenter");
    Console.WriteLine("4. Søk på kurs");
    Console.WriteLine("5. Søk på bok");
    Console.WriteLine("6. Lån bok");
    Console.WriteLine("7. Returner bok");
    Console.WriteLine("8. Registrer bok");
    Console.WriteLine("0. Avslutt");
}

//Case liste for menyvalg
while (true)
{    
    PrintMenu();
    string choice = Console.ReadLine() ?? "Ukjent valg";

    switch (choice)
    {
        case "1":
            CreateCourse(courses);
            break;
        case "2":
            MeldStudent(courses, students);
            break;
        case "3":
            PrintCourses(courses);
            break;
        case "4":
            SearchCourse(courses);
            break;
        case "5":
            SearchBook(library);
            break;
        case "6":
            LoanBook(library, students, employees);
            break;
        case "7":
            ReturnBook(library, students, employees);
            break;
        case "8":
            RegisterBook(library);
            break;
        case "0":
            Console.WriteLine("Avslutter programmet...");
            return;
    
        default:
            Console.WriteLine("Ugyldig valg. Prøv igjen.");
            break;
    
    }
}


//Opprete kurs 1
static void CreateCourse(List<Course> courses)
{
    Console.WriteLine("Skriv inn kurs ID:");
    string courseId = Console.ReadLine() ?? "";
    Console.WriteLine("Skriv inn kurs navn:");
    string courseName = Console.ReadLine() ?? "";
    Console.WriteLine("Skriv inn antall studiepoeng:");
    int credits = int.Parse(Console.ReadLine() ?? "0");
    Console.WriteLine("Skriv inn maks antall studenter:");
    int maxStudents = int.Parse(Console.ReadLine() ?? "0");

    Course newCourse = new Course
    {
        CourseId = courseId,
        CourseName = courseName,
        Credits = credits,
        MaxStudents = maxStudents
    };

    courses.Add(newCourse);
    Console.WriteLine($"Kurs {courseName} opprettet.");
}

// Meld student til/av kurs 2
static void MeldStudent(List<Course> courses, List<Student> students)
{
    Console.WriteLine("Skriv inn student ID:");
    string studentId = Console.ReadLine() ?? "";

    Student? student = students.FirstOrDefault(s => s.StudentId == studentId);

    if (student == null)
    {
        Console.WriteLine("Student ikke funnet.");
        return;
    }

    Console.WriteLine("Skriv inn kurs ID:");
    string courseId = Console.ReadLine() ?? "";
    Course? course = courses.FirstOrDefault(c => c.CourseId == courseId);
    if (course == null)
    {
        Console.WriteLine("Kurs ikke funnet.");
        return;
    }

    if (course.Students.Contains(student))
    {
        course.Students.Remove(student);
        student.Courses.Remove(course);
        Console.WriteLine($"Student {student.Name} meldt av kurs {course.CourseName}.");
    }
    else
    {
        if (course.EnrollStudent(student))
        {
            Console.WriteLine($"Student {student.Name} meldt på kurs {course.CourseName}.");
        }
         else
        {
            Console.WriteLine("Kurs er fullt. Kan ikke melde på flere studenter.");
        }
    }
}

// Print kurs og studenter 3
static void PrintCourses(List<Course> courses)
{
    if (courses.Count == 0)
    {
        Console.WriteLine("Ingen kurs tilgjengelig.");
        return;
    }
    foreach (var course in courses)
    {
        Console.WriteLine($"Kurs: {course.CourseName} (ID: {course.CourseId}, Studiepoeng: {course.Credits}, Max Studenter: {course.MaxStudents})");
        if (course.Students.Count == 0)
        {
            Console.WriteLine("  Ingen studenter meldt på dette kurset.");
        }
        else
        {
            Console.WriteLine("  Studenter:");
            foreach (var student in course.Students)
            {
                Console.WriteLine($"    - {student.Name} (ID: {student.StudentId})");
            }
        }
    }
}

// Søk på kurs 4
static void SearchCourse(List<Course> courses)
{
    Console.WriteLine("Skriv inn kurs ID eller navn for å søke:");
    string search = Console.ReadLine() ?? "";
    Course? course = courses.FirstOrDefault(c => c.CourseId == search || c.CourseName == search);
    if (course == null)
    {
        Console.WriteLine("Kurs ikke funnet.");
        return;
    }
    Console.WriteLine($"Kurs: {course.CourseName} (ID: {course.CourseId}, Studiepoeng: {course.Credits}, Max Studenter: {course.MaxStudents})");
    if (course.Students.Count == 0)
    {
        Console.WriteLine("  Ingen studenter meldt på dette kurset.");
    }
    else
    {
        Console.WriteLine("  Studenter:");
        foreach (var student in course.Students)
        {
            Console.WriteLine($"    - {student.Name} (ID: {student.StudentId})");
        }
    }
}

// Søk på bok 5
static void SearchBook(Library library)
{
    Console.WriteLine("Skriv inn bok ID for å søke:");
    string bookId = Console.ReadLine() ?? "";
    Book? book = library.Books.FirstOrDefault(b => b.BookId == bookId);
    if (book == null)
    {
        Console.WriteLine("Bok ikke funnet.");
        return;
    }
    Console.WriteLine($"Bok: {book.Title} (ID: {book.BookId}, Forfatter: {book.Author}, År: {book.Year}, Eksemplarer: {book.Copies})");
}

// Lån bok 6
static void LoanBook(Library library, List<Student> students, List<Employee> employees)
{
    Console.WriteLine("Hvem skal låne?");
    Console.WriteLine("1 - Student/Utvekslingsstudent");
    Console.WriteLine("2 - Ansatt");
    string borrowerType = Console.ReadLine() ?? "";

    User? borrower = null;

    if (borrowerType == "1")
    {
        Console.WriteLine("Skriv inn student ID:");
        string studentId = Console.ReadLine() ?? "";
        borrower = students.FirstOrDefault(s => s.StudentId == studentId);
    }
    else if (borrowerType == "2")
    {
        Console.WriteLine("Skriv inn ansatt ID:");
        string employeeId = Console.ReadLine() ?? "";
        borrower = employees.FirstOrDefault(e => e.EmployeeId == employeeId);
    }
    else
    {
        Console.WriteLine("Ugyldig valg.");
        return;
    }

    if (borrower == null)
    {
        Console.WriteLine("Bruker ikke funnet.");
        return;
    }

    Console.WriteLine("Skriv inn bok ID for å låne:");
    string bookId = Console.ReadLine() ?? "";

    if (library.LoanBook(borrower, bookId))
    {
        Console.WriteLine($"{borrower.Name} har lånt boka.");
    }
    else
    {
        Console.WriteLine("Bok ikke tilgjengelig.");
    }

    PrintLoans(library);
}

// Returner bok 7
static void ReturnBook(Library library, List<Student> students, List<Employee> employees)
{
    Console.WriteLine("Hvem returnerer boka?");
    Console.WriteLine("1 - Student/Utvekslingsstudent");
    Console.WriteLine("2 - Ansatt");
    string borrowerType = Console.ReadLine() ?? "";

    User? borrower = null;

    if (borrowerType == "1")
    {
        Console.WriteLine("Skriv inn student ID:");
        string studentId = Console.ReadLine() ?? "";
        borrower = students.FirstOrDefault(s => s.StudentId == studentId);
    }
    else if (borrowerType == "2")
    {
        Console.WriteLine("Skriv inn ansatt ID:");
        string employeeId = Console.ReadLine() ?? "";
        borrower = employees.FirstOrDefault(e => e.EmployeeId == employeeId);
    }
    else
    {
        Console.WriteLine("Ugyldig valg.");
        return;
    }

    if (borrower == null)
    {
        Console.WriteLine("Bruker ikke funnet.");
        return;
    }

    Console.WriteLine("Skriv inn bok ID for å returnere:");
    string bookId = Console.ReadLine() ?? "";

    if (library.ReturnBook(borrower,bookId))
    {
        Console.WriteLine($"{borrower.Name} har returnert boka.");
    }
    else
    {
        Console.WriteLine("Lån ikke funnet.");
    }
    PrintLoans(library);
}

// Vis aktive lån + historikk (brukes av 6 og 7)
static void PrintLoans(Library library)
{
    Console.WriteLine("\n--- Aktive lån ---");

    var activeLoans = library.Loans.Where(l => !l.Returned);

    if (!activeLoans.Any())
    {
        Console.WriteLine("Ingen aktive lån.");
    }
    else
    {
        foreach (var loan in activeLoans)
        {
            Console.WriteLine($"{loan.Borrower.Name} har lånt {loan.Book.Title} (Forfall: {loan.DueDate.ToShortDateString()})");
        }
    }

    Console.WriteLine("\n--- Lånehistorikk ---");

    var history = library.Loans.Where(l => l.Returned);

    if (!history.Any())
    {
        Console.WriteLine("Ingen historikk enda.");
    }
    else
    {
        foreach (var loan in history)
        {
            Console.WriteLine($"{loan.Borrower.Name} returnerte {loan.Book.Title}");
        }
    }
}

// Register bok 8 
static void RegisterBook(Library library)
{
    Console.WriteLine("Skriv inn bok ID:");
    string bookId = Console.ReadLine() ?? "";

    Console.WriteLine("Skriv inn bok tittel:");
    string title = Console.ReadLine() ?? "";

    Console.WriteLine("Skriv inn bok forfatter:");
    string author = Console.ReadLine() ?? "";

    Console.WriteLine("Skriv inn år: ");
    int year = int.Parse(Console.ReadLine() ?? "0");

    Console.WriteLine("Skriv inn antall eksemplarer: ");
    int copies = int.Parse(Console.ReadLine() ?? "0");

    Book newBook = new Book
    {
        BookId = bookId,
        Title = title,
        Author = author,
        Year = year,
        Copies = copies
    };

    library.AddBook(newBook);
    Console.WriteLine($"Bok {title} registrert i biblioteket.");
}