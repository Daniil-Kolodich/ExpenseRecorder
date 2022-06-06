using ExpenseRecorder.Context ;
using ExpenseRecorder.Models ;
using ExpenseRecorder.Repositories.Interfaces ;
using Microsoft.EntityFrameworkCore ;

namespace ExpenseRecorder.Repositories ;

public class UserRepository : BaseRepository<User>, IUserRepository
{
	public UserRepository(ExpenseRecorderContext context) : base(context)
	{
	}
}
