using CommunityToolkit.Mvvm.Messaging;
namespace BarcodeScannerApp.Views
{
    public partial class MainPage : ContentPage
    {
        private MainViewModel ViewModel => BindingContext as MainViewModel;

        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();

            WeakReferenceMessenger.Default.Register<string>(this, (sender,barcode) =>
            {
                ShowBarcodePopup(barcode);
            });
        }

        private void ShowBarcodePopup(string barcode = "")
        {
            BarcodeEntry.Text = barcode;

            PopupOverlay.IsVisible = true;
            PopupFrame.IsVisible = true;
        }

        private void OnPopupOkClicked(object sender, EventArgs e)
        {
            string code = BarcodeEntry.Text;
            if (int.TryParse(QuantityEntry.Text, out int quantity) && !string.IsNullOrEmpty(code))
            {
                ViewModel.AddOrUpdateBarcode(code, quantity);
            }

            PopupOverlay.IsVisible = false;
            PopupFrame.IsVisible = false;
            BarcodeEntry.Text = string.Empty;
            QuantityEntry.Text = string.Empty;
        }

        private void OnPopupCancelClicked(object sender, EventArgs e)
        {
            PopupOverlay.IsVisible = false;
            PopupFrame.IsVisible = false;
            BarcodeEntry.Text = string.Empty;
            QuantityEntry.Text = string.Empty;
        }

        private void OnAddBarcodeButtonClicked(object sender, EventArgs e)
        {
            ShowBarcodePopup();
        }
    }
}
