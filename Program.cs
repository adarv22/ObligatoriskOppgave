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
    StudentId = "1234",
    Username = "ola123",
    Password = "12345",
    Role = UserRole.Student
});

students.Add(new Student
{
    Name = "Kari Nordmann",
    Email = "kari@uia.no",
    StudentId = "5678",
    Username = "kari5678",
    Password = "56789",
    Role = UserRole.Student
});

employees.Add(new Employee
{
    Name = "Per Hansen",
    Email = "perH@uia.no",
    EmployeeId = "E123",
    Position = "Lærer",
    Department = "Informatikk",
    Username = "perH123",
    Password = "perH12345",
    Role = UserRole.Teacher
});

employees.Add(new Employee
{
    Name = "Lise Pedersen",
    Email = "liseP@uia.no",
    EmployeeId = "E456",
    Position = "Bibliotekar",
    Department = "Bibliotek",
    Username = "liseP456",
    Password = "liseP45678",
    Role = UserRole.Librarian
});

students.Add(new ExchangeStudent
{
    Name = "Anna Svensson",
    Email = "anna@uia.no",
    StudentId = "9012",
    Username = "anna9012",
    Password = "90123",
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
    MaxStudents = 30,
    Teacher = employees.FirstOrDefault(e => e.Role == UserRole.Teacher)
});

