using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using BarcodeScannerApp.Models;
using Microsoft.Maui.Controls;

namespace BarcodeScannerApp
{
    public class MainViewModel : BindableObject
    {
        public ObservableCollection<BarcodeItem> BarcodeItems { get; set; } = new ObservableCollection<BarcodeItem>();

        public ICommand AddBarcodeCommand { get; }
        public ICommand IncreaseQuantityCommand { get; }
        public ICommand DecreaseQuantityCommand { get; }
        public ICommand DeleteItemCommand { get; }

        public MainViewModel()
        {
            AddBarcodeCommand = new Command<(string barcode, int quantity)>(tuple => AddOrUpdateBarcode(tuple.barcode, tuple.quantity));
            IncreaseQuantityCommand = new Command<BarcodeItem>(item => IncreaseQuantity(item));
            DecreaseQuantityCommand = new Command<BarcodeItem>(item => DecreaseQuantity(item));
            DeleteItemCommand = new Command<BarcodeItem>(item => DeleteItem(item));
        }

        public void AddOrUpdateBarcode(string barcode, int quantity)
        {
            var existingItem = BarcodeItems.FirstOrDefault(b => b.Code == barcode);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                BarcodeItems.Add(new BarcodeItem { Code = barcode, Quantity = quantity });
            }
            OnPropertyChanged(nameof(BarcodeItems));
        }

        private void IncreaseQuantity(BarcodeItem item)
        {
            if (item != null)
            {
                item.Quantity++;
            }
        }

        private void DecreaseQuantity(BarcodeItem item)
        {
            if (item != null && item.Quantity > 0)
            {
                item.Quantity--;
                if (item.Quantity == 0)
                {
                    Application.Current.MainPage.DisplayAlert("Confirmar Exclusão",
                        "A quantidade chegou a zero. Deseja excluir este item?", "Sim", "Não")
                        .ContinueWith(task =>
                        {
                            if (task.Result == true)
                            {
                                // Remove o item da lista
                                DeleteItem(item);
                            }
                            else
                            {
                                item.Quantity = 1;
                            }
                        });
                }
            }
        }

        private void DeleteItem(BarcodeItem item)
        {
            if (item != null && BarcodeItems.Contains(item))
            {
                BarcodeItems.Remove(item);
            }
        }
    }
}
