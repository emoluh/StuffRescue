namespace Core.Common.Configuration
{
    public class ConfigHelper
    {
        public class ConnectionStrings
        {
            public static string DefaultConnection = "DefaultConnection";
            public static string Value = "Server=(localdb)\\mssqllocaldb;Database=StuffRescue;Trusted_Connection=True;MultipleActiveResultSets=true";
        }
        public class Authentication
        {
            public class Facebook
            {
                public static string AppId = "Authentication:Facebook:AppId";
                public static string AppSecret = "Authentication:Facebook:AppSecret";
            }
            public class Google
            {
                public static string ClientId = "Authentication:Google:ClientId";
                public static string ClientSecret = "Authentication:Google:ClientSecret";
            }
        }
        public class Email
        {
            public class SendGrid
            {
                public static string SendGridUser = "Email:SendGrid:SendGridUser";
                public static string SendGridKey = "Email:SendGrid:SendGridKey";
            }
            public class Sender
            {
                public static string From = "Email:Sender:From";
                public static string Name = "Email:Sender:Name";
            }
        }
        public class SMS
        {
            public class Twilio
            {
                public static string SMSAccountIdentification = "SMSAccountIdentification";
                public static string SMSAccountPassword = "SMSAccountPassword";
                public static string SMSAccountFrom = "SMSAccountFrom";
            }
        }
        public class FeatureToggle
        {
           public static string Facebook = "Facebook";
        }
        public class Logging
        {
            public static string Name = "Logging";
            public static string IncludeScopes = "IncludeScopes";
            public class LogLevel
            {
                public static string Default = "Default";
            }
        }
    }
}
