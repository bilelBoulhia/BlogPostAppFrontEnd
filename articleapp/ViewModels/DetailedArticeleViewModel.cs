using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using articleapp.Models;
using articleapp.Pages;
using articleapp.Repo;
using CommunityToolkit.Mvvm.Input;

namespace articleapp.ViewModels
{
    public class DetailedArticeleViewModel : INotifyPropertyChanged
    {
        private readonly object _articleId;
        private readonly ArticleRepo _articleRepo;

        private FullArticleWithDetails _articleDetails;
        public FullArticleWithDetails ArticleDetails
        {
            get => _articleDetails;
            set
            {
                _articleDetails = value;
                OnPropertyChanged();
            }
        }

        public IRelayCommand AuthorTappedCommand { get; }

        public DetailedArticeleViewModel(object articleId, ArticleRepo articleRepo)
        {
            _articleId = articleId;
            _articleRepo = articleRepo;

            AuthorTappedCommand = new RelayCommand(OnAuthorTapped);

            LoadArticleDetailsAsync();
        }

        private async Task LoadArticleDetailsAsync()
        {
            try
            {
                ArticleDetails = await _articleRepo.GetDetailedArticle(Convert.ToInt32(_articleId));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading article details: {ex.Message}");
            }
        }

        private async void OnAuthorTapped()
        {
            if (ArticleDetails?.UserId != null)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new OtherPoeplePage(ArticleDetails.UserId));
            }
        }

        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
