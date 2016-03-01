using System;
using System.Web;
using System.Web.Configuration;
using Lanem.Filters;
using Lanem.IO;
using Lanem.Loggers;
using Lanem.Parsers;

namespace Lanem
{
    public class ErrorHandlerModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.Error += OnError;
        }

        protected virtual void OnError(object sender, EventArgs e)
        {
            var application = sender as HttpApplication;

            if (application == null)
                return;

            var exception = application.Server.GetLastError();

            LogError(application, exception);
        }

        protected virtual void LogError(HttpApplication application, Exception exception)
        {
            var errorLogger = CreateErrorLogger(application);
            
            var error = new Error(
                exception,
                new HttpRequestWrapper(application.Request));

            errorLogger.Log(error);
        }

        protected virtual IErrorLogger CreateErrorLogger(HttpApplication application)
        {
            var errorLogPath = application.Server.MapPath(
                WebConfigurationManager.AppSettings["Lanem_Log_Directory_Path"]);

            return new FileErrorLogger(
                new NoExceptionFilter(),
                new TextErrorParser(),
                new LogFileNameGenerator(errorLogPath),
                new FileWriter());
        }

        public void Dispose()
        {
        }
    }
}