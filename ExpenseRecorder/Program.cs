using System.Text ;
using System.Text.Json.Serialization ;
using ExpenseRecorder.Context ;
using ExpenseRecorder.Mapping ;
using ExpenseRecorder.Models ;
using ExpenseRecorder.Repositories ;
using ExpenseRecorder.Repositories.Interfaces ;
using ExpenseRecorder.Services ;
using ExpenseRecorder.Services.Interfaces ;
using ExpenseRecorder.UnitOfWork ;
using ExpenseRecorder.UnitOfWork.Interfaces ;
using Microsoft.AspNetCore.Authentication.JwtBearer ;
using Microsoft.AspNetCore.Identity ;
using Microsoft.EntityFrameworkCore ;
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
	options.TokenValidationParameters = new TokenValidationParameters
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

builder.Services.AddAutoMapper( typeof(UserProfile) , typeof(CategoryProfile) ) ;

builder.Services.AddAuthorization() ;

builder.Services.AddDbContext< ExpenseRecorderContext >( context => context
   .UseSqlServer( "Server=(localdb)\\mssqllocaldb;Database=expense_recorder_v1;Trusted_Connection=True;" ) ) ;

var identity        = builder.Services.AddIdentityCore< User >() ;
var identityBuilder = new IdentityBuilder( identity.UserType , identity.Services ) ;
identityBuilder.AddEntityFrameworkStores< ExpenseRecorderContext >() ;
identityBuilder.AddSignInManager< SignInManager< User > >() ;
identityBuilder.AddUserManager< UserManager< User > >() ;

builder.Services.AddScoped< ICategoryRepository , CategoryRepository >() ;
builder.Services.AddScoped< ICategoryService , CategoryService >() ;

builder.Services.AddScoped< IUserService , UserService >() ;

builder.Services.AddScoped< IAuthenticationService , AuthenticationService >() ;

builder.Services.AddScoped< IUnitOfWork , UnitOfWork >() ;


builder.Services.AddHttpContextAccessor() ;
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
