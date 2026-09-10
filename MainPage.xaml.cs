using System.Collections.ObjectModel;
using ZXing.Net.Maui;

namespace MauiApp2
{
    public partial class MainPage : ContentPage
    {

        ObservableCollection<item> Products { get; set; }
        public MainPage()
        {
            InitializeComponent();
            Products = new ObservableCollection<item>();
            BindingContext = this;
        }

        /*private void add_Clicked(object sender, EventArgs e)
        {
            if(quantity.Text != "" && entryadd.Text != "")
            {
                Products.Add(new item { Name = entryadd.Text, Quantity = int.Parse(quantity.Text) });
                list.ItemsSource = Products;
                entryadd.Text = "";
                quantity.Text = "";
            }
            
            
        }*/

        protected override void OnAppearing() // nadpisanie domyślnej metody OnAppearing która jest wywoływana gdy strona staje się widoczna
        {
            base.OnAppearing();

            // Konfiguracja formatów
            cameraView.Options = new BarcodeReaderOptions
            {
                Formats = BarcodeFormats.OneDimensional | BarcodeFormats.TwoDimensional,
                AutoRotate = true,
                Multiple = true,
                TryHarder = true
            };
            
        }


        private void Button_Clicked(object sender, EventArgs e)
        {
            Products.Remove((item)list.SelectedItem);
        }

        private void cameraView_BarcodesDetected(object sender, ZXing.Net.Maui.BarcodeDetectionEventArgs e)
        {
            var result = e?.Results?.FirstOrDefault(); // Jeśli e nie jest null pobierz wynik(kolekcje wyników) i weź pierwszy element(FirstOrDefault)
            if (result is null)
                return;
            MainThread.BeginInvokeOnMainThread(() =>
            {
                codeValue.Text = result.Value;
                Products.Add(new item { Name = codeValue.Text, Quantity = 0 });
                list.ItemsSource = Products;
            });
        }

        private void dodaj(object sender, EventArgs e)
        {
            if (list.SelectedItem is item product)
            {
                product.Quantity++;
                list.ItemsSource = null;
                list.ItemsSource = Products;
            }
        }

        private void usunac(object sender, EventArgs e)
        {
            if (list.SelectedItem is item product)
            {
                product.Quantity--;
                if(product.Quantity > 0)
                {
                    list.ItemsSource = null;
                    list.ItemsSource = Products;
                }
                
            }
        }
    }
}
