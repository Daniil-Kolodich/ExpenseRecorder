using ExpenseRecorder.Models ;
using LanguageExt.Common ;

namespace ExpenseRecorder.Services.Interfaces ;

public interface IUserService : IBaseService< User >
{
	Task< Result< string > > LoginAsync( string username , string password ) ;
}
