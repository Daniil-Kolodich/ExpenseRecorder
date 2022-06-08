using AutoMapper ;
using ExpenseRecorder.DTO.Requests.User ;
using ExpenseRecorder.DTO.Responses.User ;
using ExpenseRecorder.Exceptions ;
using ExpenseRecorder.Models ;
using ExpenseRecorder.Services.Interfaces ;
using Microsoft.AspNetCore.Mvc ;

namespace ExpenseRecorder.Controllers ;

[ ApiController ]
[ Route( "[controller]" ) ]
public class UserController : ControllerBase
{
	private readonly IMapper      _mapper ;
	private readonly IUserService _userService ;

	public UserController(
		IMapper      mapper ,
		IUserService userService)
	{
		_mapper      = mapper ;
		_userService = userService ;
	}

/*
	[ HttpGet ]
	public async Task< ActionResult< IEnumerable< User > > > GetAll()
	{
		var response = await _userService.GetAllAsync() ;
		return response.Match< ActionResult< IEnumerable< User > > >(
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
*/
	[ HttpPost ]
	[ Route( "register" ) ]
	public async Task< ActionResult< UserResponse > > Post([ FromBody ] UserCreateUpdateRequest user)
	{
		var userToCreate = _mapper.Map< User >( user ) ;
		var result       = await _userService.CreateAsync( userToCreate , user.Password ) ;

		if ( !result!.Succeeded ) return BadRequest( result.Errors ) ;

		return Ok( _mapper.Map< UserResponse >( userToCreate ) ) ;
	}


	[ HttpPost ]
	[ Route( "login" ) ]
	public async Task< ActionResult< UserLoginResponse > > Login([ FromBody ] UserLoginRequest request)
	{
		var user   = _mapper.Map< User >( request ) ;
		var result = await _userService.LoginAsync( user , request.Password ) ;

		return result.Match< ActionResult< UserLoginResponse > >(
			success => Ok( new UserLoginResponse { Token = success , UserName = user.UserName } ) ,
			failure => failure switch
			{
				NotFoundException nf   => NotFound( nf ) ,
				BadRequestException br => BadRequest( br ) ,
				_                       => StatusCode( 500 , failure )
			} ) ;
	}
}
