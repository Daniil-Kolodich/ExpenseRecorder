using ExpenseRecorder.Context ;
using ExpenseRecorder.Models ;
using ExpenseRecorder.Repositories.Interfaces ;
using Microsoft.EntityFrameworkCore ;

namespace ExpenseRecorder.Repositories ;

public class TransactionRepository : BaseRepository< Transaction > , ITransactionRepository
{
	public TransactionRepository(ExpenseRecorderContext context)
		: base( context )
	{ }

	public override IQueryable< Transaction > GetAllAsQueryable(bool tracking = false)
	{
		return ( tracking ? Data.AsQueryable() : Data.AsNoTracking() ).Include( t => t.Category ) ;
	}

	public override async Task< Transaction? > GetAsync(int id , bool tracking = true)
	{
		return await ( tracking ? Data : Data.AsNoTracking() ).Include( t => t.Category )
															  .FirstOrDefaultAsync( x => x.Id == id ) ;
	}
}
