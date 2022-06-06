using ExpenseRecorder.Models ;
using ExpenseRecorder.Services.Interfaces ;
using Microsoft.AspNetCore.Mvc ;

namespace ExpenseRecorder.Controllers ;

[ ApiController ]
[ Route( "[controller]" ) ]
public class UserController : ControllerBase
{
	private readonly IUserService _userService ;

	public UserController(IUserService userService)
	{
		_userService = userService ;
	}

	[ HttpGet ]
	public async Task< ActionResult< IEnumerable< User > > > GetAll()
	{
		var response = await _userService.GetAllAsync() ;
		return response.Match< ActionResult< IEnumerable< User > > >(
			success => Ok( success ) ,
			failure => BadRequest( failure ) ) ;
	}

	[ HttpPost ]
	public async Task< ActionResult< User > > Post([ FromBody ] User user)
	{
		var result = await _userService.AddAsync( user ) ;

		return result.Match<ActionResult<User>>(
			success => Ok( success ) ,
			failure => BadRequest( failure ) ) ;
	}

	[ HttpGet ]
	[ Route( "{id}" ) ]
	public async Task< ActionResult< User > > Get(int id)
	{
		var result = await _userService.GetAsync( id ) ;

		return result.Match< ActionResult< User > >(
			success => Ok( success ) ,
			failure => BadRequest( failure ) ) ;
	}
	
	[ HttpPut ]
	[ Route( "{id}" ) ]
	public async Task< ActionResult< User > > Put(int id, [ FromBody ] User user)
	{
		var result = await _userService.UpdateAsync( id, user ) ;
		
		return result.Match< ActionResult< User > >(
			success => Ok( success ) ,
			failure => BadRequest( failure ) ) ;
	}
	
	[ HttpDelete ]
	[ Route( "{id}" ) ]
	public async Task< ActionResult< User > > Delete(int id)
	{
		var result = await _userService.DeleteAsync( id ) ;

		return result.Match< ActionResult< User > >(
			success => Ok( success ) ,
			failure => BadRequest( failure ) ) ;
	}

	[ HttpPost ]
	[ Route( "login" ) ]
	public async Task< ActionResult< string > > Login(string username, string password)
	{
		var result = await _userService.LoginAsync( username, password ) ;

		return result.Match< ActionResult< string > >(
			success => Ok( success ) ,
			failure => BadRequest( failure ) ) ;
	}
}
