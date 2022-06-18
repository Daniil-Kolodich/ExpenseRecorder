using ExpenseRecorder.Models.Interfaces ;

namespace ExpenseRecorder.Models ;

public class Transaction : IUserEntity< Transaction >
{
	public string UserId { get ; set ; } = String.Empty ;
	public int    Id     { get ; set ; }


	public string?         Description { get ; set ; }
	public decimal         Amount      { get ; set ; }
	public DateTime        Date        { get ; set ; }
	public TransactionType Type        { get ; set ; }


	public int      CategoryId { get ; set ; }
	public Category Category   { get ; set ; } = null! ;


	public int            PaymentAccountId { get ; set ; }
	public PaymentAccount PaymentAccount   { get ; set ; } = null! ;

	public void CopyFrom(Transaction entity)
	{
		Description      = entity.Description ;
		Amount           = entity.Amount ;
		CategoryId       = entity.CategoryId ;
		Type			 = entity.Type ;
		Date             = entity.Date ;
		PaymentAccountId = entity.PaymentAccountId ;
	}
}
