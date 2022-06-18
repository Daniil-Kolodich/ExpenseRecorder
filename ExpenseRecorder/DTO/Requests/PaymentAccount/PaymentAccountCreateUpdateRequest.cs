namespace ExpenseRecorder.DTO.Requests.PaymentAccount ;

public class PaymentAccountCreateUpdateRequest
{
	public string Name     { get ; set ; } = String.Empty;
	public string Currency { get ; set ; } = String.Empty ;
	public decimal Balance  { get ; set ; }
}
