using ExpenseRecorder.Models.Interfaces ;
using ExpenseRecorder.Repositories.Interfaces ;
using ExpenseRecorder.Services.Interfaces ;
using ExpenseRecorder.UnitOfWork.Interfaces ;
using LanguageExt.Common ;

namespace ExpenseRecorder.Services ;

public class BaseService < T > : IBaseService< T >
	where T : class , IEntity
{
	protected readonly IBaseRepository< T > _repository ;
	protected readonly IUnitOfWork          _unitOfWork ;

	public BaseService(IBaseRepository< T > repository , IUnitOfWork unitOfWork)
	{
		_repository = repository ;
		_unitOfWork = unitOfWork ;
	}

	public async Task< Result< IEnumerable< T > > > GetAllAsync()
	{
		return new Result< IEnumerable< T > >( await _repository.GetAllAsync() ) ;
	}

	public async Task< Result< T > > GetAsync(int id)
	{
		try
		{
			var result = await _repository.GetAsync( id ) ;

			// TODO : replace with custom exception
			if ( result is null ) return new Result< T >( new ArgumentException() ) ;

			return new Result< T >( result ) ;
		}
		catch ( Exception ex ) { return new Result< T >( ex ) ; }
	}

	public async Task< Result< T > > AddAsync(T entity)
	{
		try
		{
			var addedEntity = await _repository.AddAsync( entity ) ;
			var result      = await _unitOfWork.SaveAsync() ;

			if ( addedEntity is null || !result ) return new Result< T >( new ArgumentException() ) ;

			return new Result< T >( addedEntity ) ;
		}
		catch ( Exception ex ) { return new Result< T >( ex ) ; }
	}

	public async Task< Result< T > > UpdateAsync(int id , T entity)
	{
		try
		{
			var updatedEntity = await _repository.UpdateAsync( id , entity ) ;
			var result       = await _unitOfWork.SaveAsync() ;

			if ( updatedEntity is null || !result ) return new Result< T >( new ArgumentException() ) ;

			return new Result< T >( updatedEntity ) ;
		}
		catch ( Exception ex ) { return new Result< T >( ex ) ; }
	}

	public async Task< Result< T > > DeleteAsync(int id)
	{
		try
		{
			var deletedEntity = await _repository.DeleteAsync( id ) ;
			var result       = await _unitOfWork.SaveAsync() ;
			if ( deletedEntity is null || !result ) return new Result< T >( new ArgumentException() ) ;

			return new Result< T >( deletedEntity ) ;
		}
		catch ( Exception ex ) { return new Result< T >( ex ) ; }
	}
}
