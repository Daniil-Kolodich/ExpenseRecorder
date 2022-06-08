namespace ExpenseRecorder.UnitOfWork.Interfaces ;

public interface IUnitOfWork
{
	Task< bool > SaveAsync() ;
}
