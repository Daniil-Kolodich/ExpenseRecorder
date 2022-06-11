namespace ExpenseRecorder.SearchHandlers.Options.Interfaces ;

public interface IPaymentAccountSearchOptions : ISearchOptions< PaymentAccount >
{
	public string  Name        { get ; set ; }
	public string  Currency    { get ; set ; }
	public decimal BalanceFrom { get ; set ; }
	public decimal BalanceTo   { get ; set ; }
}
