using ExpenseRecorder.DTO.Responses.Category ;
using ExpenseRecorder.DTO.Responses.PaymentAccount ;

namespace ExpenseRecorder.DTO.Responses.Transaction ;

public class TransactionResponse
{
	public int              Id          { get ; set ; }
	public string?          Description { get ; set ; }
	public decimal          Amount      { get ; set ; }
	public int              CategoryId  { get ; set ; }
	public CategoryResponse Category    { get ; set ; } = null! ;
	public DateTime         Date        { get ; set ; }
	public string           Type        { get ; set ; } = String.Empty ;
	public int PaymentAccountId { get; set; }
	public PaymentAccountResponse PaymentAccount { get; set; } = null!;
}
