# Lanem
**L**ight **A**sp.**N**et **E**rror Handler **M**odule for processing and logging ASP.NET application exceptions.

[![Build status](https://ci.appveyor.com/api/projects/status/ta3vwfttw0g85l2a/branch/master?svg=true)](https://ci.appveyor.com/project/dustinmoris/lanem/branch/master)
[![NuGet Version](https://img.shields.io/nuget/v/Lanem.svg)](https://www.nuget.org/packages/Lanem/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/Lanem.svg)](https://www.nuget.org/packages/Lanem/)

[![Build status](https://ci-buildstats.azurewebsites.net/appveyor/chart/dustinmoris/lanem)](https://ci.appveyor.com/project/dustinmoris/lanem/history)

## About
Lanem is an extremely light weight HttpModule to globally handle ASP.NET errors.

It doesn't implement any HTTP endpoints to view error logs from a remote location. The logs can only be accessed by someone who has access to the production environment.

## Support

Currently the project supports ASP.NET applications built with .NET Framework 3.5, 4.0, 4.5 and 4.6.

## Installation

Please use the official [nuget.org](https://www.nuget.org/) feed to download and install the package in your ASP.NET application.

```
PM> Install-Package Lanem
```

This project follows [Semantic Versioning](http://semver.org/).

## Usage

By default the error handler logs all exceptions into your local file storage. You can set the error log directory path in your application config file:

```
<appSettings>
  <add key="Lanem_Log_Directory_Path" value="~\App_Data\Logs"/>
</appSettings>
```

### Override default behaviour

If you have special requirements you can create a derived class from the default error handler and override the `CreateErrorLogger` method:

```
public class CustomErrorHandlerModule : ErrorHandlerModule
{
    protected override IErrorLogger CreateErrorLogger(HttpApplication application)
    {
        // return your custom implementation
    }
}
```

You must swap your custom error handler with the default one in your web.config:
```
<system.webServer>
  <modules>
    <add name="CustomErrorHandler" type="MyNamespace.CustomErrorHandlerModule, MyAssembly"/>
  </modules>
</system.webServer>
```

#### Customizing the FileErrorLogger

The by default shipped `FileErrorLogger` class allows some additional customization.

You can provide an
-   `IErrorFilter` to limit which errors get logged
-   `IExceptionSerializer` to change how the exception object gets written into the file
-   `ILogFilePathGenerator` if you want to change how the name of an error file gets generated
-   `IFileWriter` if you wish to change how the contents get written onto disk

For example if you want to prevent logging *HTTP 404 - Not found* errors you could do the following:

```
public class SkipNotFoundErrors : IErrorFilter
{
    public bool SkipError(Exception exception)
    {
        var httpException = exception as HttpException;

        return (httpException != null && httpException.GetHttpCode() == 404);
    }
}

public class CustomErrorHandlerModule : ErrorHandlerModule
{
    protected override IErrorLogger CreateErrorLogger(HttpApplication application)
    {
        return new FileErrorLogger(
            new SkipNotFoundErrors(), // <-- Providing your custom error filter
            new JsonExceptionSerializer(),
            new LogFilePathGenerator("your-path-error-log-path"),
            new FileWriter());
    }
}
```

If you want to combine multiple error filters you could use the Decorator pattern:

```
public class SkipNotFoundErrors : IErrorFilter
{
    public bool SkipError(Exception exception)
    {
        var httpException = exception as HttpException;

        return (httpException != null && httpException.GetHttpCode() == 404);
    }
}

public class SkipUnauthorizedErrors : IErrorFilter
{
    private readonly IErrorFilter _errorFilter;

    public SkipUnauthorizedErrors(IErrorFilter errorFilter)
    {
        _errorFilter = errorFilter;
    }

    public bool SkipError(Exception exception)
    {
        if (_errorFilter.SkipError(exception))
            return true;

        var httpException = exception as HttpException;

        return (httpException != null && httpException.GetHttpCode() == 401);
    }
}

public class CustomErrorHandlerModule : ErrorHandlerModule
{
    protected override IErrorLogger CreateErrorLogger(HttpApplication application)
    {
        return new FileErrorLogger(
            new SkipUnauthorizedErrors(
                new SkipNotFoundErrors()), // <-- Providing multiple error filters using the Decorator pattern
            new JsonExceptionSerializer(),
            new LogFilePathGenerator("your-path-error-log-path"),
            new FileWriter());
    }
}
```

## Sample of a default error log

```
Sunday, 22 November 2015 11:29:20

Exception:
--------------
{
  "ClassName": "System.Web.HttpException",
  "Message": "A potentially dangerous Request.Path value was detected from the client (<).",
  "Data": null,
  "InnerException": null,
  "HelpURL": null,
  "StackTraceString": "   at System.Web.HttpRequest.ValidateInputIfRequiredByConfig()\r\n   at System.Web.HttpApplication.PipelineStepManager.ValidateHelper(HttpContext context)",
  "RemoteStackTraceString": null,
  "RemoteStackIndex": 0,
  "ExceptionMethod": "8\nValidateInputIfRequiredByConfig\nSystem.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a\nSystem.Web.HttpRequest\nVoid ValidateInputIfRequiredByConfig()",
  "HResult": -2147467259,
  "Source": "System.Web",
  "WatsonBuckets": null,
  "_httpCode": 400
}

HTTP Request:
--------------
GET http://localhost:62192/<script> HTTP/1.1
Cache-Control: no-cache
Connection: keep-alive
Accept: text/html
Accept-Encoding: gzip, deflate, sdch
Accept-Language: en-US,en;q=0.8,en-GB;q=0.6
Host: localhost:62192
User-Agent: Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/46.0.2490.86 Safari/537.36
Postman-Token: 8a5ca996-0884-ef60-1d97-42bd635a5f08
DNT: 1
```

## Contribution

Feedback is more than welcome and pull requests are accepted!

You can either contact me via GitHub or find me on [dusted.codes](http://dusted.codes/).
