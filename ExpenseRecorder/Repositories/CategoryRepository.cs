using ExpenseRecorder.Context ;
using ExpenseRecorder.Models ;
using ExpenseRecorder.Repositories.Interfaces ;

namespace ExpenseRecorder.Repositories ;

public class CategoryRepository : BaseRepository< Category > , ICategoryRepository
{
	public CategoryRepository(ExpenseRecorderContext context)
		: base( context )
	{ }
}
