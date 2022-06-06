using System.IdentityModel.Tokens.Jwt ;
using System.Security.Claims ;
using System.Text ;
using ExpenseRecorder.Models ;
using ExpenseRecorder.Repositories.Interfaces ;
using ExpenseRecorder.Services.Interfaces ;
using ExpenseRecorder.UnitOfWork.Interfaces ;
using LanguageExt.Common ;
using Microsoft.IdentityModel.Tokens ;

namespace ExpenseRecorder.Services ;

public class UserService : BaseService< User > , IUserService
{
	public static readonly string SecretKey = "db3OIsj+BXE9NZDy0t8W3TcNekrF+2d/1sFnWG4HnV8TZY30iTOdtVWJOMZ4zY" ;
	public static readonly string Issuer    = "https://localhost:7043" ;
	public static readonly string Audience = "https://localhost:7043" ;

	public UserService(IUserRepository repository , IUnitOfWork unitOfWork)
		: base( repository , unitOfWork )
	{ }

	public async Task< Result< string > > LoginAsync(User userForLogin)
	{
		var users = await _repository.GetAllAsync() ;
		var user  = users.SingleOrDefault( u => u.Name == userForLogin.Name && u.Password == userForLogin.Password ) ;

		if ( user is null )
			return new Result< string >( new ArgumentException() ) ;

		var token = GenerateToken( user ) ;

		return new Result< string >( token ) ;
	}
	// TODO : refactor this method
	private string GenerateToken(User user)
	{
		var claims = new[ ]
		{
			new Claim( ClaimTypes.NameIdentifier , user.Name ) , new Claim( ClaimTypes.Email , user.Email ) ,
		} ;

		var token = new JwtSecurityToken
		(
			issuer : "https://localhost:7043" ,
			audience : "https://localhost:7043" ,
			claims : claims ,
			expires : DateTime.UtcNow.AddMinutes( 30 ) ,
			notBefore : DateTime.UtcNow ,
			signingCredentials : new SigningCredentials(
				new SymmetricSecurityKey(
					Encoding.UTF8.GetBytes(
						"111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111" ) ) ,
				SecurityAlgorithms.HmacSha256 )
		) ;

		var tokenString = new JwtSecurityTokenHandler().WriteToken( token ) ;

		return tokenString ;
	}
}
