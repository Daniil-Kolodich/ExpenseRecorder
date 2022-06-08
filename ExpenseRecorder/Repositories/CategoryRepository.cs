using ExpenseRecorder.Context ;
using ExpenseRecorder.Models ;
using ExpenseRecorder.Repositories.Interfaces ;
using Microsoft.EntityFrameworkCore ;

namespace ExpenseRecorder.Repositories ;

public class CategoryRepository : BaseRepository< Category > , ICategoryRepository
{
	public CategoryRepository(ExpenseRecorderContext context)
		: base( context )
	{ }

	// TODO : Do i fucking need this ?
	public override async Task< IEnumerable< Category > > GetAllAsync(bool tracking = false)
	{
		if ( tracking ) return await Data.Include( d => d.User ).ToListAsync() ;

		return await Data.Include( d => d.User ).AsNoTracking().ToListAsync() ;
	}

	public override async Task< Category? > GetAsync(int id , bool tracking = true)
	{
		if ( tracking ) return await Data.Include( d => d.User ).SingleOrDefaultAsync( d => d.Id == id ) ;

		return await Data.Include( d => d.User ).AsNoTracking().SingleOrDefaultAsync( d => d.Id == id ) ;
	}
}
