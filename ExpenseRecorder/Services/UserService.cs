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
	public UserService(IUserRepository repository , IUnitOfWork unitOfWork)
		: base( repository , unitOfWork )
	{ }

	public async Task< Result< string > > LoginAsync(string username , string password)
	{
		var users = await _repository.GetAllAsync() ;
		var user  = users.SingleOrDefault( u => u.Name == username && u.Password == password ) ;

		if ( user is null )
			return new Result< string >( new ArgumentException() ) ;

		var token = GenerateToken( user ) ;

		return new Result< string >( token ) ;
	}

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
