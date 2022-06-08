using System.Security.Claims ;
using AutoMapper ;
using ExpenseRecorder.DTO.Requests.Category ;
using ExpenseRecorder.DTO.Requests.User ;
using ExpenseRecorder.DTO.Responses.Category ;
using ExpenseRecorder.DTO.Responses.User ;
using ExpenseRecorder.Models ;
using ExpenseRecorder.Services.Interfaces ;
using Microsoft.AspNetCore.Authorization ;
using Microsoft.AspNetCore.Mvc ;

namespace ExpenseRecorder.Controllers ;

[ ApiController ]
[ Route( "[controller]" ) ]
public class CategoryController : ControllerBase
{
	private readonly ICategoryService _categoryService ;
	private readonly IMapper          _mapper ;

	public CategoryController(IMapper mapper , ICategoryService categoryService)
	{
		_mapper          = mapper ;
		_categoryService = categoryService ;
	}

	[ HttpGet ]
	[ Authorize ]
	public async Task< ActionResult< IEnumerable< CategoryResponse > > > GetAll()
	{
		var response = await _categoryService.GetAllAsync() ;

		return response.Match< ActionResult< IEnumerable< CategoryResponse > > >(
			success => Ok( _mapper.Map< IEnumerable< CategoryResponse > >( success ) ) ,
			failure => BadRequest( failure ) ) ;
	}

	[ HttpGet ]
	[ Route( "{id}" ) ]
	[ Authorize ]
	public async Task< ActionResult< CategoryResponse > > Get(int id)
	{
		var result = await _categoryService.GetAsync( id ) ;

		return result.Match< ActionResult< CategoryResponse > >(
			success => Ok( _mapper.Map< CategoryResponse >( success ) ) ,
			failure => BadRequest( failure ) ) ;
	}

	[ HttpPut ]
	[ Route( "{id}" ) ]
	[ Authorize ]
	public async Task< ActionResult< CategoryResponse > > Put(int id , [ FromBody ] CategoryCreateUpdateRequest request)
	{
		var category = _mapper.Map< Category >( request ) ;
		category.Id = id ;
		var result = await _categoryService.UpdateAsync( id , category ) ;

		return result.Match< ActionResult< CategoryResponse > >(
			success => Ok( _mapper.Map< CategoryResponse >( success ) ) ,
			failure => BadRequest( failure.ToString() ) ) ;
	}

	[ HttpDelete ]
	[ Route( "{id}" ) ]
	[ Authorize ]
	public async Task< ActionResult< CategoryResponse > > Delete(int id)
	{
		var result = await _categoryService.DeleteAsync( id ) ;

		return result.Match< ActionResult< CategoryResponse > >(
			success => Ok( _mapper.Map< CategoryResponse >( success ) ) ,
			failure => BadRequest( failure ) ) ;
	}

	[ HttpPost ]
	[ Authorize ]
	public async Task< ActionResult< CategoryResponse > > Post([ FromBody ] CategoryCreateUpdateRequest request)
	{
		var category = _mapper.Map< Category >( request ) ;
		var result   = await _categoryService.AddAsync( category ) ;

		return result.Match< ActionResult< CategoryResponse > >(
			success => Ok( _mapper.Map< CategoryResponse >( success ) ) ,
			failure => BadRequest( failure ) ) ;
	}
}
