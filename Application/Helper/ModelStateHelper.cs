using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Application.Helper;

public class ModelStateHelper
{
    public static ModelStateDictionary AddErrors(ValidationResult validationResult)
    {
        var modelStateDictionary = new ModelStateDictionary();
        
        foreach (ValidationFailure failure in validationResult.Errors)
        {
            modelStateDictionary.AddModelError(failure.PropertyName, failure.ErrorMessage);
        }

        return modelStateDictionary;
    }
}