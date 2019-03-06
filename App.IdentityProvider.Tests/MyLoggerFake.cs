using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using com.b_velop.App.IdentityProvider;

namespace App.IdentityProvider.Tests
{
    public class MyLoggerFake<T> : ILogger<T>
    {
        public HashSet<string> Logs { get; set; }
        public MyLoggerFake()
        {
            Logs = new HashSet<string>();
        }
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            Logs.Add(state.ToString());
        }
    }

    public class MyLoggerFactory : ILoggerFactory
    {
  

        public ILogger CreateLogger(string categoryName)
        {
            return new MyLoggerFake<IIdentityProviderService>();
        }

        public void AddProvider(ILoggerProvider provider)
        {
        }

        public void Dispose()
        {
        }
    }
}
