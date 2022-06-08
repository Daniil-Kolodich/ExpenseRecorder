namespace ExpenseRecorder.DTO.Requests.User ;

public class UserCreateUpdateRequest
{
	public string UserName     { get ; set ; } = String.Empty ;
	public string Email    { get ; set ; } = String.Empty ;
	public string Password { get ; set ; } = String.Empty ;
}
