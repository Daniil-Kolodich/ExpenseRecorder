using ExpenseRecorder.Models.Interfaces ;

namespace ExpenseRecorder.Repositories.Interfaces ;

public interface IBaseRepository < T >
	where T : class , IEntity
{
	Task< IEnumerable< T > > GetAllAsync() ;
	Task< T? >               GetAsync(int    id) ;
	Task< T? >               AddAsync(T      entity) ;
	Task< T? >               UpdateAsync(int id , T entity) ;
	Task< T? >               DeleteAsync(int id) ;
	
//	Task<T?> FindAsync(int id);
}
