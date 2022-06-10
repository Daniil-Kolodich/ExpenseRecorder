using ExpenseRecorder.Context ;
using ExpenseRecorder.Models ;
using ExpenseRecorder.Repositories.Interfaces ;
using ExpenseRecorder.Services.Interfaces ;
using Microsoft.EntityFrameworkCore ;

namespace ExpenseRecorder.Repositories ;

public class TransactionRepository : BaseRepository< Transaction > , ITransactionRepository
{
	public TransactionRepository(ExpenseRecorderContext context , IAuthenticationService authenticationService)
		: base( context , authenticationService )
	{ }

	public override IQueryable< Transaction > GetAllAsQueryable(bool tracking = false)
	{
		return base.GetAllAsQueryable( tracking ).Include( t => t.Category ).Include( t => t.PaymentAccount) ;
	}

	public override async Task< Transaction? > GetAsync(int id , bool tracking = true)
	{
		return await ( tracking ? Data : Data.AsNoTracking() ).Include( t => t.Category ).Include( t => t.PaymentAccount)
															  .FirstOrDefaultAsync( x => x.Id == id && x.UserId == UserId ) ;
	}
}
