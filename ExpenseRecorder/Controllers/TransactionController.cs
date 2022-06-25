using AutoMapper ;
using ExpenseRecorder.DTO.Requests.Transaction ;
using ExpenseRecorder.DTO.Responses.Transaction ;
using ExpenseRecorder.Exceptions ;
using ExpenseRecorder.Models ;
using ExpenseRecorder.Services.Interfaces ;
using Microsoft.AspNetCore.Authorization ;
using Microsoft.AspNetCore.Mvc ;

namespace ExpenseRecorder.Controllers ;

[ ApiController ]
[ Route( "[controller]" ) ]
[ Authorize ]
public class TransactionController : ControllerBase
{
	private readonly IMapper             _mapper ;
	private readonly ITransactionService _transactionService ;

	public TransactionController(IMapper mapper , ITransactionService transactionService)
	{
		_mapper             = mapper ;
		_transactionService = transactionService ;
	}

	private ActionResult MapExceptionsToActionResults(Exception ex)
	{
		return ex switch
		{
			NotFoundException nf          => NotFound( nf.Message ) ,
			NotAuthenticatedException na  => Unauthorized( na.Message ) ,
			PredicateMismatchException pm => Unauthorized( pm.Message ) ,
			BadRequestException br        => BadRequest( br.Message ) ,
			SaveContextException sc       => StatusCode( 500 , sc.Message ) ,
			_                             => StatusCode( 500 , ex.Message )
		} ;
	}

	[ HttpGet ]
	[ Route( "GetAll" ) ]
	[ Authorize ]
	public async Task< ActionResult< IEnumerable< TransactionResponse > > > GetAll()
	{
		var response = await _transactionService.GetAllAsync() ;

		return response.Match< ActionResult< IEnumerable< TransactionResponse > > >(
			success => Ok( _mapper.Map< IEnumerable< TransactionResponse > >( success ) ) ,
			failure => MapExceptionsToActionResults( failure ) ) ;
	}

	[ HttpGet ]
	[ Route( "{id}" ) ]
	[ Authorize ]
	public async Task< ActionResult< TransactionResponse > > Get(int id)
	{
		var result = await _transactionService.GetAsync( id ) ;

		return result.Match< ActionResult< TransactionResponse > >(
			success => Ok( _mapper.Map< TransactionResponse >( success ) ) ,
			failure => MapExceptionsToActionResults( failure ) ) ;
	}

	[ HttpPut ]
	[ Route( "{id}" ) ]
	[ Authorize ]
	public async Task< ActionResult< TransactionResponse > > Put(
		int                                         id ,
		[ FromBody ] TransactionCreateUpdateRequest request)
	{
		var category = _mapper.Map< Transaction >( request ) ;
		var result   = await _transactionService.UpdateAsync( id , category ) ;

		return result.Match< ActionResult< TransactionResponse > >(
			success => Ok( _mapper.Map< TransactionResponse >( success ) ) ,
			failure => MapExceptionsToActionResults( failure ) ) ;
	}

	[ HttpDelete ]
	[ Route( "{id}" ) ]
	[ Authorize ]
	public async Task< ActionResult< TransactionResponse > > Delete(int id)
	{
		var result = await _transactionService.DeleteAsync( id ) ;

		return result.Match< ActionResult< TransactionResponse > >(
			success => Ok( _mapper.Map< TransactionResponse >( success ) ) ,
			failure => MapExceptionsToActionResults( failure ) ) ;
	}

	[ HttpPost ]
	[ Authorize ]
	public async Task< ActionResult< TransactionResponse > > Post([ FromBody ] TransactionCreateUpdateRequest request)
	{
		var category = _mapper.Map< Transaction >( request ) ;
		var result   = await _transactionService.AddAsync( category ) ;

		return result.Match< ActionResult< TransactionResponse > >(
			success => Ok( _mapper.Map< TransactionResponse >( success ) ) ,
			failure => MapExceptionsToActionResults( failure ) ) ;
	}
}
