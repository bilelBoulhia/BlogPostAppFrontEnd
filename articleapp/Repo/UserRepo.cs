using articleapp.auth;
using articleapp.Helpers;
using articleapp.Interfaces;
using articleapp.Models;
using Microsoft.Extensions.Configuration;

namespace articleapp.Repo
{
    public class UserRepo : IUser
    {
        private readonly AuthContext _authContext = AuthContext.Instance;

        public UserRepo()
        {
         
        }

      
        private async Task<string> GetTokenAsync()
        {
            var token = await _authContext.GetAsync("access_token");
            if (string.IsNullOrEmpty(token))
            {
                throw new InvalidOperationException("no token found");
            }
            return token;
        }

        public async Task<TokenModel> PerformLogin(LoginModel loginModel)
        {
            string url = "https://articlesapp.onrender.com/api/User/Login";
            return await HttpRequestHelper.PostRequest<TokenModel>(url, loginModel);
        }

        public async Task<bool> CheckAuth(string token)
        {
            
            string url = "https://articlesapp.onrender.com/api/Helpers/CheckAuth";
            return await HttpRequestHelper.GetRequest<bool>(url, token);
        }

        public async Task<UserModel> GetUserData(int userId)
        {
            var token = await GetTokenAsync(); 
            string url = $"https://articlesapp.onrender.com/api/User/GetAccountById?userId={userId}";
            return await HttpRequestHelper.GetRequest<UserModel>(url, token);
        }

        public async Task<List<FollowerModel>> GetUserFollowing(int userId)
        {
            var token = await GetTokenAsync(); 
            string url = $"https://articlesapp.onrender.com/api/User/GetUserFollowing?userId={userId}";
            return await HttpRequestHelper.GetRequest<List<FollowerModel>>(url, token);
        }

        public async Task<List<HobbyModel>> GetUserHobby(int userId)
        {
            var token = await GetTokenAsync(); 
            string url = $"https://articlesapp.onrender.com/api/User/GetUserHobbies?userId={userId}";
            return await HttpRequestHelper.GetRequest<List<HobbyModel>>(url, token);
        }

        public async Task<string> PerformSignUp(UserModel user)
        {
            string url = "https://articlesapp.onrender.com/api/User/CreateAccount";
            return await HttpRequestHelper.PostRequest<string>(url, user);
        }

        public async Task<string> AddHobbies(HobbiesModel hobby)
        {
            string url = "https://articlesapp.onrender.com/api/User/AddHobbies";
            return await HttpRequestHelper.PostRequest<string>(url, hobby);
        }

        public async Task<List<HobbyModel>> GetHobbies()
        {
            string url = "https://articlesapp.onrender.com/api/User/GetAllHobbies";
            return await HttpRequestHelper.GetRequest<List<HobbyModel>>(url);
        }

        public Task PerformLogout()
        {
            _authContext.Remove("access_token");
            _authContext.Remove("refresh_token");
            _authContext.Remove("userId");

            return Task.CompletedTask;
        }
    }
}
