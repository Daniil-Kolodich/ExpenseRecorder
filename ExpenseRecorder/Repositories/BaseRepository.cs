using ExpenseRecorder.Context ;
using ExpenseRecorder.Models.Interfaces ;
using ExpenseRecorder.Repositories.Interfaces ;
using Microsoft.EntityFrameworkCore ;

namespace ExpenseRecorder.Repositories ;

public class BaseRepository < T > : IBaseRepository< T >
	where T : class , IEntity< T >
{
	protected readonly ExpenseRecorderContext _context ;

	public BaseRepository(ExpenseRecorderContext context)
	{
		_context = context ;
		Data     = _context.Set< T >() ;
	}

	public DbSet< T > Data { get ; }

	public virtual async Task< IEnumerable< T > > GetAllAsync(bool tracking = false)
	{
		if ( tracking ) return await Data.ToListAsync() ;

		return await Data.AsNoTracking().ToListAsync() ;
	}

	// TODO: single or first ? that is the question, especially for the id
	public virtual async Task< T? > GetAsync(int id , bool tracking = true)
	{
		if ( tracking ) return await Data.SingleOrDefaultAsync( e => e.Id == id ) ;

		return await Data.AsNoTracking().SingleOrDefaultAsync( e => e.Id == id ) ;
	}

	public virtual async Task< T? > AddAsync(T entity)
	{
		return ( await Data.AddAsync( entity ) ).Entity ;
	}

	public virtual async Task< T? > UpdateAsync(int id , T entity)
	{
		var old = await GetAsync( id , false ) ;

		if ( old == null ) return null ;

		old.CopyFrom( entity ) ;
		_context.Entry( old ).State = EntityState.Modified ;

		return old ;
	}

	public virtual async Task< T? > DeleteAsync(int id)
	{
		var entity = await GetAsync( id ) ;

		if ( entity == null ) return null ;

		return Data.Remove( entity ).Entity ;
	}
}
