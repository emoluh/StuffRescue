using System;
using Microsoft.Extensions.Configuration;

namespace Core.Common.Configuration
{
    public class WebConfigApplicationSettings : IApplicationSettings
    {
        private IConfigurationRoot _config;

        public WebConfigApplicationSettings(IConfigurationRoot config)
        {
            _config = config;
        }
        public string ConnectionString
        {
            get { return _config[ConfigHelper.ConnectionStrings.DefaultConnection]; }
        }
        public string LoggerName
        {
            get { return _config[ConfigHelper.Logging.Name]; }
        }

        public string SenderEmail
        {
            get { return _config[ConfigHelper.Email.Sender.From]; }
        }

        public string SenderName
        {
            get { return _config[ConfigHelper.Email.Sender.Name]; }
        }
    }
}
