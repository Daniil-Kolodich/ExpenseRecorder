using AutoMapper ;
using ExpenseRecorder.DTO.Requests.PaymentAccount ;
using ExpenseRecorder.DTO.Responses.PaymentAccount ;
using ExpenseRecorder.Exceptions ;
using ExpenseRecorder.Models ;
using ExpenseRecorder.SearchHandlers.Options ;
using ExpenseRecorder.Services.Interfaces ;
using Microsoft.AspNetCore.Authorization ;
using Microsoft.AspNetCore.Mvc ;

namespace ExpenseRecorder.Controllers ;

[ ApiController ]
[ Route( "[controller]" ) ]
[ Authorize ]
public class PaymentAccountController : ControllerBase
{
	private readonly IPaymentAccountService _paymentAccountService ;
	private readonly IMapper          _mapper ;

	public PaymentAccountController(IMapper mapper , IPaymentAccountService paymentAccountService)
	{
		_mapper          = mapper ;
		_paymentAccountService = paymentAccountService ;
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
	public async Task< ActionResult< IEnumerable< PaymentAccountResponse > > > GetAll([FromQuery] PaymentAccountSearchOptions options)
	{
		var response = await _paymentAccountService.GetAllAsync(options) ;

		return response.Match< ActionResult< IEnumerable< PaymentAccountResponse > > >(
			success => Ok( _mapper.Map< IEnumerable< PaymentAccountResponse > >( success ) ) ,
			failure => MapExceptionsToActionResults( failure ) ) ;
	}

	[ HttpGet ]
	[ Route( "{id}" ) ]
	public async Task< ActionResult< PaymentAccountResponse > > Get(int id)
	{
		var result = await _paymentAccountService.GetAsync( id ) ;
		return result.Match< ActionResult< PaymentAccountResponse > >(
			success => Ok( _mapper.Map< PaymentAccountResponse >( success ) ) ,
			failure => MapExceptionsToActionResults( failure ) ) ;
	}

	[ HttpPut ]
	[ Route( "{id}" ) ]
	public async Task< ActionResult< PaymentAccountResponse > > Put(int id , [ FromBody ] PaymentAccountCreateUpdateRequest request)
	{
		var paymentAccount = _mapper.Map< PaymentAccount >( request ) ;
		var result   = await _paymentAccountService.UpdateAsync( id , paymentAccount ) ;

		return result.Match< ActionResult< PaymentAccountResponse > >(
			success => Ok( _mapper.Map< PaymentAccountResponse >( success ) ) ,
			failure => MapExceptionsToActionResults( failure ) ) ;
	}

	[ HttpDelete ]
	[ Route( "{id}" ) ]
	public async Task< ActionResult< PaymentAccountResponse > > Delete(int id)
	{
		var result = await _paymentAccountService.DeleteAsync( id ) ;

		return result.Match< ActionResult< PaymentAccountResponse > >(
			success => Ok( _mapper.Map< PaymentAccountResponse >( success ) ) ,
			failure => MapExceptionsToActionResults( failure ) ) ;
	}

	[ HttpPost ]
	public async Task< ActionResult< PaymentAccountResponse > > Post([ FromBody ] PaymentAccountCreateUpdateRequest request)
	{
		var paymentAccount = _mapper.Map< PaymentAccount >( request ) ;
		var result   = await _paymentAccountService.AddAsync( paymentAccount ) ;

		return result.Match< ActionResult< PaymentAccountResponse > >(
			success => Ok( _mapper.Map< PaymentAccountResponse >( success ) ) ,
			failure => MapExceptionsToActionResults( failure ) ) ;
	}
}
