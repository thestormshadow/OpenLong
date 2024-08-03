using Serilog;
using Serilog.Enrichers;

namespace Long.Shared.Helpers
{
    public static class Logger
    {
        private static bool initialized = false;

        public static void Initialize(string fileName)
        {
            if (initialized)
            {
                return;
            }

            Log.Logger = new LoggerConfiguration()
#if DEBUG
                .MinimumLevel.Debug()
#else
                .MinimumLevel.Information()
#endif
                .Enrich.With(new ThreadIdEnricher())
                .WriteTo.Console(outputTemplate: "{Timestamp:HH:mm:ss.fff} [{Level:u3}] ({ThreadId}) {Message}{NewLine}{Exception}")
                .WriteTo.File($"Logs/{fileName}.log", outputTemplate: "{Timestamp:HH:mm:ss.fff} [{Level}] ({ThreadId}) {Message}{NewLine}{Exception}{NewLine}", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            initialized = true;
        }

        public static ILogger CreateSysLogger(string fileName)
        {
            if (!initialized)
            {
                throw new TypeInitializationException("Logger is not initialized.", null);
            }

            return new LoggerConfiguration()
#if DEBUG
                .MinimumLevel.Debug()
#else
                .MinimumLevel.Information()
#endif
                .WriteTo.File($"Logs/{fileName}.log", outputTemplate: "{Timestamp:HH:mm:ss.fff} [{Level}] ({ThreadId}) {Message}{NewLine}{Exception}{NewLine}", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }


        public static ILogger CreateSysConsoleLogger(string fileName)
        {
            if (!initialized)
            {
                throw new TypeInitializationException("Logger is not initialized.", null);
            }

            return new LoggerConfiguration()
#if DEBUG
                .MinimumLevel.Debug()
#else
                .MinimumLevel.Information()
#endif
                .Enrich.With(new ThreadIdEnricher())
                .WriteTo.Console(outputTemplate: "{Timestamp:HH:mm:ss.fff} [{Level:u3}] {Message}{NewLine}{Exception}")
                .WriteTo.File($"Logs/{fileName}.log", outputTemplate: "{Timestamp:HH:mm:ss.fff} [{Level}] ({ThreadId}) {Message}{NewLine}{Exception}{NewLine}", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }

        public static ILogger CreateLogger(string fileName)
        {
            if (!initialized)
            {
                throw new TypeInitializationException("Logger is not initialized.", null);
            }

            return new LoggerConfiguration()
                .WriteTo.File(@"GmLogs/" + $"{fileName}.log", outputTemplate: "{Message} - {Timestamp:HH:mm:ss.fff}{NewLine}", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }

        public static ILogger CreateConsoleLogger(string fileName)
        {
            if (!initialized)
            {
                throw new TypeInitializationException("Logger is not initialized.", null);
            }

            return new LoggerConfiguration()
                .WriteTo.Console(outputTemplate: "{Timestamp:HH:mm:ss.fff} [{Level:u3}] {Message}{NewLine}{Exception}")
                .WriteTo.File(@"GmLogs/" + $"{fileName}.log", outputTemplate: "{Message} - {Timestamp:HH:mm:ss.fff}{NewLine}", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }
    }
}