using System.Text ;
using System.Text.Json.Serialization ;
using Microsoft.AspNetCore.Authentication.JwtBearer ;
using Microsoft.IdentityModel.Tokens ;
using Microsoft.OpenApi.Models ;

var builder = WebApplication.CreateBuilder( args ) ;

// Add services to the container.

builder.Services.AddControllers()
	   .AddJsonOptions( j => j.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles ) ;

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer() ;

builder.Services.AddSwaggerGen( options =>
{
	options.AddSecurityDefinition( "Bearer" , new OpenApiSecurityScheme
	{
		Scheme       = "Bearer" ,
		BearerFormat = "JWT" ,
		In           = ParameterLocation.Header ,
		Name         = "Authorization" ,
		Description =
			"Bearer Authentication with JWT Token" ,
		Type = SecuritySchemeType.Http
	} ) ;

	options.AddSecurityRequirement( new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference { Id = "Bearer" , Type = ReferenceType.SecurityScheme }
			} ,
			new List< string >()
		}
	} ) ;
} ) ;

builder.Services.AddAuthentication( JwtBearerDefaults.AuthenticationScheme ).AddJwtBearer( options =>
{
	options.TokenValidationParameters = new TokenValidationParameters()
	{
		ValidateActor            = false ,
		ValidateAudience         = false ,
		ValidateIssuer           = false ,
		ValidateLifetime         = false ,
		ValidateIssuerSigningKey = false ,
		ValidIssuer              = "https://localhost:7043" ,
		ValidAudience            = "https://localhost:7043" ,
		IssuerSigningKey = new SymmetricSecurityKey( Encoding.UTF8.GetBytes(
			"111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111" ) )
	} ;
} ) ;

// add authorization
builder.Services.AddAuthorization() ;
var app = builder.Build() ;

// Configure the HTTP request pipeline.
if ( app.Environment.IsDevelopment() )
{
	app.UseSwagger() ;
	app.UseSwaggerUI() ;
}

app.UseHttpsRedirection() ;

app.UseAuthentication() ;
app.UseAuthorization() ;

app.MapControllers() ;

app.Run() ;
