using ExpenseRecorder.Models.Interfaces ;
using LanguageExt.Common ;

namespace ExpenseRecorder.Services.Interfaces ;

public interface IBaseService < T >
	where T : class , IEntity
{
	Task< Result< IEnumerable< T > > > GetAllAsync() ;
	Task< Result< T > >                GetAsync(int    id) ;
	Task< Result< T > >                AddAsync(T      entity) ;
	Task< Result< T > >                UpdateAsync(int id , T entity) ;
	Task< Result< T > >                DeleteAsync(int id) ;
}
