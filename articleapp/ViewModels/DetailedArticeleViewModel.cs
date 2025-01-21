using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

using articleapp.Models;
using articleapp.Repo;

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

        public DetailedArticeleViewModel(object articleId, ArticleRepo articleRepo)
        {
            _articleId = articleId;
            _articleRepo = articleRepo;

   
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

        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
