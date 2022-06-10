using ExpenseRecorder.Context ;
using ExpenseRecorder.UnitOfWork.Interfaces ;

namespace ExpenseRecorder.UnitOfWork ;

public class UnitOfWork : IUnitOfWork
{
	private readonly ExpenseRecorderContext _context ;

	public UnitOfWork(ExpenseRecorderContext context) { _context = context ; }

	public async Task< bool > SaveAsync()
	{
		try { return await _context.SaveChangesAsync() > 0 ; }
		catch ( Exception ) { return false ; }
	}
}
