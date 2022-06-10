using ExpenseRecorder.Context ;
using ExpenseRecorder.Repositories.Interfaces ;
using ExpenseRecorder.Services.Interfaces ;

namespace ExpenseRecorder.Repositories ;

public class PaymentAccountRepository : BaseRepository< PaymentAccount > , IPaymentAccountRepository
{
	public PaymentAccountRepository(ExpenseRecorderContext context , IAuthenticationService authenticationService)
		: base( context , authenticationService )
	{ }
}
