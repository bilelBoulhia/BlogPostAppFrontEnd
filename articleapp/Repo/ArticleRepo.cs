using articleapp.Helpers;
using articleapp.Interfaces;
using articleapp.Models;


namespace articleapp.Repo
{
    public class ArticleRepo : IArticles
    {
        string temptoken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJhdWQiOiJ1c2VycyIsImlzcyI6InNlcnZlciIsImV4cCI6MTc0MDE2MDc0NCwiaWF0IjoxNzM3MTYwNzQ0LCJuYmYiOjE3MzcxNjA3NDQsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJzdHJpbmciLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjE0IiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvZW1haWxhZGRyZXNzIjoiZEBnLmMifQ.9jZ7TDu7fqariOxHDSJ0bP8HiENNucR34fEc_nTj0LQ";


        public async Task<ArticleModel> PostArticle( ArticleModel articleModel ,string? token = null)
        {
            string url = $"https://articlesapp.onrender.com/api/Article/CreateArticle";
            return await HttpRequestHelper.PostRequest<ArticleModel>(url,articleModel ,temptoken);
        }

        public async Task<LikeArticleModel> PostArticleLike(LikeArticleModel likearticleModel, string? token = null)
        {
            string url = $"https://articlesapp.onrender.com/api/Article/addlike";
            return await HttpRequestHelper.PostRequest<LikeArticleModel>(url, likearticleModel, temptoken);
        }


        public async Task<string> DeleteArticle(int articleId, string? token = null)
        {
            string url = $"https://articlesapp.onrender.com/api/Article/DeleteArticle?articleId={articleId}";
            return await HttpRequestHelper.DeleteRequest(url, temptoken);
        }

        public async Task<string> DeleteLikeArticle(LikeArticleModel likeArticle, string? token = null)
        {
            string url = $"https://articlesapp.onrender.com/api/Article/RemoveArticleLike";
            return await HttpRequestHelper.DeleteRequest(url, temptoken, likeArticle);
        }

        public async Task<List<BasicArticleWithDetails>> GetAllArticles(string? token = null)
        {
            string url = $"https://articlesapp.onrender.com/api/Article/GetAllArticles";
            return await HttpRequestHelper.GetRequest<List<BasicArticleWithDetails>>(url, temptoken);
        }
        public async Task<List<BasicArticleWithDetails>> GetAllArticlesByUser(int userId,string? token = null  )
        {
            string url = $"https://articlesapp.onrender.com/api/Article/GetArticleByUser?userId={userId}";
            return await HttpRequestHelper.GetRequest<List<BasicArticleWithDetails>>(url, temptoken);
        }


      public async Task<FullArticleWithDetails> GetDetailedArticle(int articleId ,string? token = null )
{
    string url = $"https://articlesapp.onrender.com/api/Article/GetFullArticleDetails?articleId={articleId}";
    
    // Deserialize the response into a List<FullArticleWithDetails> (even if there's only one item)
    var articles = await HttpRequestHelper.GetRequest<List<FullArticleWithDetails>>(url, temptoken);
    
    // Return the first article from the list (assuming only one article is returned)
    return articles?.FirstOrDefault();
}


        #region saved articles
        public async Task<List<BasicArticleWithDetails>> GetSavedArticlesByUser(int userId , string? token = null)
        {
            string url = $"https://articlesapp.onrender.com/api/Article/GetAllArticlesSavedByUser?userId={userId}";
            return await HttpRequestHelper.GetRequest<List<BasicArticleWithDetails>>(url, temptoken);
        }
        public async Task<List<BasicArticleWithDetails>> SearchArticle(string? searchQuery,string? token = null )
        {
            string url = $"https://articlesapp.onrender.com/api/Article/SearchArticle?searchQuery={searchQuery}";
            return await HttpRequestHelper.GetRequest<List<BasicArticleWithDetails>>(url, temptoken);
        }


        public async Task<List<CategoryModel>> GetAllCategory(string? token = null)
        {
            string url = $"https://articlesapp.onrender.com/api/Article/GetAllCategory";
            return await HttpRequestHelper.GetRequest<List<CategoryModel>>(url, temptoken);
        }



        #endregion
    }
}
