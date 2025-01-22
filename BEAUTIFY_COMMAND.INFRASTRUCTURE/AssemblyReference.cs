using System.Reflection;

namespace BEAUTIFY_COMMAND.INFRASTRUCTURE;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}