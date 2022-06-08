using ExpenseRecorder.Models.Interfaces ;

namespace ExpenseRecorder.Repositories.Interfaces ;

public interface IBaseRepository < T >
	where T : class , IEntity< T >
{
	Task< IEnumerable< T > > GetAllAsync(bool tracking           = false) ;
	Task< T? >               GetAsync(int     id , bool tracking = true) ;
	Task< T? >               AddAsync(T       entity) ;
	Task< T? >               UpdateAsync(int  id , T entity) ;
	Task< T? >               DeleteAsync(int  id) ;
}
