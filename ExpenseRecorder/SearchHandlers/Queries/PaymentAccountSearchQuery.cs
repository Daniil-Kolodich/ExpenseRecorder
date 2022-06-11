using ExpenseRecorder.SearchHandlers.Options.Interfaces ;
using ExpenseRecorder.SearchHandlers.Queries.Interfaces ;

namespace ExpenseRecorder.SearchHandlers.Queries ;

public class PaymentAccountSearchQuery : SearchQuery< PaymentAccount >, IPaymentAccountSearchQuery
{ }
