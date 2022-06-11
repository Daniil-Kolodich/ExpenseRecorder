using ExpenseRecorder.Models.Interfaces ;
using ExpenseRecorder.SearchHandlers.Options.Interfaces ;
using ExpenseRecorder.SearchHandlers.Queries.Interfaces ;

namespace ExpenseRecorder.SearchHandlers.Queries ;

public class SearchQuery < T > : ISearchQuery< T >
	where T : class , IUserEntity< T >
{
	private protected IQueryable< T >? _query = null! ;

	public ISearchQuery< T > SetBaseQuery(IQueryable< T > baseQuery)
	{
		_query = baseQuery ;

		return this ;
	}

	public virtual IQueryable< T > Build(ISearchOptions< T > options)
	{
		var filters = options.GetSearchOptions() ;
		if (!filters.Any())
		{
			return _query! ;
		}
		return filters.Aggregate(_query!, (current, filter) => current.Where(filter)) ;
	}
}
