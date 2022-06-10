using ExpenseRecorder.Context ;
using ExpenseRecorder.Models ;
using ExpenseRecorder.Repositories.Interfaces ;
using ExpenseRecorder.Services.Interfaces ;

namespace ExpenseRecorder.Repositories ;

public class CategoryRepository : BaseRepository< Category > , ICategoryRepository
{
	public CategoryRepository(ExpenseRecorderContext context, IAuthenticationService authenticationService)
		: base( context , authenticationService)
	{
	}
}
