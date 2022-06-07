using ExpenseRecorder.Context ;
using ExpenseRecorder.Models ;
using ExpenseRecorder.Repositories.Interfaces ;
using Microsoft.EntityFrameworkCore ;

namespace ExpenseRecorder.Repositories ;

public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
	public CategoryRepository(ExpenseRecorderContext context) : base(context)
	{
	}
	
	public override async Task< IEnumerable< Category > > GetAllAsync() => await Data.Include( d => d.User ).ToListAsync() ;
	public override async Task< Category? > GetAsync(int id) => await Data.Include( d => d.User ).SingleOrDefaultAsync( e => e.Id == id ) ;
}
