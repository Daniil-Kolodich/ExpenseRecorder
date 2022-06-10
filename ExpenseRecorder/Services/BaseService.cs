using System.Linq.Expressions ;
using ExpenseRecorder.Exceptions ;
using ExpenseRecorder.Models.Interfaces ;
using ExpenseRecorder.Repositories.Interfaces ;
using ExpenseRecorder.Services.Interfaces ;
using ExpenseRecorder.UnitOfWork.Interfaces ;
using LanguageExt.Common ;
using Microsoft.EntityFrameworkCore ;

namespace ExpenseRecorder.Services ;

public class BaseService < T > : IBaseService< T >
	where T : class , IEntity< T >
{
	private protected readonly IBaseRepository< T > _repository ;
	private readonly IUnitOfWork _unitOfWork ;
	private protected IList< Expression< Func< T , bool > > > _filters = new List< Expression< Func< T , bool > > >() ;

	public BaseService(IBaseRepository< T > repository , IUnitOfWork unitOfWork)
	{
		_repository = repository ;
		_unitOfWork = unitOfWork ;
	}

	public virtual async Task< Result< IEnumerable< T > > > GetAllAsync()
	{
		var query = _repository.GetAllAsQueryable() ;

		if ( _filters.Any() )
			query = _filters.Aggregate( query , (current , filter) => current.Where( filter ) ) ;

		var result = await query.ToListAsync() ;

		return new Result< IEnumerable< T > >( result ) ;
	}

	public virtual async Task< Result< T > > GetAsync(int id , Func< T , bool >? predicate = null)
	{
		try
		{
			var result = await _repository.GetAsync( id ) ;

			if ( result is null )
				return new Result< T >(
					new NotFoundException( $"Unable to find {typeof(T)} with {id} to perform GetAsync" ) ) ;

			if ( predicate is not null && !predicate( result ) )
				return new Result< T >( new PredicateMismatchException( "Result doesn't fit predicate" ) ) ;

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

	public virtual async Task< Result< T > > UpdateAsync(int id , T entity , Func< T , bool >? predicate = null)
	{
		try
		{
			var oldEntity = await _repository.UpdateAsync( id , entity ) ;

			if ( oldEntity is null )
				return new Result< T >(
					new BadRequestException( $"Unable to find {typeof(T)} with {id} to perform UpdateAsync" ) ) ;

			if ( predicate is not null && !predicate( oldEntity ) )
				return new Result< T >( new PredicateMismatchException( "Result doesn't fit predicate" ) ) ;

			var result = await _unitOfWork.SaveAsync() ;

			if ( !result )
				return new Result< T >(
					new SaveContextException( $"Unable to save {typeof(T)} to perform UpdateAsync" ) ) ;

			return new Result< T >( oldEntity ) ;
		}
		catch ( Exception ex ) { return new Result< T >( ex ) ; }
	}

	public virtual async Task< Result< T > > DeleteAsync(int id , Func< T , bool >? predicate = null)
	{
		try
		{
			var deletedEntity = await _repository.DeleteAsync( id ) ;

			if ( deletedEntity is null )
				return new Result< T >(
					new NotFoundException( $"Unable to find {typeof(T)} with {id} to perform DeleteAsync" ) ) ;

			if ( predicate is not null && !predicate( deletedEntity ) )
				return new Result< T >( new PredicateMismatchException( "Result doesn't fit predicate" ) ) ;

			var result = await _unitOfWork.SaveAsync() ;

			if ( !result )
				return new Result< T >(
					new SaveContextException( $"Unable to save {typeof(T)} to perform DeleteAsync" ) ) ;

			return new Result< T >( deletedEntity ) ;
		}
		catch ( Exception ex ) { return new Result< T >( ex ) ; }
	}
}
