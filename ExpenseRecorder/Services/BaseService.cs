using ExpenseRecorder.Exceptions ;
using ExpenseRecorder.Models.Interfaces ;
using ExpenseRecorder.Repositories.Interfaces ;
using ExpenseRecorder.SearchHandlers.Options.Interfaces ;
using ExpenseRecorder.SearchHandlers.Queries.Interfaces ;
using ExpenseRecorder.Services.Interfaces ;
using ExpenseRecorder.Services.Result ;
using ExpenseRecorder.UnitOfWork.Interfaces ;
using Microsoft.EntityFrameworkCore ;

namespace ExpenseRecorder.Services ;

public class BaseService < T > : IBaseService< T >
	where T : class , IUserEntity< T >
{
	private protected readonly IBaseRepository< T > _repository ;
	private readonly           IUnitOfWork          _unitOfWork ;
	private readonly           ISearchQuery< T >?   _queryBuilder ;

	public BaseService(IBaseRepository< T > repository , IUnitOfWork unitOfWork , ISearchQuery< T >? queryBuilder = null)
	{
		_repository   = repository ;
		_unitOfWork   = unitOfWork ;
		_queryBuilder = queryBuilder ;
	}

	public virtual async Task< Result< IEnumerable< T > > > GetAllAsync(ISearchOptions< T >? searchOptions = null)
	{
		var query = _repository.GetAllAsQueryable() ;

		if ( searchOptions is not null ) { query = _queryBuilder?.SetBaseQuery( query ).Build( searchOptions ) ?? query ; }

		var result = await query.ToListAsync() ;

		return new Result< IEnumerable< T > >( result ) ;
	}

	public virtual async Task< Result< T > > GetAsync(int id)
	{
		try
		{
			var result = await _repository.GetAsync( id ) ;

			if ( result is null )
				return new Result< T >(
					new NotFoundException( $"Unable to find {typeof(T)} with {id} to perform GetAsync" ) ) ;

			return new Result< T >( result ) ;
		}
		catch ( Exception ex ) { return new Result< T >( ex ) ; }
	}

	public virtual async Task< Result< T > > AddAsync(T entity)
	{
		try
		{
			var addedEntity = await _repository.AddAsync( entity ) ;

			if ( addedEntity is null )
				return new Result< T >( new BadRequestException( $"Unable to add {typeof(T)} to perform AddAsync" ) ) ;

			var result = await _unitOfWork.SaveAsync() ;

			if ( !result )
				return new Result< T >(
					new SaveContextException( $"Unable to save {typeof(T)} to perform AddAsync" ) ) ;

			return new Result< T >( addedEntity ) ;
		}
		catch ( Exception ex ) { return new Result< T >( ex ) ; }
	}

	public virtual async Task< Result< T > > UpdateAsync(int id , T entity)
	{
		try
		{
			var oldEntity = await _repository.UpdateAsync( id , entity ) ;

			if ( oldEntity is null )
				return new Result< T >(
					new BadRequestException( $"Unable to find {typeof(T)} with {id} to perform UpdateAsync" ) ) ;

			var result = await _unitOfWork.SaveAsync() ;

			if ( !result )
				return new Result< T >(
					new SaveContextException( $"Unable to save {typeof(T)} to perform UpdateAsync" ) ) ;

			return new Result< T >( oldEntity ) ;
		}
		catch ( Exception ex ) { return new Result< T >( ex ) ; }
	}

	public virtual async Task< Result< T > > DeleteAsync(int id)
	{
		try
		{
			var deletedEntity = await _repository.DeleteAsync( id ) ;

			if ( deletedEntity is null )
				return new Result< T >(
					new NotFoundException( $"Unable to find {typeof(T)} with {id} to perform DeleteAsync" ) ) ;

			var result = await _unitOfWork.SaveAsync() ;

			if ( !result )
				return new Result< T >(
					new SaveContextException( $"Unable to save {typeof(T)} to perform DeleteAsync" ) ) ;

			return new Result< T >( deletedEntity ) ;
		}
		catch ( Exception ex ) { return new Result< T >( ex ) ; }
	}
}
