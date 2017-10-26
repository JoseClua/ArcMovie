using Xamarin.Forms;

namespace ArcMovie.Views
{
    public partial class DetailPage : ContentPage
    {       
        public DetailPage()
        {
            InitializeComponent();
            
            GenresListView.ItemSelected += (sender, e) => {
                // Manually deselect item
                ((ListView)sender).SelectedItem = null;
            };
        }        
    }
}
