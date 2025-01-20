using articleapp.Models;
using articleapp.Repo;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using Serilog;

namespace articleapp.ViewModels
{
    public partial class ProfileViewModel : ObservableObject
    {
        private readonly UserRepo _userRepo;
        private readonly ArticleRepo _articleRepo;
        [ObservableProperty]
        private string userName;
        [ObservableProperty]
        private string userImage;
        [ObservableProperty]
        private int followingCount;

        [ObservableProperty]
        private ObservableCollection<string> hobbies;
        [ObservableProperty]
        private ObservableCollection<ArticleModel> currentArticles;
        [ObservableProperty]
        private Dictionary<int, string> articleCategories;
        [ObservableProperty]
        private bool myArticlesSelected;
        [ObservableProperty]
        private bool savedArticlesSelected;

        public ProfileViewModel(UserRepo userRepo, ArticleRepo articleRepo)
        {
            _userRepo = userRepo;
            _articleRepo = articleRepo;
            Hobbies = new ObservableCollection<string>();
            CurrentArticles = new ObservableCollection<ArticleModel>();
            ArticleCategories = new Dictionary<int, string>();
            MyArticlesSelected = true;
            SavedArticlesSelected = false;
            InitializeAsync();
        }

        private async void InitializeAsync()
        {
            await LoadUserDataAsync();
        }

        private async Task LoadUserDataAsync()
        {
            var userData = await GetUserData();
            var userFollower = await GetUserFollowers();
            var userHobbies = await GetUserHobbies();

            if (userData != null)
            {
                UserName = $"{userData.UserName} {userData.UserFamilyName}";
                UserImage = userData.UserImage ?? "placeholder_pfp.jpg";
                FollowingCount = userFollower.Count;

                if (userHobbies != null)
                {
                    Hobbies.Clear();
                    foreach (var hobby in userHobbies)
                    {
                        Hobbies.Add(hobby.HobbyName);
                    }
                }
                await LoadArticles(true);
            }
        }

        private async Task<UserModel> GetUserData()
        {
            return await _userRepo.GetUserData();
        }

        private async Task<List<FollowerModel>> GetUserFollowers()
        {
            return await _userRepo.GetUserFollowing();
        }

        private async Task<List<HobbyModel>> GetUserHobbies()
        {
            return await _userRepo.GetUserHobby();
        }

        private async Task<List<ArticleModel>> GetUserArticles()
        {
            return await _articleRepo.GetAllArticlesByUser();
        }

        private async Task<List<ArticleModel>> GetUserSavedArticles()
        {
            return await _articleRepo.GetSavedArticlesByUser();
        }



        [RelayCommand]
        private async Task LoadArticles(bool myArticles)
        {
            try
            {
                MyArticlesSelected = myArticles;
                SavedArticlesSelected = !myArticles;
                CurrentArticles.Clear();
                ArticleCategories.Clear();

                List<ArticleModel> articles;
                if (myArticles)
                {
                    articles = await GetUserArticles();
                }
                else
                {
                    articles = await GetUserSavedArticles();
                }

              
            }
            catch (Exception ex)
            {
                Log.Information($"{ex.Message}");
            }
        }

        [RelayCommand]
        private async Task ShowMyArticles()
        {
            await LoadArticles(true);
        }

        [RelayCommand]
        private async Task ShowSavedArticles()
        {
            await LoadArticles(false);
        }

        // Helper method to get category name for an article
        public string GetCategoryNameForArticle(int articleId)
        {
            return ArticleCategories.TryGetValue(articleId, out string categoryName)
                ? categoryName
                : "Uncategorized";
        }
    }
}