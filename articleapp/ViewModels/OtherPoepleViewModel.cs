using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using articleapp.Models;
using articleapp.Repo;

using Serilog;
namespace articleapp.ViewModels
{
    public partial class OtherPoepleViewModel : ObservableObject
    {

        private readonly object _userId;
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

  
 

        public OtherPoepleViewModel(object userId, UserRepo userRepo,ArticleRepo articleRepo)
        {
            _userRepo = userRepo;
            _articleRepo = articleRepo;
            _userId = userId;
            Hobbies = new ObservableCollection<string>();
            CurrentArticles = new ObservableCollection<BasicArticleWithDetails>();


         
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

                if (userFollower != null)
                {
                    FollowingCount = userFollower.Count;
                } else { FollowingCount = 0; }      
                

                if (userHobbies != null)
                {
                    Hobbies.Clear();
                    foreach (var hobby in userHobbies)
                    {
                        Hobbies.Add(hobby.HobbyName);
                    }
                }
                await LoadArticles();
            }
        }

        private async Task<UserModel> GetUserData()
        {
            return await _userRepo.GetUserData(Convert.ToInt32(_userId));
        }

        private async Task<List<FollowerModel>> GetUserFollowers()
        {
            return await _userRepo.GetUserFollowing(Convert.ToInt32(_userId));
        }

        private async Task<List<HobbyModel>> GetUserHobbies()
        {
            return await _userRepo.GetUserHobby(Convert.ToInt32(_userId));
        }

        private async Task<List<BasicArticleWithDetails>> GetUserArticles(int userId)
        {
            return await _articleRepo.GetAllArticlesByUser(userId);
        }

    



        [RelayCommand]
        private async Task LoadArticles()
        {
            try
            {

                CurrentArticles.Clear();


                List<BasicArticleWithDetails> articles;
              
               articles = await GetUserArticles(Userdata.UserId);
              

                foreach (var article in articles)
                {

                    CurrentArticles.Add(new BasicArticleWithDetails
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
            }
            catch (Exception ex)
            {
                Log.Information($"{ex.Message}");
            }
        }



   
    


    }
}
