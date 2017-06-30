namespace Core.Common.Configuration
{
    public interface IApplicationSettings
    {
        string ConnectionString { get; }
        string LoggerName { get; }
        string SenderEmail { get; }
        string SenderName { get; }
    }
}
