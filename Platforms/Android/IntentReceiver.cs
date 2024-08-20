using Android.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Messaging;
using Android.App;

namespace BarcodeScannerApp.Platforms.Android
{

    [BroadcastReceiver(Enabled = true, Exported = true)]
    [IntentFilter(new[] { "com.prodan.barcodescanner.SCAN_RESULT" })]
    public class IntentReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            //System.Console.WriteLine("Here is DW on MAUI");
            if (intent.Extras != null)
            {
                String bc_type = intent.Extras.GetString("com.symbol.datawedge.label_type");
                String bc_data = intent.Extras.GetString("com.symbol.datawedge.data_string");

                WeakReferenceMessenger.Default.Send(bc_data);
            }


        }
    }

}
