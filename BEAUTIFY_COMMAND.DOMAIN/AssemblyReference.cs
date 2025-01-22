using System.Reflection;

namespace BEAUTIFY_COMMAND.DOMAIN;
public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}