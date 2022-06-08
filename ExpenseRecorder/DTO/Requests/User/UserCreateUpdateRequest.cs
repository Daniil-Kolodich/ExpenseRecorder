namespace ExpenseRecorder.DTO.Requests.User ;

public class UserCreateUpdateRequest
{
	public string UserName { get ; set ; } = string.Empty ;
	public string Email    { get ; set ; } = string.Empty ;
	public string Password { get ; set ; } = string.Empty ;
}
