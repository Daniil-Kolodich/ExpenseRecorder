namespace ExpenseRecorder.DTO.Requests.PaymentAccount ;

public class PaymentAccountCreateUpdateRequest
{
	public string Name { get ; set ; }
	public string  Currency { get ; set ; }
	public decimal Balance  { get ; set ; }
}
