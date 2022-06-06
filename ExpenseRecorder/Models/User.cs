using ExpenseRecorder.Models.Interfaces ;

namespace ExpenseRecorder.Models ;

public class User : IEntity
{
	public int    Id   { get;  set; }
	public string Name { get ; set ; } = String.Empty ;
	public string Email { get ; set ; } = String.Empty ;
	public string Password { get ; set ; } = String.Empty ;
}
