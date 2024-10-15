using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

namespace Application.Helper;

public class CsvUtils
{
    public static IEnumerable<T> ReadFile<T>(string csvFilePath)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HeaderValidated = null,
            MissingFieldFound = null
        };
            
        using (var reader = new StreamReader(csvFilePath))
        using (var csv = new CsvReader(reader, config))
        {
            return csv.GetRecords<T>().ToList();
        } 
    }
}