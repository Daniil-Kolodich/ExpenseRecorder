using ExpenseRecorder.Models.Interfaces ;
using ExpenseRecorder.SearchHandlers.Options.Interfaces ;
using ExpenseRecorder.Services.Result ;

namespace ExpenseRecorder.Services.Interfaces ;

public interface IBaseService < T >
	where T : class , IUserEntity< T >
{
	// TODO: Add predicates string responses when is it not walid as static params cause why not
	Task< Result< IEnumerable< T > > > GetAllAsync(ISearchOptions<T>? searchOptions = null) ;
	Task< Result< T > >                GetAsync(int    id) ;
	Task< Result< T > >                AddAsync(T      entity) ;
	Task< Result< T > >                UpdateAsync(int id , T entity) ;
	Task< Result< T > >                DeleteAsync(int id) ;
}
