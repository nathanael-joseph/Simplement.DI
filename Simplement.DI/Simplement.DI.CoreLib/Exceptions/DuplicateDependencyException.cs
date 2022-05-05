namespace Simplement.DI.CoreLib.Exceptions
{
    public class DuplicateDependencyException : InvalidOperationException
    {
        public DuplicateDependencyException(Type t)
            : base($"The dependency {t.Name} can only be registered once.")
        { }
    }
}
