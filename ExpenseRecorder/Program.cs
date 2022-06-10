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

builder.Services.AddControllers()
	   .AddJsonOptions( j => j.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles ) ;

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
		ValidateIssuerSigningKey = true ,
		ValidIssuer              = UserService.Issuer ,
		ValidAudience            = UserService.Audience ,
		IssuerSigningKey         = new SymmetricSecurityKey( Encoding.UTF8.GetBytes( UserService.SecretKey ) )
	} ;
} ) ;

builder.Services.AddAutoMapper( typeof(UserProfile) , typeof(CategoryProfile) , typeof(TransactionProfile) ,
	typeof(PaymentAccountProfile) ) ;

builder.Services.AddAuthorization() ;

builder.Services.AddDbContext< ExpenseRecorderContext >( context => context
   .UseSqlServer( builder.Configuration.GetConnectionString( "DefaultConnection" ) ) ) ;

var identity        = builder.Services.AddIdentityCore< User >() ;
var identityBuilder = new IdentityBuilder( identity.UserType , identity.Services ) ;
identityBuilder.AddEntityFrameworkStores< ExpenseRecorderContext >() ;
identityBuilder.AddSignInManager< SignInManager< User > >() ;
identityBuilder.AddUserManager< UserManager< User > >() ;

builder.Services.AddScoped< ICategoryRepository , CategoryRepository >() ;
builder.Services.AddScoped< ICategoryService , CategoryService >() ;

builder.Services.AddScoped< ITransactionRepository , TransactionRepository >() ;
builder.Services.AddScoped< ITransactionService , TransactionService >() ;

builder.Services.AddScoped< IPaymentAccountRepository , PaymentAccountRepository >() ;
builder.Services.AddScoped< IPaymentAccountService , PaymentAccountService >() ;

builder.Services.AddScoped< IUserService , UserService >() ;

builder.Services.AddScoped< IAuthenticationService , AuthenticationService >() ;

builder.Services.AddScoped< IUnitOfWork , UnitOfWork >() ;


builder.Services.AddHttpContextAccessor() ;
var app = builder.Build() ;

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
