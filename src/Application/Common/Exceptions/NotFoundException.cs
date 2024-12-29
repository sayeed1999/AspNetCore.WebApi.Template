namespace Application.Common.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException() : base("Item with the specified id is not found.")
    {
    }

    public NotFoundException(string message) : base(message)
    {
    }

    public static void ThrowIfNull(object? entity)
    {
        if (entity == null)
        {
            throw new NotFoundException();
        }
    }
}