courses.Add(new Course
{
    CourseId = "CS102",
    CourseName = "Objektorientert programmering",
    Credits = 10,
    MaxStudents = 25,
    Teacher = employees.FirstOrDefault(e => e.Role == UserRole.Teacher)
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

// Start meny
while (true)
{
    Console.WriteLine("\n1. Eksisterende bruker");
    Console.WriteLine("2. Ny bruker");
    Console.WriteLine("0. Avslutt");

    string choice = Console.ReadLine() ?? "";

    switch (choice)
    {
        case "1":
            User? user = Login(students, employees);
            if (user != null)
            {
                ShowRoleMenu(user, courses, students, employees, library);
            }
            break;

        case "2":
            RegisterUser(students, employees);
            break;

        case "0":
            return;

        default:
            Console.WriteLine("Ugyldig valg.");
            break;
    }
}

// Innlogging
static User? Login(List<Student> students, List<Employee> employees)
{
    Console.WriteLine("Skriv inn brukernavn:");
    string username = Console.ReadLine() ?? "";

    Console.WriteLine("Skriv inn passord:");
    string password = Console.ReadLine() ?? "";

    User? user = students.FirstOrDefault(s =>
        s.Username.Equals(username, StringComparison.OrdinalIgnoreCase)
        && s.Password == password);

    if (user == null)
    {
        user = employees.FirstOrDefault(e =>
            e.Username.Equals(username, StringComparison.OrdinalIgnoreCase)
            && e.Password == password);
    }

    if (user == null)
    {
        Console.WriteLine("Feil brukernavn eller passord.");
        return null;
    }

    Console.WriteLine($"Velkommen {user.Name}!");
    return user;
}

// Registrering av ny bruker
static void RegisterUser(List<Student> students, List<Employee> employees)
{
    Console.WriteLine("Skriv inn navn:");
    string name = Console.ReadLine() ?? "";

    Console.WriteLine("Skriv inn e-post:");
    string email = Console.ReadLine() ?? "";

    Console.WriteLine("Velg brukernavn:");
    string username = Console.ReadLine() ?? "";

    Console.WriteLine("Velg passord:");
    string password = Console.ReadLine() ?? "";

    if (string.IsNullOrWhiteSpace(name) ||
        string.IsNullOrWhiteSpace(email) ||
        string.IsNullOrWhiteSpace(username) ||
        string.IsNullOrWhiteSpace(password))
    {
        Console.WriteLine("Alle felter må fylles ut.");
        return;
    }

    bool usernameExists =
        students.Any(s => s.Username.Equals(username, StringComparison.OrdinalIgnoreCase)) ||
        employees.Any(e => e.Username.Equals(username, StringComparison.OrdinalIgnoreCase));

    if (usernameExists)
    {
        Console.WriteLine("Brukernavnet finnes allerede.");
        return;
    }

    Console.WriteLine("Velg rolle:");
    Console.WriteLine("1. Student");
    Console.WriteLine("2. Faglærer");
    Console.WriteLine("3. Bibliotekansatt");

    string roleChoice = Console.ReadLine() ?? "";

    switch (roleChoice)
    {
        case "1":
            Console.WriteLine("Skriv inn student ID:");
            string studentId = Console.ReadLine() ?? "";

            students.Add(new Student
            {
                Name = name,
                Email = email,
                Username = username,
                Password = password,
                StudentId = studentId
            });

            Console.WriteLine("Student registrert.");
            break;

        case "2":
            Console.WriteLine("Skriv inn ansatt ID:");
            string teacherId = Console.ReadLine() ?? "";

            employees.Add(new Employee
            {
                Name = name,
                Email = email,
                Username = username,
                Password = password,
                EmployeeId = teacherId,
                Position = "Lærer",
                Department = "Informatikk",
                Role = UserRole.Teacher
            });

            Console.WriteLine("Faglærer registrert.");
            break;

        case "3":
            Console.WriteLine("Skriv inn ansatt ID:");
            string librarianId = Console.ReadLine() ?? "";

            employees.Add(new Employee
            {
                Name = name,
                Email = email,
                Username = username,
                Password = password,
                EmployeeId = librarianId,
                Position = "Bibliotekar",
                Department = "Bibliotek",
                Role = UserRole.Librarian
            });

            Console.WriteLine("Bibliotekansatt registrert.");
            break;

        default:
            Console.WriteLine("Ugyldig rollevalg.");
            break;
    }
}

//Show Rolle meny 
static void ShowRoleMenu(User user, List<Course> courses, List<Student> students, List<Employee> employees, Library library)
{
    if (user.Role == UserRole.Student)
    {
        StudentMenu((Student)user, courses, library);
    }
    else if (user.Role == UserRole.Teacher)
    {
        TeacherMenu((Employee)user, courses, students, library);
    }
    else if (user.Role == UserRole.Librarian)
    {
        LibrarianMenu(library);
    }
}

//Rollebasert meny
//Lærer
static void TeacherMenu(Employee teacher, List<Course> courses, List<Student> students, Library library)
{
    while (true)
    {
        Console.WriteLine("\n--- Faglærermeny ---");
        Console.WriteLine("1. Opprett kurs");
        Console.WriteLine("2. Søk på kurs");
        Console.WriteLine("3. Se alle kurs");
        Console.WriteLine("4. Søk på bok");
        Console.WriteLine("5. Lån bok");
        Console.WriteLine("6. Returner bok");
        Console.WriteLine("7. Sett karakter");
        Console.WriteLine("8. Registrer pensum");
        Console.WriteLine("0. Logg ut");

        string choice = Console.ReadLine() ?? "";

        switch (choice)
        {
            case "1":
                CreateCourse(courses, teacher);
                break;

            case "2":
                SearchCourse(courses);
                break;

            case "3":
                PrintCourses(courses);
                break;

            case "4":
                SearchBook(library);
                break;

            case "5":
                BorrowBookForUser(library, teacher);
                break;

            case "6":
                ReturnBookForUser(library, teacher);
                break;

            case "7":
                SetGrade(courses, students, teacher);
                break;

            case "8":
                RegisterCurriculum(courses, library, teacher);
                break;

            case "0":
                return;

            default:
                Console.WriteLine("Ugyldig valg.");
                break;
        }
    }
}

//Student
static void StudentMenu(Student student, List<Course> courses, Library library)
{
    while (true)
    {
        Console.WriteLine("\n--- Studentmeny ---");
        Console.WriteLine("1. Meld deg på kurs");
        Console.WriteLine("2. Meld deg av kurs");
        Console.WriteLine("3. Se mine kurs");
        Console.WriteLine("4. Se karakterer");
        Console.WriteLine("5. Søk på bok");
        Console.WriteLine("6. Lån bok");
        Console.WriteLine("7. Returner bok");
        Console.WriteLine("0. Logg ut");

        string choice = Console.ReadLine() ?? "";

        switch (choice)
        {
            case "1":
                EnrollStudentInCourse(student, courses);
                break;

            case "2":
                UnenrollStudentFromCourse(student, courses);
                break;

            case "3":
                PrintStudentCourses(student);
                break;

            case "4":
                PrintGrades(student);
                break;

            case "5":
                SearchBook(library);
                break;

            case "6":
                BorrowBookForUser(library, student);
                break;

            case "7":
                ReturnBookForUser(library, student);
                break;

            case "0":
                return;

            default:
                Console.WriteLine("Ugyldig valg.");
                break;
        }
    }
}

//Bibliotekar
static void LibrarianMenu(Library library)
{
    while (true)
    {
        Console.WriteLine("\n--- Bibliotekmeny ---");
        Console.WriteLine("1. Registrer bok");
        Console.WriteLine("2. Se aktive lån og historikk");
        Console.WriteLine("3. Søk på bok");
        Console.WriteLine("0. Logg ut");

        string choice = Console.ReadLine() ?? "";

        switch (choice)
        {
            case "1":
                RegisterBook(library);
                break;

            case "2":
                PrintLoans(library);
                break;

            case "3":
                SearchBook(library);
                break;

            case "0":
                return;

            default:
                Console.WriteLine("Ugyldig valg.");
                break;
        }
    }
}

//Opprete kurs
static void CreateCourse(List<Course> courses, Employee teacher)
{
    Console.WriteLine("Skriv inn kurs ID:");
    string courseId = Console.ReadLine() ?? "";

    Console.WriteLine("Skriv inn kurs navn:");
    string courseName = Console.ReadLine() ?? "";

    if (courses.Any(c => c.CourseId == courseId || c.CourseName == courseName))
    {
        Console.WriteLine("Kurs med samme ID eller navn finnes allerede.");
        return;
    }

    Console.WriteLine("Skriv inn antall studiepoeng:");
    if (!int.TryParse(Console.ReadLine(), out int credits))
    {
        Console.WriteLine("Ugyldig tall.");
        return;
    }

    Console.WriteLine("Skriv inn maks antall studenter:");
    if (!int.TryParse(Console.ReadLine(), out int maxStudents))
    {
        Console.WriteLine("Ugyldig tall.");
        return;
    }

    Course newCourse = new Course
    {
        CourseId = courseId,
        CourseName = courseName,
        Credits = credits,
        MaxStudents = maxStudents,
        Teacher = teacher
    };

    courses.Add(newCourse);
    Console.WriteLine($"Kurs {courseName} opprettet.");
}

// Print kurs og studenter
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

//Enroll student i kurs 
static void EnrollStudentInCourse(Student student, List<Course> courses)
{
    Console.WriteLine("Skriv inn kurs ID:");
    string courseId = Console.ReadLine() ?? "";

    if (string.IsNullOrWhiteSpace(courseId))
    {
        Console.WriteLine("Ugyldig kurs ID.");
        return;
    }

    Course? course = courses.FirstOrDefault(c => c.CourseId == courseId);

    if (course == null)
    {
        Console.WriteLine("Kurs ikke funnet.");
        return;
    }

    if (course.EnrollStudent(student))
    {
        Console.WriteLine($"Du er meldt på {course.CourseName}.");
    }
    else
    {
        Console.WriteLine("Kunne ikke melde på. Du er kanskje allerede påmeldt eller kurset er fullt.");
    }
}

//Unenroll student fra kurs
static void UnenrollStudentFromCourse(Student student, List<Course> courses)
{
    Console.WriteLine("Skriv inn kurs ID:");
    string courseId = Console.ReadLine() ?? "";
    if (string.IsNullOrWhiteSpace(courseId))
    {
        Console.WriteLine("Ugyldig kurs ID.");
        return;
    }
    Course? course = courses.FirstOrDefault(c => c.CourseId == courseId);
    if (course == null)
    {
        Console.WriteLine("Kurs ikke funnet.");
        return;
    }
    if (course.RemoveStudent(student))
    {
        Console.WriteLine($"Du er meldt av {course.CourseName}.");
    }
    else
    {
        Console.WriteLine("Kunne ikke melde av. Du er kanskje ikke påmeldt dette kurset.");
    }
}

// Ny print studentens kurs
static void PrintStudentCourses(Student student)
{
    if (student.Courses.Count == 0)
    {
        Console.WriteLine("Du er ikke meldt på noen kurs.");
        return;
    }
    Console.WriteLine("Dine kurs:");
    foreach (var course in student.Courses)
    {
        Console.WriteLine($"- {course.CourseName} (ID: {course.CourseId}, Studiepoeng: {course.Credits})");
    }
}

// Print studentens karakterer
static void PrintGrades(Student student)
{
    if (student.Grades.Count == 0)
    {
        Console.WriteLine("Du har ingen karakterer enda.");
        return;
    }
    Console.WriteLine("Dine karakterer:");
    foreach (var grade in student.Grades)
    {
        Console.WriteLine($"- {grade.Key}: {grade.Value}");
    }
}

// Sett Karakterer
static void SetGrade(List<Course> courses, List<Student> students, Employee teacher)
{
    Console.WriteLine("Skriv inn kurs ID:");
    string courseId = Console.ReadLine() ?? "";
    Course? course = courses.FirstOrDefault(c => c.CourseId == courseId && c.Teacher == teacher);
    if (course == null)
    {
        Console.WriteLine("Kurs ikke funnet eller du er ikke lærer for dette kurset.");
        return;
    }

    Console.WriteLine("Skriv inn student ID:");
    string studentId = Console.ReadLine() ?? "";
    Student? student = students.FirstOrDefault(s => s.StudentId == studentId);
    if (student == null || !course.Students.Contains(student))
    {
        Console.WriteLine("Student ikke funnet eller ikke meldt på dette kurset.");
        return;
    }

    Console.WriteLine("Skriv inn karakter (A-F):");
    string grade = Console.ReadLine() ?? "";

    if (!new[] { "A", "B", "C", "D", "E", "F" }.Contains(grade.ToUpper()))
    {
        Console.WriteLine("Ugyldig karakter. Bruk A-F.");
        return;
    }

    student.Grades[course.CourseId] = grade.ToUpper();
    Console.WriteLine($"Karakter {grade.ToUpper()} satt for {student.Name} i {course.CourseName}.");
}

// Søk på kurs 
static void SearchCourse(List<Course> courses)
{
    Console.WriteLine("Skriv inn kurs ID eller navn for å søke:");
    string search = Console.ReadLine() ?? "";

    Course? course = courses.FirstOrDefault(c =>
        c.CourseId.Equals(search, StringComparison.OrdinalIgnoreCase) ||
        c.CourseName.Equals(search, StringComparison.OrdinalIgnoreCase));

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

// Sett pensum
static void RegisterCurriculum(List<Course> courses, Library library, Employee teacher)
{
    Console.WriteLine("Skriv inn kurs ID for å registrere pensum:");
    string courseId = Console.ReadLine() ?? "";
    Course? course = courses.FirstOrDefault(c => c.CourseId == courseId && c.Teacher == teacher);
    if (course == null)
    {
        Console.WriteLine("Kurs ikke funnet eller du er ikke lærer for dette kurset.");
        return;
    }

    Console.WriteLine("Skriv inn bok ID for pensum:");
    string bookId = Console.ReadLine() ?? "";
    Book? book = library.Books.FirstOrDefault(b => b.BookId == bookId);
    if (book == null)
    {
        Console.WriteLine("Bok ikke funnet i biblioteket.");
        return;
    }

    if (course.CurriculumBooks.Any(b => b.BookId == bookId))
    {
        Console.WriteLine("Denne boken er allerede registrert som pensum for dette kurset.");
        return;
    }

    course.CurriculumBooks.Add(book);
    Console.WriteLine($"Bok {book.Title} registrert som pensum for {course.CourseName}.");
}

// Søk på bok 
static void SearchBook(Library library)
{
    Console.WriteLine("Skriv inn bok ID eller tittel for å søke:");
    string search = Console.ReadLine() ?? "";

    Book? book = library.Books.FirstOrDefault(b =>
        b.BookId.Equals(search, StringComparison.OrdinalIgnoreCase) ||
        b.Title.Equals(search, StringComparison.OrdinalIgnoreCase));

    if (book == null)
    {
        Console.WriteLine("Bok ikke funnet.");
        return;
    }

    Console.WriteLine($"Bok: {book.Title} (ID: {book.BookId}, Forfatter: {book.Author}, År: {book.Year}, Eksemplarer: {book.Copies})");
}

//BorrowBookForUser og ReturnBookForUser for å unngå duplisering av kode i både StudentMenu og TeacherMenu
static void BorrowBookForUser(Library library, User borrower)
{
    Console.WriteLine("Skriv inn bok ID for å låne:");
    string bookId = Console.ReadLine() ?? "";

    if (string.IsNullOrWhiteSpace(bookId))
    {
        Console.WriteLine("Ugyldig bok ID.");
        return;
    }

    if (library.LoanBook(borrower, bookId))
    {
        Console.WriteLine($"{borrower.Name} har lånt boka.");
    }
    else
    {
        Console.WriteLine("Bok ikke tilgjengelig.");
    }
}

static void ReturnBookForUser(Library library, User borrower)
{
    Console.WriteLine("Skriv inn bok ID for å returnere:");
    string bookId = Console.ReadLine() ?? "";

    if (string.IsNullOrWhiteSpace(bookId))
    {
        Console.WriteLine("Ugyldig bok ID.");
        return;
    }

    if (library.ReturnBook(borrower, bookId))
    {
        Console.WriteLine($"{borrower.Name} har returnert boka.");
    }
    else
    {
        Console.WriteLine("Lån ikke funnet.");
    }
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

    Console.WriteLine("Skriv inn år:");
    if (!int.TryParse(Console.ReadLine(), out int year))
    {
        Console.WriteLine("Ugyldig år.");
        return;
    }

    Console.WriteLine("Skriv inn antall eksemplarer:");
    if (!int.TryParse(Console.ReadLine(), out int copies))
    {
        Console.WriteLine("Ugyldig antall.");
        return;
    }

    Book newBook = new Book
    {
        BookId = bookId,
        Title = title,
        Author = author,
        Year = year,
        Copies = copies
    };

    if (library.AddBook(newBook))
    {
        Console.WriteLine($"Bok {title} registrert i biblioteket.");
    }
    else
    {
        Console.WriteLine("En bok med samme ID finnes allerede.");
    }
}