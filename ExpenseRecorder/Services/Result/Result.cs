namespace ExpenseRecorder.Services.Result ;

public class Result < T >
{
	private readonly T         _value ;
	private readonly Exception _error ;

	public T         Value => _value ;
	public Exception Error => _error ;

	public readonly bool IsSuccess = false ;
	public readonly bool IsFailure = true ;
	public Result(T value)
	{
		IsSuccess = true ;
		IsFailure = false ;
		_value = value ;
		_error = default! ;
	}

	public Result(Exception error)
	{
		_error = error ;
		_value = default! ;
	}

	public TU Match < TU >(Func< T , TU > onSuccess , Func< Exception , TU > onFailure)
	{
		if ( IsSuccess ) { return onSuccess( _value ) ; }

		return onFailure( _error ) ;
	}
}
