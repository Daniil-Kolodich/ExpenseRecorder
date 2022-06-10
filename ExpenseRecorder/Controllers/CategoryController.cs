using AutoMapper ;
using ExpenseRecorder.DTO.Requests.Category ;
using ExpenseRecorder.DTO.Responses.Category ;
using ExpenseRecorder.Exceptions ;
using ExpenseRecorder.Models ;
using ExpenseRecorder.Services.Interfaces ;
using Microsoft.AspNetCore.Authorization ;
using Microsoft.AspNetCore.Mvc ;

namespace ExpenseRecorder.Controllers ;

[ ApiController ]
[ Route( "[controller]" ) ]
[ Authorize ]
public class CategoryController : ControllerBase
{
	private readonly ICategoryService _categoryService ;
	private readonly IMapper          _mapper ;

	public CategoryController(IMapper mapper , ICategoryService categoryService)
	{
		_mapper          = mapper ;
		_categoryService = categoryService ;
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
	public async Task< ActionResult< IEnumerable< CategoryResponse > > > GetAll()
	{
		var response = await _categoryService.GetAllAsync() ;

		return response.Match< ActionResult< IEnumerable< CategoryResponse > > >(
			success => Ok( _mapper.Map< IEnumerable< CategoryResponse > >( success ) ) ,
			failure => MapExceptionsToActionResults( failure ) ) ;
	}

	[ HttpGet ]
	[ Route( "{id}" ) ]
	public async Task< ActionResult< CategoryResponse > > Get(int id)
	{
		var result = await _categoryService.GetAsync( id ) ;

		return result.Match< ActionResult< CategoryResponse > >(
			success => Ok( _mapper.Map< CategoryResponse >( success ) ) ,
			failure => MapExceptionsToActionResults( failure ) ) ;
	}

	[ HttpPut ]
	[ Route( "{id}" ) ]
	public async Task< ActionResult< CategoryResponse > > Put(int id , [ FromBody ] CategoryCreateUpdateRequest request)
	{
		var category = _mapper.Map< Category >( request ) ;
		var result   = await _categoryService.UpdateAsync( id , category ) ;

		return result.Match< ActionResult< CategoryResponse > >(
			success => Ok( _mapper.Map< CategoryResponse >( success ) ) ,
			failure => MapExceptionsToActionResults( failure ) ) ;
	}

	[ HttpDelete ]
	[ Route( "{id}" ) ]
	public async Task< ActionResult< CategoryResponse > > Delete(int id)
	{
		var result = await _categoryService.DeleteAsync( id ) ;

		return result.Match< ActionResult< CategoryResponse > >(
			success => Ok( _mapper.Map< CategoryResponse >( success ) ) ,
			failure => MapExceptionsToActionResults( failure ) ) ;
	}

	[ HttpPost ]
	public async Task< ActionResult< CategoryResponse > > Post([ FromBody ] CategoryCreateUpdateRequest request)
	{
		var category = _mapper.Map< Category >( request ) ;
		var result   = await _categoryService.AddAsync( category ) ;

		return result.Match< ActionResult< CategoryResponse > >(
			success => Ok( _mapper.Map< CategoryResponse >( success ) ) ,
			failure => MapExceptionsToActionResults( failure ) ) ;
	}
}
