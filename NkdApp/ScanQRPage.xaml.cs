using System;
using System.Collections.Generic;

using Xamarin.Forms;
using ZXing;

namespace NkdApp
{
    public partial class ScanQRPage : ContentPage
    {
        public ScanQRPage()
        {
            InitializeComponent();
        }

        public async void scanView_OnScanResult(Result result)
        {
            //Device.BeginInvokeOnMainThread(async () =>
            //{
            //    await DisplayAlert("Scanned result", "The barcode's text is " + result.Text + ". The barcode's format is " + result.BarcodeFormat, "OK");
            //});

            MessagingCenter.Send<object, string>(this, "SCAN_QR", result.Text);
            await Navigation.PopModalAsync(true);
        }
    }
}
