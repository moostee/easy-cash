namespace EasyCash.Application
{
    using System.Reflection;
    using EasyCash.Application.Commands;

    public static class Assemblies
    {
        public static readonly Assembly Application = typeof(InternalCommandBase).Assembly;
    }
}
