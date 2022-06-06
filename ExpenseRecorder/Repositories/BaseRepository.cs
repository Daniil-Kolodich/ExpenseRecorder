using ExpenseRecorder.Context ;
using ExpenseRecorder.Models.Interfaces ;
using ExpenseRecorder.Repositories.Interfaces ;
using Microsoft.EntityFrameworkCore ;

namespace ExpenseRecorder.Repositories ;

public class BaseRepository < T > : IBaseRepository< T >
	where T : class , IEntity
{
	protected readonly ExpenseRecorderContext _context ;
	public             DbSet< T >             Data { get ; private set ; }

	public BaseRepository(ExpenseRecorderContext context)
	{
		_context = context ;
		Data     = _context.Set< T >() ;
	}

	public virtual async Task< IEnumerable< T > > GetAllAsync() => await Data.ToListAsync() ;
	public virtual async Task< T? > GetAsync(int id) => await Data.SingleOrDefaultAsync( e => e.Id == id ) ;

	public virtual async Task< T? > AddAsync(T entity) => ( await Data.AddAsync( entity ) ).Entity ;

	public virtual async Task< T? > UpdateAsync(int id , T entity)
	{
		var old = await GetAsync( id ) ;

		if ( old == null ) return null ;

		return Data.Update( entity ).Entity ;
	}

	public virtual async Task< T? > DeleteAsync(int id)
	{
		var entity = await GetAsync( id ) ;

		if ( entity == null ) return null ;

		return Data.Remove( entity ).Entity ;
	}
}
