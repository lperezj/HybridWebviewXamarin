using System;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NkdApp
{
    public partial class App : Application
    {
        private const string APP_SETTINGS_FILE = "NkdApp.appsettings.json";

        public App()
        {
            InitializeComponent();

            Plugin.Media.CrossMedia.Current.Initialize();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        /// <summary>
        /// Create an instance for the IConfigurationRoot.
        /// It should be created here because <see cref="APP_SETTINGS_FILE"/>.json file should be in this assembly
        /// and .Build() is not working in an independent class inside other assembly
        /// </summary>
        /// <returns>An instance of configuration</returns>
        public static IConfigurationRoot BuildConfiguration()
        {
            var embeddedResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(APP_SETTINGS_FILE);

            var configuration = new ConfigurationBuilder()
                .AddJsonStream(embeddedResourceStream)
                .Build();
            return configuration;
        }
    }
}
