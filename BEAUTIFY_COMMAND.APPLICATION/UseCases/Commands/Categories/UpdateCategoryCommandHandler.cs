namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Categories;
public class UpdateCategoryCommandHandler : ICommandHandler<CONTRACT.Services.Categories.Commands.UpdateCategoryCommand>
{
    private readonly IRepositoryBase<Category, Guid> _categoryRepository;

    public UpdateCategoryCommandHandler(IRepositoryBase<Category, Guid> categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Result> Handle(CONTRACT.Services.Categories.Commands.UpdateCategoryCommand request,
        CancellationToken cancellationToken)
    {
        var isExisted = await _categoryRepository.FindByIdAsync(request.Id, cancellationToken);

        if (isExisted == null || isExisted.IsDeleted) return Result.Failure(new Error("404", "Category not found "));

        isExisted.Name = request.Name;
        isExisted.Description = request.Description;

        if (request.ParentId == null) return Result.Success("Category updated.");
        var isParentExisted = await _categoryRepository.FindByIdAsync((Guid)request.ParentId, cancellationToken);

        if (isParentExisted == null || isParentExisted.IsDeleted)
            return Result.Failure(new Error("404", "Parent Category not found "));
        isExisted.ParentId = isParentExisted.Id;

        return Result.Success("Category updated.");
    }
}