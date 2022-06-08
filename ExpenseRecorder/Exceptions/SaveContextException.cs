namespace ExpenseRecorder.Exceptions ;

public class SaveContextException : Exception
{
	public SaveContextException()
	{ }

	public SaveContextException(string message)
		: base( message )
	{ }

	public SaveContextException(string message , Exception inner)
		: base( message , inner )
	{ }
}
