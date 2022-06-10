namespace ExpenseRecorder.DTO.Requests.Transaction ;

public class TransactionCreateUpdateRequest
{
	public string? Description { get ; set ; }

	public decimal  Amount           { get ; set ; }
	public int      CategoryId       { get ; set ; }
	public int      PaymentAccountId { get ; set ; }
	public DateTime Date             { get ; set ; }
	public string   Type             { get ; set ; }
}
