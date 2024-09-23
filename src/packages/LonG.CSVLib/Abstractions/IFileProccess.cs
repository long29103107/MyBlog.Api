using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http;
using System.Collections;

namespace LonG.CSVLib.Abstractions;

public interface IFileProccess
{
    public Stream WriteToStream(ICollection data, ClassMap mapper = null);
    public ICollection<T> ReadFile<T>(IFormFile file, ClassMap mapper = null);
}