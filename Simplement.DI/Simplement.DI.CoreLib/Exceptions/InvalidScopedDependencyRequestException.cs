namespace Simplement.DI.CoreLib.Exceptions
{
    public class InvalidScopedDependencyRequestException : InvalidOperationException
    {
        public InvalidScopedDependencyRequestException(Type t)
            : base($"The dependency {t.Name} can only be requested from a scoped container.")
        { }
    }
}
