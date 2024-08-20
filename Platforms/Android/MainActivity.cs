using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Util;
using AndroidX.LocalBroadcastManager.Content;
using CommunityToolkit.Mvvm.Messaging;

namespace BarcodeScannerApp.Platforms.Android
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            RegisterReceivers();

            // IntentFilter filter = new IntentFilter();
            // filter.AddAction("com.prodan.barcodescanner.SCAN_RESULT");
            //  RegisterReceiver(new ScanReceiver(this), filter);


            WeakReferenceMessenger.Default.Register<string>(this, (r, li) =>
            {
                if (li == "11")
                {
                    Intent i = new Intent();
                    i.SetAction("com.symbol.datawedge.api.ACTION");
                    i.PutExtra("com.symbol.datawedge.api.SCANNER_INPUT_PLUGIN", "DISABLE_PLUGIN");
                    i.PutExtra("SEND_RESULT", "true");
                    i.PutExtra("COMMAND_IDENTIFIER", "MY_DISABLE_SCANNER");  //Unique identifier
                    this.SendBroadcast(i);
                }
                else if (li=="22") {
                        Intent i = new Intent();
                        i.SetAction("com.symbol.datawedge.api.ACTION");
                        i.PutExtra("com.symbol.datawedge.api.SCANNER_INPUT_PLUGIN", "ENABLE_PLUGIN");
                        i.PutExtra("SEND_RESULT", "true");
                        i.PutExtra("COMMAND_IDENTIFIER", "MY_ENABLE_SCANNER");  //Unique identifier
                        this.SendBroadcast(i);
                    }
            });


        }

        void RegisterReceivers()
        {
            IntentFilter filter = new IntentFilter();
            filter.AddCategory("android.intent.category.DEFAULT");
            filter.AddAction("com.prodan.barcodescanner.SCAN_RESULT");
            filter.AddAction("com.zebra.sensors");

            Intent regres = RegisterReceiver(new IntentReceiver(), filter);
        }

        public class ScanReceiver : BroadcastReceiver
        {
            private readonly MainActivity _activity;

            public ScanReceiver(MainActivity activity)
            {
                _activity = activity ?? throw new ArgumentNullException(nameof(activity));
            }

            public override void OnReceive(Context context, Intent intent)
{
                string barcode = intent.GetStringExtra("barcode");

            if (!string.IsNullOrEmpty(barcode))
            {
                   
                    _activity.RunOnUiThread(() =>
                {
                    MessagingCenter.Send(_activity, "BarcodeReceived", barcode);
                });
            }
        }

        }
    }
}
