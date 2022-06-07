using System.Security.Claims ;
using ExpenseRecorder.Models ;
using ExpenseRecorder.Services.Interfaces ;

namespace ExpenseRecorder.Services ;

public class AuthenticationService : IAuthenticationService
{
	private readonly IHttpContextAccessor _httpContextAccessor ;

	public AuthenticationService(IHttpContextAccessor httpContextAccessor)
	{
		_httpContextAccessor = httpContextAccessor ;
	}

	public User? CurrentUser => CreateUserFromClaims( _httpContextAccessor.HttpContext! ) ;

	private User? CreateUserFromClaims(HttpContext httpContext)
	{
		var claims      = httpContext.User.Claims.ToList() ;
		var idClaims    = claims.FirstOrDefault( c => c.Type == ClaimTypes.NameIdentifier ) ;
		var nameClaims  = claims.FirstOrDefault( c => c.Type == ClaimTypes.Name ) ;
		var emailClaims = claims.FirstOrDefault( c => c.Type == ClaimTypes.Email ) ;

		if ( idClaims is null || nameClaims is null || emailClaims is null )
			return null ;

		return new User { Id = int.Parse( idClaims.Value ) , Name = nameClaims.Value , Email = emailClaims.Value } ;
	}
}
