using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using LonG.CSVLib.Abstractions;
using System.Collections;
using Microsoft.AspNetCore.Http;


namespace LonG.CSVLib.Implements;


public class FileProccess : IFileProccess
{
    public Stream WriteToStream(ICollection data, ClassMap map = null)
    {
        var stream = new MemoryStream();
        using (var writer = new StreamWriter(stream, leaveOpen: true))
        {
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);

            if (map is not null)
            {
                csv.Context.RegisterClassMap(map);
            }

            csv.WriteRecords(data);
        }

        stream.Position = 0;
        return stream;
    }

    public ICollection<T> ReadFile<T>(IFormFile file, ClassMap map = null)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            ShouldSkipRecord = (record) => record.Row.Parser.Record?.All((field) => string.IsNullOrWhiteSpace(field)) ?? false
        };

        using var reader = new StreamReader(file.OpenReadStream());
        using var csv = new CsvReader(reader, config);

        if (map is not null)
        {
            csv.Context.RegisterClassMap(map);
        }

        var records = csv.GetRecords<T>();
        var result = records.ToList();

        return result;
    }
}