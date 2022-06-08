namespace ExpenseRecorder.Exceptions ;

public class PredicateMismatchException : Exception
{
	public PredicateMismatchException()
	{ }

	public PredicateMismatchException(string message)
		: base( message )
	{ }

	public PredicateMismatchException(string message , Exception innerException)
		: base( message , innerException )
	{ }
}
