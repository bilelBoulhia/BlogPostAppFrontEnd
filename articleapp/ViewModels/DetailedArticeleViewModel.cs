using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using articleapp.auth;
using articleapp.Models;
using articleapp.Pages;
using articleapp.Repo;
using CommunityToolkit.Mvvm.Input;

namespace articleapp.ViewModels
{
    public class DetailedArticeleViewModel : INotifyPropertyChanged
    {
        private readonly object _articleId;
        private readonly AuthContext _authContext = AuthContext.Instance;
        private readonly ArticleRepo _articleRepo;

        private FullArticleWithDetails _articleDetails;

        private bool _isArticleOwner;
        public bool IsArticleOwner
        {
            get => _isArticleOwner;
            set
            {
                _isArticleOwner = value;
                OnPropertyChanged();
            }
        }


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
        public IRelayCommand DeleteArticleCommand { get; }

        public DetailedArticeleViewModel(object articleId, ArticleRepo articleRepo)
        {
            _articleId = articleId;
            _articleRepo = articleRepo;

            AuthorTappedCommand = new RelayCommand(OnAuthorTapped);
            DeleteArticleCommand = new RelayCommand(OnDeleteArticle);
            LoadArticleDetailsAsync();
        }
        private async Task<int?> GetUserIDAsync()
        {
            try
            {

                string? userIdString = await _authContext.GetAsync("userId");


                if (int.TryParse(userIdString, out int userId))
                {
                    return userId;
                }
                else
                {

                    return null;
                }
            }
            catch (Exception ex)
            {


                return null;
            }
        }
        private async void OnDeleteArticle()
        {
            try
            {
                bool isConfirmed = await Application.Current.MainPage.DisplayAlert(
                    "Delete",
                    "Are you sure",
                    "Yes",
                    "No");

                if (isConfirmed)
                {
                    await _articleRepo.DeleteArticle(ArticleDetails.ArticleId);
                    await Application.Current.MainPage.Navigation.PopAsync();
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "eeeror", "OK");
            }
        }


        private async Task LoadArticleDetailsAsync()
        {
            try
            {
                ArticleDetails = await _articleRepo.GetDetailedArticle(Convert.ToInt32(_articleId));

                var userId = await GetUserIDAsync();
                if (userId.HasValue)
                {
                    IsArticleOwner = ArticleDetails.UserId == userId.Value;

                }
            }
            catch (Exception ex)
            {
                throw new Exception();

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
