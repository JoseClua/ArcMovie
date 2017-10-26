using ArcMovie.Interfaces;

using Xamarin.Forms;

namespace ArcMovie.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();            
            
            ItemsListView.ItemSelected += (sender, e) => {
                ((ListView)sender).SelectedItem = null;
            };
        }        
    }
}
