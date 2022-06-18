namespace ExpenseRecorder.Models.Interfaces ;
public interface IUserEntity < T >
	where T : class
{
	int  Id { get ; set ; }
	public string UserId { get ; set ; }
	void CopyFrom(T entity) ;
	
}

