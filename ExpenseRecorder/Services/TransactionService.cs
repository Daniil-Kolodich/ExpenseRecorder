using ExpenseRecorder.Exceptions ;
using ExpenseRecorder.Models ;
using ExpenseRecorder.Repositories.Interfaces ;
using ExpenseRecorder.Services.Interfaces ;
using ExpenseRecorder.UnitOfWork.Interfaces ;
using LanguageExt.Common ;

namespace ExpenseRecorder.Services ;

public class TransactionService : BaseService< Transaction > , ITransactionService
{
	private readonly IAuthenticationService _authenticationService ;
	private readonly ICategoryRepository    _categoryRepository ;

	public TransactionService(
		ITransactionRepository repository ,
		ICategoryRepository    categoryRepository ,
		IUnitOfWork            unitOfWork ,
		IAuthenticationService authenticationService)
		: base( repository , unitOfWork )
	{
		_authenticationService = authenticationService ;
		_categoryRepository    = categoryRepository ;
	}

	public override async Task< Result< IEnumerable< Transaction > > > GetAllAsync()
	{
		var user = _authenticationService.CurrentUser ;

		if ( user is null )
			return new Result< IEnumerable< Transaction > >( new NotAuthenticatedException( "Not authenticated" ) ) ;

		_filters.Add( x => x.UserId == user.Id ) ;

		return await base.GetAllAsync() ;
	}

	public override async Task< Result< Transaction > > GetAsync(int id , Func< Transaction , bool >? predicate = null)
	{
		var user = _authenticationService.CurrentUser ;

		if ( user is null ) return new Result< Transaction >( new NotAuthenticatedException( "Not authenticated" ) ) ;

		return await base.GetAsync( id , c => c.UserId == user.Id ) ;
	}

	public override async Task< Result< Transaction > > AddAsync(Transaction entity)
	{
		var user = _authenticationService.CurrentUser ;

		if ( user is null ) return new Result< Transaction >( new NotAuthenticatedException( "Not authenticated" ) ) ;

		entity.UserId = user.Id ;

		var category = await _categoryRepository.GetAsync( entity.CategoryId ) ;
		if (category is null || category.UserId != user.Id)
			return new Result< Transaction >( new NotFoundException( "Not found such category" ) ) ;
		return await base.AddAsync( entity ) ;
	}

	public override async Task< Result< Transaction > > UpdateAsync(
		int                         id ,
		Transaction                 entity ,
		Func< Transaction , bool >? predicate = null)
	{
		var user = _authenticationService.CurrentUser ;

		if ( user is null ) return new Result< Transaction >( new NotAuthenticatedException( "Not authenticated" ) ) ;

		var category = await _categoryRepository.GetAsync( entity.CategoryId ) ;
		if (category is null || category.UserId != user.Id)
			return new Result< Transaction >( new NotFoundException( "Not found such category !!" ) ) ;

		return await base.UpdateAsync( id , entity , c => c.UserId == user.Id) ;
	}

	public override async Task< Result< Transaction > > DeleteAsync(
		int                         id ,
		Func< Transaction , bool >? predicate = null)
	{
		var user = _authenticationService.CurrentUser ;

		if ( user is null ) return new Result< Transaction >( new NotAuthenticatedException( "Not authenticated" ) ) ;

		return await base.DeleteAsync( id , c => c.UserId == user.Id ) ;
	}
}
