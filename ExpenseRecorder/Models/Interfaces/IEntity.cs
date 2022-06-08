namespace ExpenseRecorder.Models.Interfaces ;

public interface IEntity < T >
	where T : class
{
	int  Id { get ; set ; }
	void CopyFrom(T entity) ;
}
