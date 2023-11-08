using log4net;
using log4net.Config;
using log4net.Repository;
using log4net.Repository.Hierarchy;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Reflection;
using System.Xml;

namespace Common.Logging;

public class Log4netLoggerProvider : ILoggerProvider, IDisposable
{
    private readonly ConcurrentDictionary<string, Log4NetLogger> _loggers = new ConcurrentDictionary<string, Log4NetLogger>();
    private const string DEFAULT_LOG4NET_CONFIG_FILE = "log4net.config";
    private const string LOG4NET_CONFIG_NODE = "log4net";
    private XmlElement Log4netConfig { get; set; }
    private ILoggerRepository _loggerRepository;
    /// <summary>Default constructor</summary>
    public Log4netLoggerProvider()
    {
        DoInit();
    }

    /// <summary>Constructor with parameters</summary>
    /// <param name="log4NetConfigFile">log4net config file</param>
    public Log4netLoggerProvider(string log4NetConfigFile)
    {
        if (string.IsNullOrEmpty(log4NetConfigFile))
            throw new ArgumentNullException(nameof(log4NetConfigFile));
        DoInit();
    }
    /// <summary>Create a logger</summary>
    /// <param name="categoryName">Logger's name</param>
    /// <returns>Get logger object</returns>
    public ILogger CreateLogger(string categoryName)
    {
        if (string.IsNullOrEmpty(categoryName))
            throw new ArgumentNullException(nameof(categoryName));
        return _loggers.GetOrAdd(categoryName, new Func<string, Log4NetLogger>(CreateLoggerImplementation));
    }

    private void DoInit()
    {
        Log4netConfig = Parselog4NetConfigFile(DEFAULT_LOG4NET_CONFIG_FILE);
        _loggerRepository = LogManager.CreateRepository(Assembly.GetExecutingAssembly(), typeof(Hierarchy));
        XmlConfigurator.Configure(_loggerRepository, Log4netConfig);
    }
    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="isDisposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
    protected virtual void Dispose(bool isDisposing)
    {
        if (!isDisposing)
            return;
        _loggers.Clear();
    }

    /// <summary>Create Logger implemementation</summary>
    /// <param name="name">Logger's name</param>
    /// <returns>Log4net logger implementation</returns>
    private Log4NetLogger CreateLoggerImplementation(string name)
    {
        if (!File.Exists(DEFAULT_LOG4NET_CONFIG_FILE))
        {
            using (StreamWriter text = File.CreateText(DEFAULT_LOG4NET_CONFIG_FILE))
            {
                text.Write(Log4NetConfigDefault.Text);
                text.Close();
            }
        }
        return new Log4NetLogger(name, _loggerRepository.Name);
    }

    /// <summary>Get xml log4net configuration from config file</summary>
    /// <param name="filename">Log4net configuration file</param>
    /// <returns>XmlElement with all log4net configuration loaded</returns>
    private static XmlElement Parselog4NetConfigFile(string filename)
    {
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.Load(File.OpenRead(filename));
        return xmlDocument[LOG4NET_CONFIG_NODE];
    }


}

internal static class Log4NetConfigDefault
{
    /// <summary>Gets the string with the configuration file.</summary>
    internal static string Text
    {
        get
        {
            return "\r\n  <log4net>\r\n\r\n    <appender name=\"RollingFileAppender\" type=\"log4net.Appender.RollingFileAppender\">\r\n      <lockingModel type = \"log4net.Appender.FileAppender+MinimalLock\" />\r\n      <file value=\"DefaultLog4Net.log\" />\r\n      <appendToFile value=\"false\" />\r\n      <staticLogFileName value=\"true\" />\r\n\r\n      <layout type=\"log4net.Layout.PatternLayout\">\r\n        <conversionPattern value=\"%-23date [%-5thread] %-5level %-25logger - %message%newline\" />\r\n      </layout>\r\n\r\n    </appender>\r\n\r\n    <!-- First entry is the default Logger-->\r\n    <logger name=\"Default\" >\r\n      <level value=\"DEBUG\" />\r\n      <appender-ref ref=\"RollingFileAppender\" />\r\n    </logger>\r\n\r\n  </log4net>";
        }
    }
}