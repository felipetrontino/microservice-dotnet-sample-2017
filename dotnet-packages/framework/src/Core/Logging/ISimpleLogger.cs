using System;

namespace Framework.Core.Logging
{
    public interface ISimpleLogger
    {
        void Debug(string message);

        void Info(string message);

        void Warn(string message);

        void Error(string message, Exception ex = null);

        void Log(LoggingType type, string message);
    }
}