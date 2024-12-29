namespace Application.Common.Exceptions;

public class ForbiddenAccessException() : Exception("The current user's access is denied to perform this action.")
{
}
