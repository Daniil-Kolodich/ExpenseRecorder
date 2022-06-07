using AutoMapper ;
using ExpenseRecorder.DTO.Requests.Category ;
using ExpenseRecorder.DTO.Responses.Category ;
using ExpenseRecorder.Models ;

namespace ExpenseRecorder.Mapping ;

public class CategoryProfile : Profile
{
	public CategoryProfile()
	{
		CreateMap< CategoryCreateUpdateRequest , Category >() ;
		CreateMap< Category , CategoryResponse >() ;
	}
}
