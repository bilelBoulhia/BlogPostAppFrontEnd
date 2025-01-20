
using articleapp.Helpers;
using articleapp.Interfaces;
using articleapp.Models;

using Microsoft.Extensions.Configuration;


namespace articleapp.Repo
{
    public class UserRepo : IUser
    {
        private readonly IConfiguration _configuration;

        public UserRepo(IConfiguration configuration)
        {
            _configuration = configuration;
        }
 
           string temptoken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJhdWQiOiJ1c2VycyIsImlzcyI6InNlcnZlciIsImV4cCI6MTc0MDE2MDc0NCwiaWF0IjoxNzM3MTYwNzQ0LCJuYmYiOjE3MzcxNjA3NDQsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJzdHJpbmciLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjE0IiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvZW1haWxhZGRyZXNzIjoiZEBnLmMifQ.9jZ7TDu7fqariOxHDSJ0bP8HiENNucR34fEc_nTj0LQ";
        public async Task<TokenModel> PerformLogin(LoginModel loginModel)
        {
            string url = "https://articlesapp.onrender.com/api/User/Login";
            return await HttpRequestHelper.PostRequest<TokenModel>(url, loginModel);
        }

        public async Task<UserModel> GetUserData(string? token = null, int? userId = 14)
        {
            string url = $"https://articlesapp.onrender.com/api/User/GetAccountById?userId={userId}";
            return await HttpRequestHelper.GetRequest<UserModel>(url, temptoken);
        }

        public async Task<List<FollowerModel>> GetUserFollowing(string? token = null, int? userId = 14)
        {
            string url = $"https://articlesapp.onrender.com/api/User/GetUserFollowing?userId={userId}";
            return await HttpRequestHelper.GetRequest<List<FollowerModel>>(url, temptoken);
        }

        public async Task<List<HobbyModel>> GetUserHobby(int? userId = 14)
        {
            string url = $"https://articlesapp.onrender.com/api/User/GetUserHobbies?userId={userId}";
            return await HttpRequestHelper.GetRequest<List<HobbyModel>>(url, temptoken);
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



        



        public Task PerformLogout(TokenModel tokenModel)
        {
            throw new NotImplementedException();
        }

        public Task<string> UploadProfileImage(string imageName)
        {
            throw new NotImplementedException();
        }
    }
}
/*
 
// curl -X 'GET' \
//  'http://localhost:5121/api/User/GetAccountById?userId=13' \
//  -H 'accept: */
//*' \

//- H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJhdWQiOiJ1c2VycyIsImlzcyI6InNlcnZlciIsImV4cCI6MTczNzE1Mjc1NiwiaWF0IjoxNzM3MTQ1NTU2LCJuYmYiOjE3MzcxNDU1NTYsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJzdHJpbmciLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjE0IiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvZW1haWxhZGRyZXNzIjoiZEBnLmMifQ.qAF2G77bU_RcsdIh0HmnYydlJepSMpgxIjFyL6c9AWM'
//Request URL
//http://localhost:5121/api/User/GetAccountById?userId=13
//Server response
//Code	Details
//200	
//Response body
//Download
//{
//  "userId": 13,
//  "userName": "string",
//  "userFamilyName": "string",
//  "userPhoneNumber": 0,
//  "userBirthDay": "2025-01-17T00:00:00",
//  "userHash": "d5f5701e561a3751d98f8b54d603db933c73ca46f8e2518c88c02bd52a00b0e0",
//  "userSalt": "LQAAb3RddKFwuq36TZ3ybQ==",
//  "userImage": "string",
//  "userEmail": "b@g.c",
//  "articles": [],
//  "comments": [],
//  "reports": [],
//  "articles1": [],
//  "articlesNavigation": [],
//  "commentsNavigation": [],
//  "follwers": [],
//  "hobbies": [],
//  "users": []
//}
 
 