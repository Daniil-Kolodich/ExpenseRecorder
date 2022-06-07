using ExpenseRecorder.Models ;

namespace ExpenseRecorder.Services.Interfaces ;

public interface IAuthenticationService
{
	User? CurrentUser { get ; }
}
