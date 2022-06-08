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


	public override async Task< Result< IEnumerable< Category > > > GetAllAsync(
		Func< Category , bool >? predicate = null)
	{
		var user = _authenticationService.CurrentUser ;

		if ( user is null ) return new Result< IEnumerable< Category > >( new ArgumentException() ) ;

		return await base.GetAllAsync( c => c.UserId == user.Id ) ;
	}

	public override async Task< Result< Category > > GetAsync(int id , Func< Category , bool >? predicate = null)
	{
		var user = _authenticationService.CurrentUser ;

		if ( user is null ) return new Result< Category >( new ArgumentException() ) ;

		return await base.GetAsync( id , c => c.UserId == user.Id ) ;
	}

	public override async Task< Result< Category > > AddAsync(Category entity)
	{
		var user = _authenticationService.CurrentUser ;

		if ( user is null ) return new Result< Category >( new ArgumentException() ) ;

		entity.UserId = user.Id ;

		return await base.AddAsync( entity ) ;
	}

	public override async Task< Result< Category > > UpdateAsync(int id , Category entity)
	{
		// TODO :: add check if user is owner of category
		var user = _authenticationService.CurrentUser ;

		if ( user is null ) return new Result< Category >( new ArgumentException() ) ;

		var category = await _repository.GetAsync( id , false ) ;

		if ( category is null || category.UserId != user.Id ) return new Result< Category >( new ArgumentException() ) ;

		return await base.UpdateAsync( id , entity ) ;
	}

	public override async Task< Result< Category > > DeleteAsync(int id , Func< Category , bool >? predicate = null)
	{
		var user = _authenticationService.CurrentUser ;

		if ( user is null ) return new Result< Category >( new ArgumentException() ) ;

		return await base.DeleteAsync( id , c => c.UserId == user.Id ) ;
	}
}
