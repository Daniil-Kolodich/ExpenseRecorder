using System.Linq.Expressions ;
using ExpenseRecorder.SearchHandlers.Options.Interfaces ;

namespace ExpenseRecorder.SearchHandlers.Options ;

public class PaymentAccountSearchOptions : IPaymentAccountSearchOptions
{
	public string  Name        { get ; set ; } = string.Empty ;
	public string  Currency    { get ; set ; } = string.Empty ;
	public decimal BalanceFrom { get ; set ; }
	public decimal BalanceTo   { get ; set ; }

	private IList< Expression< Func< PaymentAccount , bool > > > _searchOptions =
		new List< Expression< Func< PaymentAccount , bool > > >() ;

	public IList< Expression< Func< PaymentAccount , bool > > > GetSearchOptions()
	{
		ByName() ;
		ByCurrency() ;
		ByBalance() ;

		return _searchOptions ;
	}

	private void ByName()
	{
		if ( !string.IsNullOrEmpty( Name ) )
		{
			_searchOptions.Add( x => x.Name.Contains( Name ) ) ;
		}
	}

	private void ByCurrency()
	{
		if ( !string.IsNullOrEmpty( Currency ) )
		{
			_searchOptions.Add( x => x.Currency.Contains( Currency ) ) ;
		}
	}

	private void ByBalance()
	{
		if ( BalanceTo > 0 ) { _searchOptions.Add( x => x.Balance <= BalanceTo ) ; }
		if ( BalanceFrom >= 0 ) { _searchOptions.Add( x => x.Balance >= BalanceFrom ) ; }
	}
}
