namespace AspNetBase.Core.Configuration {

    public class AppNotificationOptions
    {
        public string CurrentSmsProvider { get; set; }

        public AppSmsProvider[] SmsProviders { get; set; }
    }

    public class AppSmsProvider
    {

        public string Name { get; set; }

        public string Url { get; set; }

        public string ApiKey { get; set; }

        public string SecretKey { get; set; }

        public string LineNumber { get; set; }

        public string LineNumber2 { get; set; }
    }
}
