using ExpenseRecorder.Models ;
using ExpenseRecorder.Models.Interfaces ;

public class PaymentAccount : IUserEntity< PaymentAccount >
{
	public int    Id     { get ; set ; }
	public string UserId { get ; set ; }


	public string  Name     { get ; set ; }
	// TODO: may be currency should be a separate entity stored in the database
	public string  Currency { get ; set ; }
	public decimal Balance  { get ; set ; }


	public ICollection< Transaction > Transactions { get ; set ; } = null! ;

	public void CopyFrom(PaymentAccount entity)
	{
		Name     = entity.Name ;
		Currency = entity.Currency ;
		Balance  = entity.Balance ;
	}
}
