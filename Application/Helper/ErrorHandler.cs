using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

namespace Application.Helper;
public class ErrorHandler {
  public static string? GetErrorMessage(ModelStateDictionary modelState){
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

  public static string GetDbError(DbUpdateException ex)
  {
      Exception innerEx = ex;
      while(innerEx != null)
      {
        if (innerEx is MySqlException sqlEx)
        {
          // Check the SQL error number for specific constraint violations
          if (sqlEx.Number == 547) // Foreign key constraint violation
          {
            return "Foreign key not found";
          }
          else if (sqlEx.Number == 2627 || sqlEx.Number == 2601) // Unique constraint violation
          {
            return "Id is duplicated";
          }
          else
          {
            // Handle other types of SQL exceptions
            return "An error occurred while saving changes. Please try again later.";
          }
        }
        innerEx = innerEx.InnerException;
      }
      return "An error occurred while saving changes. Please try again later.";
  }

}
