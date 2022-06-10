using ExpenseRecorder.Repositories.Interfaces ;
using ExpenseRecorder.Services.Interfaces ;
using ExpenseRecorder.UnitOfWork.Interfaces ;

namespace ExpenseRecorder.Services ;

public class PaymentAccountService : BaseService< PaymentAccount > , IPaymentAccountService
{
	public PaymentAccountService(IPaymentAccountRepository repository , IUnitOfWork unitOfWork)
		: base( repository , unitOfWork )
	{ }
}
