namespace ExpenseRecorder.Models.Interfaces ;
// TODO : what if i add userId setting right here , it used across all the models
public interface IUserEntity < T >
	where T : class
{
	int  Id { get ; set ; }
	public string UserId { get ; set ; }
	void CopyFrom(T entity) ;
	
}

