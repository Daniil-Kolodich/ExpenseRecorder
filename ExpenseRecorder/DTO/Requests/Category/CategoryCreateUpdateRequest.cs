namespace ExpenseRecorder.DTO.Requests.Category ;

public class CategoryCreateUpdateRequest
{
	public string Name  { get ; set ; } = string.Empty ;
	public string Color { get ; set ; } = string.Empty ;
	public string Icon  { get ; set ; } = string.Empty ;
}
