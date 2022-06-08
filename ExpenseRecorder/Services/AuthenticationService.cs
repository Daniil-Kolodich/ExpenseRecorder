using System.IdentityModel.Tokens.Jwt ;
using System.Security.Claims ;
using System.Text ;
using ExpenseRecorder.Models ;
using ExpenseRecorder.Services.Interfaces ;
using Microsoft.AspNetCore.Identity ;
using Microsoft.IdentityModel.Tokens ;

namespace ExpenseRecorder.Services ;

public class AuthenticationService : IAuthenticationService
{
	private readonly IHttpContextAccessor _httpContextAccessor ;
	private readonly UserManager< User >  _userManager ;

	public AuthenticationService(IHttpContextAccessor httpContextAccessor , UserManager< User > userManager)
	{
		_httpContextAccessor = httpContextAccessor ;
		_userManager         = userManager ;
	}

	public User? CurrentUser => CreateUserFromClaims( _httpContextAccessor.HttpContext! ) ;

	private User? CreateUserFromClaims(HttpContext httpContext)
	{
		var claims = httpContext.User.Claims ;
		var userId = claims.FirstOrDefault( c => c.Type == ClaimTypes.NameIdentifier )?.Value ;
		var user   = _userManager.FindByIdAsync( userId ).Result ;

		return user ;
	}
}
