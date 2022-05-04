namespace Simplement.DI.CoreLib.Exceptions
{
    public class InvalidScopedDependancyRequestException : InvalidOperationException
    {
        public InvalidScopedDependancyRequestException(Type t)
            : base($"No scope was provided forr the scoped dependency {t.Name}.")
        { }
    }
}
