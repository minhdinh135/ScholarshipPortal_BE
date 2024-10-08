using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;

namespace Application.Helper;

public class ErrorHandler
{
    public static string? GetErrorMessage(ModelStateDictionary modelState)
    {
        foreach (var modelStateEntry in modelState)
        {
            var errors = modelStateEntry.Value.Errors;
            return errors.FirstOrDefault()?.ErrorMessage;
        }

        return null;
    }

    public static string? GetErrorMessage(List<ValidationResult> validationResults)
    {
        // Return the first error message if available
        return validationResults.FirstOrDefault()?.ErrorMessage;
    }

    public static bool Validate<T>(T obj, out List<ValidationResult> validationResults)
    {
        var context = new ValidationContext(obj, null, null);
        validationResults = new List<ValidationResult>();
        return Validator.TryValidateObject(obj, context, validationResults, true);
    }
}