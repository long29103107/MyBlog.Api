# LonG.CSVLib
.Net Csv Library

1. Add dependency injection
```
serivces.AddCSVLibService();
```

2. Inject to contructor to use
```
private readonly IFileProccess _fileProccess;

public SampleService(IFileProccess fileProccess)
{
	_fileProccess = fileProccess;
}
```