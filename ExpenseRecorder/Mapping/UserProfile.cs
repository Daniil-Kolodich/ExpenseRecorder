using AutoMapper ;
using ExpenseRecorder.DTO.Requests.User ;
using ExpenseRecorder.DTO.Responses.User ;
using ExpenseRecorder.Models ;

namespace ExpenseRecorder.Mapping ;

public class UserProfile : Profile
{
	public UserProfile()
	{
		CreateMap< UserCreateUpdateRequest , User >() ;
		CreateMap< User , UserResponse >() ;

		CreateMap< UserLoginRequest , User >() ;
	}
}
