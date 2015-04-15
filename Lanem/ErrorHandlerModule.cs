using System;
using System.Configuration;
using System.Web;
using Lanem.ErrorFilters;
using Lanem.ErrorLoggers;
using Lanem.ExceptionFormatters;
using Lanem.IO;

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

            LogError(exception);
        }

        protected virtual void LogError(Exception exception)
        {
            var errorLogger = CreateErrorLogger();

            errorLogger.Log(exception);
        }

        protected virtual IErrorLogger CreateErrorLogger()
        {
            var errorLogPath = ConfigurationManager.AppSettings["Lanem_Log_Directory_Path"];

            return new FileErrorLogger(
                new NoErrorFilter(),
                new MarkdownExceptionFormatter(),
                new LogFilePathGenerator(errorLogPath),
                new FileWriter());
        }

        public void Dispose()
        {
        }
    }
}