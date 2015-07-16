using EPiServer.Logging;

namespace Logging.Raygun
{
    public class RaygunLoggerFactory : ILoggerFactory
    {
        public RaygunLoggerFactory()
        {
        }

        public ILogger Create(string name)
        {
            return new RaygunLogger();
        }
    }
}
