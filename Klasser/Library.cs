namespace ObligatoriskOppgave1.Models;

public class Library : ILibraryService
{
   public List<Book> Books { get; set; } = new List<Book>();
    public List<Loan> Loans { get; set; } = new List<Loan>();

    public bool AddBook(Book book)
{
    if (Books.Any(b => b.BookId == book.BookId))
    {
        return false;
    }

    Books.Add(book);
    return true;
}
    public bool LoanBook(User borrower, string bookId)
    {
        Book? book = Books.FirstOrDefault(b => b.BookId == bookId);
        if (book == null || book.Copies <= 0)
        {
            return false; // Boken er ikke tilgjengelig
        }
        book.Copies--;
        Loans.Add(new Loan
        {
            Borrower = borrower,
            Book = book,
            LoanDate = DateTime.Now,
            DueDate = DateTime.Now.AddDays(14),
            Returned = false
        });
        return true;
    }
    public bool ReturnBook(User borrower, string bookId)
    {
        Loan? loan = Loans.FirstOrDefault(l => 
        l.Borrower == borrower && 
        l.Book.BookId == bookId && 
        !l.Returned);
        if (loan == null)
        {
            return false; // Lånet finnes ikke
        }
        loan.Returned = true;
        loan.ReturnDate = DateTime.Now;
        loan.Book.Copies++;
        return true;
    }
}