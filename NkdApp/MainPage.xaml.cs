using System;
using System.Threading;
using Microsoft.Extensions.Configuration;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing;

namespace NkdApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        CancellationTokenSource cts;
        private DeviceFeatureHelper _deviceFeaturesHelper;

        public MainPage()
        {
            MessagingCenter.Subscribe<object, string>(this, "SCAN_QR", OnScanQRChanged);

            InitializeComponent();

            IConfigurationRoot settings = App.BuildConfiguration();
            var bot_url = settings.GetSection("BOT_URL").Value;
            var parameters = settings.GetSection("BOT_PARAMETERS").Value;

            string fullUrl = bot_url + parameters;
            webViewElement.Source = fullUrl;
            //webViewElement.Source = new HtmlWebViewSource()
            //{
            //    Html = ContentMock.HtmlSourceContent.Content
            //};

            webViewElement.RegisterAction(ExecuteActionFromJavascript);

            _deviceFeaturesHelper = new DeviceFeatureHelper();

        }

        private void OnScanQRChanged(object arg1, string arg2)
        {
            if (!string.IsNullOrWhiteSpace(arg2))
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await webViewElement.EvaluateJavaScriptAsync($"setresult_scanqr('{arg2}')");
                });
            }
        }

        private async void ExecuteActionFromJavascript(string param1, string param2)
        {
            if (param1 != null && param1.Equals("PHOTO") && param2.Equals("CAMERA"))
            {
                var result = await _deviceFeaturesHelper.TakePhoto(this);
                if (result != null)
                {
                    await webViewElement.EvaluateJavaScriptAsync($"setresult_takephoto('{result}')");
                }
            }
            else if (param1 != null && param1.Equals("PHOTO") && param2.Equals("GALLERY"))
            {
                var result = await _deviceFeaturesHelper.SelectPhoto(this);
                if (result != null)
                {
                    await webViewElement.EvaluateJavaScriptAsync($"setresult_selectphoto('{result}')");
                }
            }
            else if (param1 != null && param1.Equals("DEVICEINFO"))
            {
                var result = await _deviceFeaturesHelper.GetDeviceData();
                if (result != null)
                {
                    await webViewElement.EvaluateJavaScriptAsync($"setresult_getdeviceinfo('{result}')");
                }
            }
            else if (param1 != null && param1.Equals("GPS"))
            {
                var result = await _deviceFeaturesHelper.GetGpsLocation();
                if (result != null)
                {
                    await webViewElement.EvaluateJavaScriptAsync($"setresult_getgpslocation('{result}')");
                }
            }
            else if (param1 != null && param1.Equals("QR"))
            {
                var scanPage = new ScanQRPage();
                await Navigation.PushModalAsync(scanPage);
            }
        }
    }
}
