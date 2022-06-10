using ExpenseRecorder.Context ;
using ExpenseRecorder.Models.Interfaces ;
using ExpenseRecorder.Repositories.Interfaces ;
using Microsoft.EntityFrameworkCore ;

namespace ExpenseRecorder.Repositories ;

public class BaseRepository < T > : IBaseRepository< T >
	where T : class , IEntity< T >
{
	private readonly ExpenseRecorderContext _context ;

	protected BaseRepository(ExpenseRecorderContext context)
	{
		_context = context ;
		Data     = _context.Set< T >() ;
	}

	private DbSet< T > Data { get ; }

	public virtual IQueryable< T > GetAllAsQueryable(bool tracking = false) =>
		tracking ? Data.AsQueryable() : Data.AsNoTracking() ;

	public virtual async Task< T? > GetAsync(int id , bool tracking = true) =>
		await ( tracking ? Data : Data.AsNoTracking() ).FirstOrDefaultAsync( x => x.Id == id ) ;

	public virtual async Task< T? > AddAsync(T entity) => ( await Data.AddAsync( entity ) ).Entity ;

	public virtual async Task< T? > UpdateAsync(int id , T entity)
	{
		// TODO : what if i get as tracking ?
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
