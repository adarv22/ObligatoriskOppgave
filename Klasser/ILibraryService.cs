namespace ObligatoriskOppgave1.Models;

public interface ILibraryService
{
    bool AddBook(Book book);
    bool LoanBook(User borrower, string bookId);
    bool ReturnBook(User borrower, string bookId);
}