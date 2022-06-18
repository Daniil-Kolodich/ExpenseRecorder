using ExpenseRecorder.DTO.Responses.Transaction ;

namespace ExpenseRecorder.DTO.Responses.PaymentAccount ;

public class PaymentAccountResponse
{
	public int     Id       { get ; set ; }
	public string  Name     { get ; set ; } = String.Empty ;
	public string  Currency { get ; set ; } = String.Empty ;
	public decimal Balance  { get ; set ; }

	public ICollection< TransactionResponse > Transactions { get ; set ; } = new List< TransactionResponse >() ;
}
