namespace Simplement.DI.CoreLib.Exceptions
{
    public class UnknownScopeException : InvalidOperationException
    {
        public UnknownScopeException(Type t)
            : base($"An unknown scope was provided for the dependancy {t.Name}")
        { }
    }
}
