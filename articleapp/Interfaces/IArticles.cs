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
        Task<string> PostArticle(ArticleModel articleModel, string? token = null);
        Task<LikeArticleModel> PostArticleLike(LikeArticleModel likearticleModel, string? token = null);
        Task<string> DeleteArticle(int articleId, string? token = null);
        Task<string> DeleteLikeArticle(LikeArticleModel likeArticle, string? token = null);
        Task<List<BasicArticleWithDetails>> GetAllArticles(string? token = null);
        Task<List<BasicArticleWithDetails>> GetAllArticlesByUser(int userId,string? token = null );


        // Saved articles
        Task<List<BasicArticleWithDetails>> GetSavedArticlesByUser(int userId,string? token = null);
       
    }
}
