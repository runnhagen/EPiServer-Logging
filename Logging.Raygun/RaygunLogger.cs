using System;
using EPiServer.Logging;
using Mindscape.Raygun4Net;

namespace Logging.Raygun
{
    public class RaygunLogger : ILogger
    {
        public bool IsEnabled(Level level)
        {
            if (level == Level.Error)
                return true;

            return false;
        }

        public void Log<TState, TException>(Level level, TState state, TException exception, Func<TState, TException, string> messageFormatter) where TException : Exception
        {
            if (messageFormatter == null)
                return;

            if (ErrorShouldBeLogged(level) && ErrorShouldBeWrappedInException(state))
                exception = (TException)new Exception((string)(object)state, exception);

            if (ErrorShouldBeLogged(level))
                new RaygunClient().SendInBackground(exception);
//                new RaygunClient().Send(exception);
        }

        private static bool ErrorShouldBeLogged(Level level)
        {
            return level == Level.Error;
        }

        private static bool ErrorShouldBeWrappedInException<TState>(TState state)
        {
            return state is string && !string.IsNullOrWhiteSpace((string) (object) state);
        }
    }
}
