using ExpenseRecorder.Models ;
using ExpenseRecorder.Services.Result ;
using Microsoft.AspNetCore.Identity ;

namespace ExpenseRecorder.Services.Interfaces ;

// TODO : will it be like base service ?
public interface IUserService
{
	Task< IdentityResult? >  CreateAsync(User user ,         string password) ;
	Task< Result< string > > LoginAsync(User  userForLogin , string password) ;
}
