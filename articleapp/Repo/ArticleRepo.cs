using articleapp.Helpers;
using articleapp.Interfaces;
using articleapp.Models;
using articleapp.auth; 
using Microsoft.Extensions.Configuration;

namespace articleapp.Repo
{
    public class ArticleRepo : IArticles
    {
        private readonly AuthContext _authContext = AuthContext.Instance;


      


        private async Task<string> GetTokenAsync()
        {
            var token = await _authContext.GetAsync("access_token");
            if (string.IsNullOrEmpty(token))
            {
                throw new InvalidOperationException("no token");
            }
            return token;
        }

        public async Task<string> PostArticle(ArticleModel articleModel)
        {
            var accessToken = await GetTokenAsync(); 
            string url = "https://articlesapp.onrender.com/api/Article/CreateArticle";
            return await HttpRequestHelper.PostRequest<string>(url, articleModel, accessToken);
        }

        public async Task<LikeArticleModel> PostArticleLike(LikeArticleModel likearticleModel)
        {
            var accessToken = await GetTokenAsync(); 
            string url = "https://articlesapp.onrender.com/api/Article/addlike";
            return await HttpRequestHelper.PostRequest<LikeArticleModel>(url, likearticleModel, accessToken);
        }

        public async Task<string> DeleteArticle(int articleId)
        {
            var accessToken = await GetTokenAsync(); 
            string url = $"https://articlesapp.onrender.com/api/Article/DeleteArticle?articleId={articleId}";
            return await HttpRequestHelper.DeleteRequest(url, accessToken);
        }

        public async Task<string> DeleteLikeArticle(LikeArticleModel likeArticle)
        {
            var accessToken = await GetTokenAsync(); 
            string url = "https://articlesapp.onrender.com/api/Article/RemoveArticleLike";
            return await HttpRequestHelper.DeleteRequest(url, accessToken, likeArticle);
        }

        public async Task<List<BasicArticleWithDetails>> GetAllArticles()
        {
            var accessToken = await GetTokenAsync(); 
            string url = "https://articlesapp.onrender.com/api/Article/GetAllArticles";
            return await HttpRequestHelper.GetRequest<List<BasicArticleWithDetails>>(url, accessToken);
        }

        public async Task<List<BasicArticleWithDetails>> GetAllArticlesByUser(int userId)
        {
            var accessToken = await GetTokenAsync();
            string url = $"https://articlesapp.onrender.com/api/Article/GetArticleByUser?userId={userId}";
            return await HttpRequestHelper.GetRequest<List<BasicArticleWithDetails>>(url, accessToken);
        }

        public async Task<FullArticleWithDetails> GetDetailedArticle(int articleId)
        {
            var accessToken = await GetTokenAsync(); 
            string url = $"https://articlesapp.onrender.com/api/Article/GetFullArticleDetails?articleId={articleId}";
            var articles = await HttpRequestHelper.GetRequest<List<FullArticleWithDetails>>(url, accessToken);
            return articles?.FirstOrDefault();
        }

        public async Task<List<BasicArticleWithDetails>> GetArticlesByCategory(int articleId)
        {
            var accessToken = await GetTokenAsync(); 
            string url = $"https://articlesapp.onrender.com/api/Article/GetArticlesByCategory?categoryId={articleId}";
            return await HttpRequestHelper.GetRequest<List<BasicArticleWithDetails>>(url, accessToken);
        }

        #region saved articles
        public async Task<List<BasicArticleWithDetails>> GetSavedArticlesByUser(int userId)
        {
            var accessToken = await GetTokenAsync(); 
            string url = $"https://articlesapp.onrender.com/api/Article/GetAllArticlesSavedByUser?userId={userId}";
            return await HttpRequestHelper.GetRequest<List<BasicArticleWithDetails>>(url, accessToken);
        }

        public async Task<List<BasicArticleWithDetails>> SearchArticle(string? searchQuery)
        {
            var accessToken = await GetTokenAsync();
            string url = $"https://articlesapp.onrender.com/api/Article/SearchArticle?searchQuery={searchQuery}";
            return await HttpRequestHelper.GetRequest<List<BasicArticleWithDetails>>(url, accessToken);
        }

        public async Task<List<CategoryModel>> GetAllCategory()
        {
            var accessToken = await GetTokenAsync(); 
            string url = "https://articlesapp.onrender.com/api/Article/GetAllCategory";
            return await HttpRequestHelper.GetRequest<List<CategoryModel>>(url, accessToken);
        }
        #endregion
    }
}
