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
        private UserModel Userdata;
        [ObservableProperty]
        private string userName;
        [ObservableProperty]
        private string userImage;
        [ObservableProperty]
        private int followingCount;

        [ObservableProperty]
        private ObservableCollection<string> hobbies;
        [ObservableProperty]
        private ObservableCollection<BasicArticleWithDetails> currentArticles;

        [ObservableProperty]
        private bool myArticlesSelected;
        [ObservableProperty]
        private bool savedArticlesSelected;

        public ProfileViewModel(UserRepo userRepo, ArticleRepo articleRepo)
        {
            _userRepo = userRepo;
            _articleRepo = articleRepo;
            Hobbies = new ObservableCollection<string>();
            CurrentArticles = new ObservableCollection<BasicArticleWithDetails>();
           
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
            Userdata = userData;
            var userFollower = await GetUserFollowers();
            var userHobbies = await GetUserHobbies();

            if (userData != null)
            {
                UserName = $"{userData.UserName} {userData.UserFamilyName}";
                
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

        private async Task<List<BasicArticleWithDetails>> GetUserArticles(int userId)
        {
            return await _articleRepo.GetAllArticlesByUser(userId);
        }

        private async Task<List<BasicArticleWithDetails>> GetUserSavedArticles(int userId)
        {
            return await _articleRepo.GetSavedArticlesByUser(userId);
        }



        [RelayCommand]
        private async Task LoadArticles(bool myArticles)
        {
            try
            {
                MyArticlesSelected = myArticles;
                SavedArticlesSelected = !myArticles;
                if(CurrentArticles != null) { CurrentArticles.Clear(); } else { CurrentArticles = new ObservableCollection<BasicArticleWithDetails>(); }

               

                List<BasicArticleWithDetails> articles;
                if (myArticles)
                {
                    articles = await GetUserArticles(Userdata.UserId);
                }
                else
                {
                    articles = await GetUserSavedArticles(Userdata.UserId);
                }

                if(articles != null)
                {
                    foreach (var article in articles)
                    {

                        CurrentArticles?.Add(new BasicArticleWithDetails
                        {
                            ArticleId = article.ArticleId,
                            ArticleTitle = article.ArticleTitle,
                            ArticleContent = article.ArticleContent,
                            ArticleCreatedAt = article.ArticleCreatedAt,
                            NumberOfComments = article.NumberOfComments,
                            NumberOfLikes = article.NumberOfLikes,
                            CategoryName = article.CategoryName,
                            UserName = article.UserName,
                        });
                    }
                } else { currentArticles = null; };
               
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

     
    }
}