using ExpenseRecorder.Exceptions ;
using ExpenseRecorder.Models ;
using ExpenseRecorder.Repositories.Interfaces ;
using ExpenseRecorder.Services.Interfaces ;
using ExpenseRecorder.UnitOfWork.Interfaces ;
using LanguageExt.Common ;

namespace ExpenseRecorder.Services ;

public class CategoryService : BaseService< Category > , ICategoryService
{
	private readonly IAuthenticationService _authenticationService ;

	public CategoryService(
		ICategoryRepository    repository ,
		IUnitOfWork            unitOfWork ,
		IAuthenticationService authenticationService)
		: base( repository , unitOfWork )
	{
		_authenticationService = authenticationService ;
	}

	public override async Task< Result< IEnumerable< Category > > > GetAllAsync()
	{
		var user = _authenticationService.CurrentUser ;

		if ( user is null )
			return new Result< IEnumerable< Category > >( new NotAuthenticatedException( "Not authenticated" ) ) ;

		_filters.Add( x => x.UserId == user.Id ) ;

		return await base.GetAllAsync() ;
	}

// TODO replace func for predicate
	public override async Task< Result< Category > > GetAsync(int id , Func< Category , bool >? predicate = null)
	{
		var user = _authenticationService.CurrentUser ;

		if ( user is null ) return new Result< Category >( new NotAuthenticatedException( "Not authenticated" ) ) ;

		return await base.GetAsync( id , c => c.UserId == user.Id ) ;
	}

	public override async Task< Result< Category > > AddAsync(Category entity)
	{
		var user = _authenticationService.CurrentUser ;

		if ( user is null ) return new Result< Category >( new NotAuthenticatedException( "Not authenticated" ) ) ;

		entity.UserId = user.Id ;

		return await base.AddAsync( entity ) ;
	}

	public override async Task< Result< Category > > UpdateAsync(
		int                      id ,
		Category                 entity ,
		Func< Category , bool >? predicate = null)
	{
		var user = _authenticationService.CurrentUser ;

		if ( user is null ) return new Result< Category >( new NotAuthenticatedException( "Not authenticated" ) ) ;

		return await base.UpdateAsync( id , entity , c => c.UserId == user.Id ) ;
	}

	public override async Task< Result< Category > > DeleteAsync(int id , Func< Category , bool >? predicate = null)
	{
		var user = _authenticationService.CurrentUser ;

		if ( user is null ) return new Result< Category >( new NotAuthenticatedException( "Not authenticated" ) ) ;

		return await base.DeleteAsync( id , c => c.UserId == user.Id ) ;
	}
}
