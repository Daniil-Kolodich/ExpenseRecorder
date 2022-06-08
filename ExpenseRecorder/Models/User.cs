using ExpenseRecorder.Models.Interfaces ;
using Microsoft.AspNetCore.Identity ;

namespace ExpenseRecorder.Models ;

public class User : IdentityUser
{
	public ICollection<Category> Categories { get ; set ; } = new List<Category>() ;
}
