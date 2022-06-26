using ExpenseRecorder.Models ;
using ExpenseRecorder.Repositories.Interfaces ;
using ExpenseRecorder.Services.Interfaces ;
using ExpenseRecorder.UnitOfWork.Interfaces ;

namespace ExpenseRecorder.Services ;

public class CategoryService : BaseService< Category > , ICategoryService
{
	public CategoryService(
		ICategoryRepository repository ,
		IUnitOfWork         unitOfWork)
		: base( repository , unitOfWork )
	{ }
}
