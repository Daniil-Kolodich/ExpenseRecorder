using ExpenseRecorder.Models.Interfaces ;

namespace ExpenseRecorder.Models ;

public class Category : IEntity< Category >
{
	public string Name                      { get ; set ; } = string.Empty ;
	public string UserId                    { get ; set ; } = string.Empty ;
	public int    Id                        { get ; set ; }
	public string Color					 { get ; set ; } = "#FFFFFF" ;
	public string Icon					 { get ; set ; } = string.Empty ;
	public ICollection<Transaction> Transactions { get ; set ; } = new List<Transaction>() ;

	public void   CopyFrom(Category entity) { Name = entity.Name ; }
}
