using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using ArcMovie.Helpers;
using ArcMovie.Models;
using Xamarin.Forms;

namespace ArcMovie.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {        
        private int currentPage = 1;
        private int totalPage = 0;

        private List<Genre> genres;        
        public ObservableMovieCollection<Movie> Movies { get; set; }

        public DelegateCommand LoadUpcomingMoviesCommand { get; }
        public DelegateCommand<Movie> ShowMovieDetailCommand { get; }
        public DelegateCommand<Movie> UpdateMovieListCommand { get; }

        private readonly INavigationService navigationService;
        public MainPageViewModel(INavigationService navigationService)
        {
            Title = "Upcoming Movies";
            this.navigationService = navigationService;
            Movies = new ObservableMovieCollection<Movie>();

            LoadUpcomingMoviesCommand = new DelegateCommand(async () => await ExecuteLoadUpcomingMoviesCommand().ConfigureAwait(false));
            ShowMovieDetailCommand = new DelegateCommand<Movie>(async (Movie movie) => await ExecuteShowMovieDetailCommand(movie).ConfigureAwait(false));
            UpdateMovieListCommand = new DelegateCommand<Movie>(async (Movie movie) => await ExecuteUpdateMovieListCommand(movie).ConfigureAwait(false));

            LoadUpcomingMoviesCommand.Execute();
        }        

        private async Task ExecuteLoadUpcomingMoviesCommand()
        {

            IsBusy = true;

            try
            {
                Movies.Clear();
                currentPage = 1;
                await LoadMoviesAsync(currentPage, Enums.MovieCategory.Upcoming).ConfigureAwait(false);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task ExecuteShowMovieDetailCommand(Movie movie)
        {            
            var parameters = new NavigationParameters();
            parameters.Add(nameof(movie), movie);
            await navigationService.NavigateAsync("DetailPage", parameters).ConfigureAwait(false);
        }

        private async Task ExecuteUpdateMovieListCommand(Movie movie)
        {
            int itemLoadNextItem = 2;
            int viewCellIndex = Movies.IndexOf(movie);
            if (Movies.Count - itemLoadNextItem <= viewCellIndex)
            {                
                await NextPageUpcomingMoviesAsync().ConfigureAwait(false);
            }
        }

        private async Task LoadMoviesAsync(int page, Enums.MovieCategory movieCategory)
        {
            try
            {
                var continueOnCapturedContext = Device.RuntimePlatform == Device.Windows;

                genres = genres ?? await ApiService.GetGenresAsync().ConfigureAwait(continueOnCapturedContext);
                var searchMovie = await ApiService.GetMoviesByCategoryAsync(page, movieCategory).ConfigureAwait(continueOnCapturedContext);
                if (searchMovie != null)
                {
                    var movies = new List<Movie>();
                    totalPage = searchMovie.TotalPages;
                    foreach (var movie in searchMovie.Movies)
                    {                                                
                        GenreListToString(movie);                        
                        movies.Add(movie);
                    }
                    Movies.AddRange(movies);
                }                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public async Task NextPageUpcomingMoviesAsync()
        {
            currentPage++;
            if (currentPage <= totalPage)
            {                
                await LoadMoviesAsync(currentPage, Enums.MovieCategory.Upcoming).ConfigureAwait(false);
            }
        }

        private void GenreListToString(Movie movie)
        {            
            var genresMovie = genres.Where(genre => movie.GenreIds.Any(genreId => genreId == genre.Id));
            movie.GenresNames = movie.GenreIds != null && genresMovie != null ?
                genresMovie.Select(g => g.Name).Aggregate((first, second) => $"{first}, {second}") :
                "Undefined";            
        }
    }
}