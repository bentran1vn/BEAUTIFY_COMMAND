namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Categories;
internal sealed class MoveSubCategoryToCategoryCommandHandler(IRepositoryBase<Category, Guid> categoryRepositoryBase)
    : ICommandHandler<CONTRACT.Services.Categories.Commands.MoveSubCategoryToCategoryCommand>
{
    public async Task<Result> Handle(CONTRACT.Services.Categories.Commands.MoveSubCategoryToCategoryCommand request,
        CancellationToken cancellationToken)
    {
        if (request.SubCategoryId.Equals(request.CategoryId))
        {
            return Result.Failure(new Error("400", "Category and SubCategory are the same"));
        }

        var category = await categoryRepositoryBase.FindByIdAsync(request.CategoryId, cancellationToken);
        if (category == null)
        {
            return Result.Failure(new Error("404", "Category not found"));
        }

        if (!category.IsParent)
        {
            return Result.Failure(new Error("400", "Category is not main category"));
        }

        var subCategory = await categoryRepositoryBase.FindByIdAsync(request.SubCategoryId, cancellationToken);
        if (subCategory == null)
        {
            return Result.Failure(new Error("404", "SubCategory not found"));
        }

        subCategory.ParentId = category.Id;
        categoryRepositoryBase.Update(subCategory);
        return Result.Success();
    }
}