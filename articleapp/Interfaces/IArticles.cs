using articleapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace articleapp.Interfaces
{
    interface IArticles
    {
        Task<ArticleModel> PostArticle(ArticleModel articleModel, string? token = null);
        Task<LikeArticleModel> PostArticleLike(LikeArticleModel likearticleModel, string? token = null);
        Task<string> DeleteArticle(int articleId, string? token = null);
        Task<string> DeleteLikeArticle(LikeArticleModel likeArticle, string? token = null);
        Task<List<BasicArticleWithDetails>> GetAllArticles(string? token = null);
        Task<List<BasicArticleWithDetails>> GetAllArticlesByUser(string? token = null, int? userId = 14);


        // Saved articles
        Task<List<BasicArticleWithDetails>> GetSavedArticlesByUser(string? token = null, int? userId = 14);
       
    }
}
