using System.IdentityModel.Tokens.Jwt ;
using System.Security.Claims ;
using System.Text ;
using ExpenseRecorder.Exceptions ;
using ExpenseRecorder.Models ;
using ExpenseRecorder.Services.Interfaces ;
using ExpenseRecorder.Services.Result ;
using Microsoft.AspNetCore.Identity ;
using Microsoft.IdentityModel.Tokens ;

namespace ExpenseRecorder.Services ;

public class UserService : IUserService
{
	public static readonly string SecretKey = "db3OIsj+BXE9NZDy0t8W3TcNekrF+2d/1sFnWG4HnV8TZY30iTOdtVWJOMZ4zY" ;
	public static readonly string Issuer = "https://localhost:7043" ;
	public static readonly string Audience = "https://localhost:7043" ;
	private readonly       SignInManager< User > _signInManager ;

	private readonly UserManager< User > _userManager ;

	public UserService(UserManager< User > userManager , SignInManager< User > signInManager)
	{
		_userManager   = userManager ;
		_signInManager = signInManager ;
	}

	public async Task< IdentityResult? > CreateAsync(User user , string password)
	{
		return await _userManager.CreateAsync( user , password ) ;
	}


	public async Task< Result< string > > LoginAsync(User userForLogin , string password)
	{
		var user = await _userManager.FindByNameAsync( userForLogin.UserName ) ;

		if ( user == null ) return new Result< string >( new NotFoundException( "User not found" ) ) ;

		var result = await _signInManager.CheckPasswordSignInAsync( user , password , false ) ;

		if ( !result.Succeeded ) return new Result< string >( new BadRequestException( "Wrong data passed" ) ) ;

		return new Result< string >( GenerateToken( user ) ) ;
	}

	// TODO : refactor this method
	private string GenerateToken(User user)
	{
		var claims = new[ ]
		{
			new Claim( ClaimTypes.NameIdentifier , user.Id ) , new Claim( ClaimTypes.Name , user.UserName ) ,
			new Claim( ClaimTypes.Email ,          user.Email )
		} ;

		var token = new JwtSecurityToken
		(
			Issuer ,
			Audience ,
			claims ,
			expires : DateTime.UtcNow.AddMinutes( 30 ) ,
			notBefore : DateTime.UtcNow ,
			signingCredentials : new SigningCredentials(
				new SymmetricSecurityKey( Encoding.UTF8.GetBytes( SecretKey ) ) , SecurityAlgorithms.HmacSha256 )
		) ;

		var tokenString = new JwtSecurityTokenHandler().WriteToken( token ) ;

		return tokenString ;
	}
}
