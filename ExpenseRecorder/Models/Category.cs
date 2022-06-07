using ExpenseRecorder.Models.Interfaces ;

namespace ExpenseRecorder.Models ;

public class Category : IEntity
{
	public int Id { get ; set ; }
	public string Name { get ; set ; }
	public int UserId { get ; set ; }
	public User User { get ; set ; }
}
