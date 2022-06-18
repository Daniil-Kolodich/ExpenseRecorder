using ExpenseRecorder.Models.Interfaces ;

namespace ExpenseRecorder.Models ;

public class Category : IUserEntity< Category >
{
	public string                     Name         { get ; set ; } = string.Empty ;
	public string                     UserId       { get ; set ; } = string.Empty ;
	public string                     Color        { get ; set ; } = "#FFFFFF" ;
	public string                     Icon         { get ; set ; } = string.Empty ;
	public ICollection< Transaction > Transactions { get ; set ; } = new List< Transaction >() ;

	public int Id { get ; set ; }

	public void CopyFrom(Category entity)
	{
		Name  = entity.Name ;
		Color = entity.Color ;
		Icon  = entity.Icon ;
	}
}
