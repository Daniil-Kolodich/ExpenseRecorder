using System.IdentityModel.Tokens.Jwt ;
using System.Security.Claims ;
using System.Text ;
using ExpenseRecorder.Models ;
using Microsoft.AspNetCore.Mvc ;
using Microsoft.IdentityModel.Tokens ;

namespace ExpenseRecorder.Controllers ;

[ ApiController ]
[ Route( "[controller]" ) ]
public class UserController : ControllerBase
{
	public static List< User > Users = new List< User >()
	{
		new User() { Id = 1 , Name = "User1" , Password = "1111" , Email = "test@mail.ru" } ,
		new User() { Id = 2 , Name = "User2" , Password = "2222" , Email = "test2@mail.ru" }
	} ;

	[ HttpGet ]
	public async Task< ActionResult< string > > Get(string userName , string password)
	{
		var user = Users.FirstOrDefault( u => u.Name == userName && u.Password == password ) ;

		if ( user == null ) { return NotFound() ; }

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

		return Ok( tokenString ) ;
	}
}
