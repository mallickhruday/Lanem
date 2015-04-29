﻿using System;
using System.Web;
using System.Web.Configuration;
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

            LogError(application, exception);
        }

        protected virtual void LogError(HttpApplication application, Exception exception)
        {
            var errorLogger = CreateErrorLogger(application);

            errorLogger.Log(exception);
        }

        protected virtual IErrorLogger CreateErrorLogger(HttpApplication application)
        {
            var errorLogPath = application.Server.MapPath(
                WebConfigurationManager.AppSettings["Lanem_Log_Directory_Path"]);

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