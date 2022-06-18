using ExpenseRecorder.Context ;
using ExpenseRecorder.Repositories.Interfaces ;
using ExpenseRecorder.Services.Interfaces ;
using Microsoft.EntityFrameworkCore ;

namespace ExpenseRecorder.Repositories ;

public class PaymentAccountRepository : BaseRepository< PaymentAccount > , IPaymentAccountRepository
{
	public PaymentAccountRepository(ExpenseRecorderContext context , IAuthenticationService authenticationService)
		: base( context , authenticationService )
	{ }

	public override async Task< PaymentAccount? > GetAsync(int id , bool tracking = true)
	{
		return await ( tracking ? Data : Data.AsNoTracking() )
					.Include( p => p.Transactions ).ThenInclude(t => t.Category)
					.FirstOrDefaultAsync( x => x.Id == id && x.UserId == UserId ) ;
	}
}
