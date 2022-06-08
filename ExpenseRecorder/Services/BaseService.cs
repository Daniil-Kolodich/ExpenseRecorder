using ExpenseRecorder.Exceptions ;
using ExpenseRecorder.Models.Interfaces ;
using ExpenseRecorder.Repositories.Interfaces ;
using ExpenseRecorder.Services.Interfaces ;
using ExpenseRecorder.UnitOfWork.Interfaces ;
using LanguageExt.Common ;

namespace ExpenseRecorder.Services ;

public class BaseService < T > : IBaseService< T >
	where T : class , IEntity< T >
{
	protected readonly IBaseRepository< T > _repository ;
	protected readonly IUnitOfWork          _unitOfWork ;

	public BaseService(IBaseRepository< T > repository , IUnitOfWork unitOfWork)
	{
		_repository = repository ;
		_unitOfWork = unitOfWork ;
	}

	public virtual async Task< Result< IEnumerable< T > > > GetAllAsync(Func< T , bool >? predicate = null)
	{
		var result                          = await _repository.GetAllAsync() ;
		if ( predicate is not null ) result = result.Where( predicate ) ;

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
			var result      = await _unitOfWork.SaveAsync() ;
			
			
			if ( addedEntity is null )
				return new Result< T >( new BadRequestException( $"Unable to save {typeof(T)} to perform AddAsync" ) ) ;

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
			entity.Id = id ;
			var updatedEntity = await _repository.UpdateAsync( id , entity ) ;
			var result        = await _unitOfWork.SaveAsync() ;

			if ( updatedEntity is null )
				return new Result< T >(
					new BadRequestException( $"Unable to find {typeof(T)} with {id} to perform UpdateAsync" ) ) ;

			if ( !result )
				return new Result< T >(
					new SaveContextException( $"Unable to save {typeof(T)} to perform UpdateAsync" ) ) ;

			return new Result< T >( updatedEntity ) ;
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
