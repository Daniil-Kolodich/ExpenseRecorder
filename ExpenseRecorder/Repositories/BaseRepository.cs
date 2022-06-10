using ExpenseRecorder.Context ;
using ExpenseRecorder.Models.Interfaces ;
using ExpenseRecorder.Repositories.Interfaces ;
using ExpenseRecorder.Services.Interfaces ;
using Microsoft.EntityFrameworkCore ;

namespace ExpenseRecorder.Repositories ;

public class BaseRepository < T > : IBaseRepository< T >
	where T : class , IUserEntity< T >
{
	private readonly ExpenseRecorderContext _context ;
	private readonly IAuthenticationService _authenticationService ;
	protected BaseRepository(ExpenseRecorderContext context , IAuthenticationService authenticationService)
	{
		_context                    = context ;
		_authenticationService = authenticationService ;
		Data                        = _context.Set< T >() ;
	}

	public DbSet< T > Data   { get ; }
	public string     UserId => _authenticationService.CurrentUser?.Id ?? throw new ArgumentException("No user logged in");

	public virtual IQueryable< T > GetAllAsQueryable(bool tracking = false)
	{
		return ( tracking ? Data.AsTracking() : Data.AsNoTracking() ).Where( x => x.UserId == UserId ) ;
	}

	public virtual async Task< T? > GetAsync(int id , bool tracking = true)
	{
		return await ( tracking ? Data : Data.AsNoTracking() ).FirstOrDefaultAsync( x =>
			x.Id == id && x.UserId == UserId ) ;
	}

	public virtual async Task< T? > AddAsync(T entity)
	{
		entity.UserId = UserId ;
		return ( await Data.AddAsync( entity ) ).Entity ;
	}

	public virtual async Task< T? > UpdateAsync(int id , T entity)
	{
		// TODO : what if i get as tracking ?
		var old = await GetAsync( id , false ) ;

		if ( old        == null ) return null ;
		if ( old.UserId != UserId ) return null ;

		old.CopyFrom( entity ) ;
		_context.Entry( old ).State = EntityState.Modified ;

		return old ;
	}

	public virtual async Task< T? > DeleteAsync(int id)
	{
		var entity = await GetAsync( id ) ;

		if ( entity        == null ) return null ;
		if ( entity.UserId != UserId ) return null ;

		return Data.Remove( entity ).Entity ;
	}
}
