using AutoMapper ;
using ExpenseRecorder.DTO.Requests.PaymentAccount ;
using ExpenseRecorder.DTO.Responses.PaymentAccount ;

namespace ExpenseRecorder.Mapping ;

public class PaymentAccountProfile : Profile
{
	public PaymentAccountProfile()
	{
		CreateMap< PaymentAccountCreateUpdateRequest , PaymentAccount >() ;
		CreateMap< PaymentAccount , PaymentAccountResponse >() ;
	}
}
