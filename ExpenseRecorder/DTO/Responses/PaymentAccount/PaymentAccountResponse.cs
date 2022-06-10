using ExpenseRecorder.DTO.Responses.Transaction ;

namespace ExpenseRecorder.DTO.Responses.PaymentAccount ;

public class PaymentAccountResponse
{
	public int    Id     { get ; set ; }
	public string Name { get ; set ; }
	public string  Currency { get ; set ; }
	public decimal Balance  { get ; set ; }
}
