using ExpenseRecorder.Exceptions ;
using ExpenseRecorder.Repositories.Interfaces ;
using ExpenseRecorder.SearchHandlers.Queries.Interfaces ;
using ExpenseRecorder.Services.Interfaces ;
using ExpenseRecorder.UnitOfWork.Interfaces ;
using LanguageExt.Common ;

namespace ExpenseRecorder.Services ;

public class PaymentAccountService : BaseService< PaymentAccount > , IPaymentAccountService
{
	public PaymentAccountService(
		IPaymentAccountRepository  repository ,
		IUnitOfWork                unitOfWork ,
		IPaymentAccountSearchQuery searchQuery)
		: base( repository , unitOfWork , searchQuery )
	{ }
}
