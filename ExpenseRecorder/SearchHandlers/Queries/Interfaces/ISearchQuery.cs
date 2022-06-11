using ExpenseRecorder.Models.Interfaces ;
using ExpenseRecorder.SearchHandlers.Options.Interfaces ;

namespace ExpenseRecorder.SearchHandlers.Queries.Interfaces ;

public interface ISearchQuery < T >
	where T : class , IUserEntity< T >
{
	ISearchQuery< T > SetBaseQuery(IQueryable< T >             baseQuery) ;
	IQueryable< T >   Build(ISearchOptions<T> 			  options) ;
}
