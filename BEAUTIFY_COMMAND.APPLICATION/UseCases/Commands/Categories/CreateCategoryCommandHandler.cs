namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Categories;
public class CreateCategoryCommandHandler : ICommandHandler<CONTRACT.Services.Categories.Commands.CreateCategoryCommand>
{
    private readonly IRepositoryBase<Category, Guid> _categoryRepository;

    public CreateCategoryCommandHandler(IRepositoryBase<Category, Guid> categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Result> Handle(CONTRACT.Services.Categories.Commands.CreateCategoryCommand request,
        CancellationToken cancellationToken)
    {
        var category = new Category
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            IsParent = true
        };
        if (request.ParentId != null)
        {
            var isExisted = await _categoryRepository.FindByIdAsync((Guid)request.ParentId, cancellationToken);

            if (isExisted == null || isExisted.IsDeleted)
                return Result.Failure(new Error("404", "Category not found "));

            category.ParentId = isExisted.Id;
            category.IsParent = false;
        }

        _categoryRepository.Add(category);

        return Result.Success("Category created.");
    }
}