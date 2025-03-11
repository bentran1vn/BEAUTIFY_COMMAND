namespace BEAUTIFY_COMMAND.CONTRACT.Services.Categories;

public static class Commands
{
    public record CreateCategoryCommand(string Name, string Description, Guid? ParentId) : ICommand;
    public record UpdateCategoryCommand(Guid Id, string Name, string Description, Guid? ParentId) : ICommand;
    public record DeleteCategoryCommand(Guid Id) : ICommand;
}