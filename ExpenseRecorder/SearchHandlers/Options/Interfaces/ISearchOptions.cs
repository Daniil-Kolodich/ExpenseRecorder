using System.Linq.Expressions ;
using ExpenseRecorder.Models.Interfaces ;

namespace ExpenseRecorder.SearchHandlers.Options.Interfaces ;

public interface ISearchOptions < T >
	where T : class , IUserEntity< T >
{
	IList< Expression< Func< T , bool > > > GetSearchOptions() ;
}
