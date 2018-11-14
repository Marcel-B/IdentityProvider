using System;

namespace com.marcelbenders.App.IdentityProvider.Services
{
    public interface ILogger
    {
        void Log(string message);
        void Log(string message, string type);
        void Log(string message, Type type);
        void Log(string message, Type type, Exception ex);
    }
}