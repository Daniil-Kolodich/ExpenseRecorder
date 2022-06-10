using ExpenseRecorder.Exceptions ;
using ExpenseRecorder.Models ;
using ExpenseRecorder.Repositories.Interfaces ;
using ExpenseRecorder.Services.Interfaces ;
using ExpenseRecorder.UnitOfWork.Interfaces ;
using LanguageExt.Common ;

namespace ExpenseRecorder.Services ;

public class TransactionService : BaseService< Transaction > , ITransactionService
{
	private readonly ICategoryRepository       _categoryRepository ;
	private readonly IPaymentAccountRepository _paymentAccountRepository ;

	public TransactionService(
		ITransactionRepository    repository ,
		ICategoryRepository       categoryRepository ,
		IUnitOfWork               unitOfWork ,
		IPaymentAccountRepository paymentAccountRepository)
		: base( repository , unitOfWork )
	{
		_categoryRepository       = categoryRepository ;
		_paymentAccountRepository = paymentAccountRepository ;
	}

	public override async Task< Result< IEnumerable< Transaction > > > GetAllAsync()
	{
		// TODO : add query to customize the result
		return await base.GetAllAsync() ;
	}

	public override async Task< Result< Transaction > > AddAsync(Transaction entity)
	{
		var category = await _categoryRepository.GetAsync( entity.CategoryId ) ;

		if ( category is null || category.UserId != _repository.UserId )
			return new Result< Transaction >( new NotFoundException( "Not found such category" ) ) ;

		var paymentAccount = await _paymentAccountRepository.GetAsync( entity.PaymentAccountId ) ;

		if ( paymentAccount is null || paymentAccount.UserId != _repository.UserId )
			return new Result< Transaction >( new NotFoundException( "Not found such payment account" ) ) ;

		return await base.AddAsync( entity ) ;
	}

	public override async Task< Result< Transaction > > UpdateAsync(int id , Transaction entity)
	{
		var category = await _categoryRepository.GetAsync( entity.CategoryId ) ;

		if ( category is null || category.UserId != _repository.UserId )
			return new Result< Transaction >( new NotFoundException( "Not found such category" ) ) ;


		var paymentAccount = await _paymentAccountRepository.GetAsync( entity.PaymentAccountId ) ;

		if ( paymentAccount is null || paymentAccount.UserId != _repository.UserId )
			return new Result< Transaction >( new NotFoundException( "Not found such payment account" ) ) ;

		return await base.UpdateAsync( id , entity ) ;
	}
}
