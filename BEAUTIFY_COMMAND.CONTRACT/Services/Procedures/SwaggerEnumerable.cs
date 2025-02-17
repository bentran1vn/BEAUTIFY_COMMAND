using System.Collections;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BEAUTIFY_COMMAND.CONTRACT.Services.Procedures;

public class SwaggerEnumerable<T> : IModelBinder where T : IEnumerable
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var val = bindingContext.ValueProvider.GetValue(bindingContext.FieldName);
        var first = string.Concat("[", string.Join(",", val.ToList()), "]");
        var model = JsonSerializer.Deserialize<T>(first);
        bindingContext.Result = ModelBindingResult.Success(model);
        return Task.CompletedTask;
    }
}