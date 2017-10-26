using Prism.Mvvm;
using ArcMovie.Interfaces;

using Xamarin.Forms;

namespace ArcMovie.ViewModels
{
    public class BaseViewModel : BindableBase
    {
        protected IMovieService ApiService => DependencyService.Get<IMovieService>();

        bool isBusy = false;

        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;

        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }        
    }
}
