using MediatR;

namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Categories;

public class DeleteCategoryCommandHandler: ICommandHandler<CONTRACT.Services.Categories.Commands.DeleteCategoryCommand>
{
    private readonly IRepositoryBase<Category, Guid> _categoryRepository;

    public DeleteCategoryCommandHandler(IRepositoryBase<Category, Guid> categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }
    
    public async Task<Result> Handle(CONTRACT.Services.Categories.Commands.DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var isExisted = await _categoryRepository.FindByIdAsync(request.Id, cancellationToken);
        
        if (isExisted == null || isExisted.IsDeleted)
        {
            return Result.Failure(new Error("404", "Category not found "));
        }
        
        _categoryRepository.Remove(isExisted);
        
        return Result.Success("Category deleted.");
    }
}