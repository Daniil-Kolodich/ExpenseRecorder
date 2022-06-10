using ExpenseRecorder.Models.Interfaces ;

namespace ExpenseRecorder.Models ;

public class Transaction : IUserEntity< Transaction >
{
	public string? Description { get ; set ; }

	public decimal Amount { get ; set ; }
	public int      CategoryId { get ; set ; }
	public Category Category   { get ; set ; } = null! ;

	public DateTime        Date { get ; set ; }
	public TransactionType Type { get ; set ; }

	public string UserId { get ; set ; }
	public int    Id     { get ; set ; }

	public void CopyFrom(Transaction entity)
	{
		Description = entity.Description ;
		Amount      = entity.Amount ;
		CategoryId  = entity.CategoryId ;
		Date        = entity.Date ;
	}
}
