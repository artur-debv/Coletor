using Microsoft.Maui.Controls;

namespace BarcodeScannerApp.Views
{
    public partial class BarcodePopupPage : ContentPage
    {
        public string Barcode { get; private set; }
        public int Quantity { get; private set; }
        public bool IsConfirmed { get; private set; }

        public BarcodePopupPage()
        {
            InitializeComponent();
        }

        private async void OnOkButtonClicked(object sender, EventArgs e)
        {
            if (int.TryParse(QuantityEntry.Text, out int quantity))
            {
                Barcode = BarcodeEntry.Text;
                Quantity = quantity;
                IsConfirmed = true;
                await Navigation.PopModalAsync();
            }
            else
            {
                await DisplayAlert("Invalid Quantity", "Please enter a valid quantity.", "OK");
            }
        }

        private async void OnCancelButtonClicked(object sender, EventArgs e)
        {
            IsConfirmed = false;
            await Navigation.PopModalAsync();
        }
    }
}
