using AutoMapper ;
using ExpenseRecorder.DTO.Requests.Transaction ;
using ExpenseRecorder.DTO.Responses.Transaction ;
using ExpenseRecorder.Models ;

namespace ExpenseRecorder.Mapping ;

public class TransactionProfile : Profile
{
	public TransactionProfile()
	{
		CreateMap< TransactionCreateUpdateRequest , Transaction >()
		   .ForMember( t => t.Type , opt => opt.MapFrom( r => FromStringToEnum( r.Type ) ) ) ;

		CreateMap< Transaction , TransactionResponse >()
		   .ForMember( t => t.Type , opt => opt.MapFrom( r => r.Type.ToString() ) ) ;
	}

	public TransactionType FromStringToEnum(string value)
	{
		return (TransactionType) Enum.Parse( typeof(TransactionType) , value ) ;
	}
}
