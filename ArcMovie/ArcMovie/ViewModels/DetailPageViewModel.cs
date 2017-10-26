using Prism.Navigation;
using System.Threading.Tasks;
using ArcMovie.Models;

namespace ArcMovie.ViewModels
{
    public class DetailPageViewModel : BaseViewModel, INavigationAware
    {
        private Movie movie;
        public Movie Movie
        {
            get { return movie; }
            set { SetProperty(ref movie, value); }
        }        
  
        private async Task LoadMovieDetailAsync(int movieId)
        {
            var movieDetail = await ApiService.GetMovieDetailAsync(movieId).ConfigureAwait(false);
            if (movieDetail != null)
            {
                Movie = movieDetail;
            }            
        }

        public async void OnNavigatingTo(NavigationParameters parameters)
        {
            Movie = parameters.GetValue<Movie>("movie");            
            Title = Movie.Title;
            await LoadMovieDetailAsync(Movie.Id).ConfigureAwait(false);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            // INavigationAware
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            // INavigationAware
        }
    }
}