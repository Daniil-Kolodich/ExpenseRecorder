using System.Security.Claims ;
using ExpenseRecorder.Models ;
using ExpenseRecorder.Services.Interfaces ;
using Microsoft.AspNetCore.Identity ;

namespace ExpenseRecorder.Services ;

public class AuthenticationService : IAuthenticationService
{
	private readonly IHttpContextAccessor _httpContextAccessor ;
	private readonly UserManager< User >  _userManager ; // TODO do i need this?

	public AuthenticationService(IHttpContextAccessor httpContextAccessor , UserManager< User > userManager)
	{
		_httpContextAccessor = httpContextAccessor ;
		_userManager         = userManager ;
	}
	// TODO : maybe inject as singleton and cache current user, and when something changes, update the cache
	public User? CurrentUser =>  CreateUserFromClaims( _httpContextAccessor.HttpContext! ) ;

	private User? CreateUserFromClaims(HttpContext httpContext)
	{
		var claims = httpContext.User.Claims ;
		var userId = claims.FirstOrDefault( c => c.Type == ClaimTypes.NameIdentifier )?.Value ;
		// TODO: i have claims for each property, actually i could recreate the user from the claims, but i'm lazy
		var user   = _userManager.FindByIdAsync( userId ).Result ;

		return user ;
	}
}
