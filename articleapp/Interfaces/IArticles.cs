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
        Task<string> PostArticle(ArticleModel articleModel);
        Task<LikeArticleModel> PostArticleLike(LikeArticleModel likearticleModel);
        Task<string> DeleteArticle(int articleId);
        Task<string> DeleteLikeArticle(LikeArticleModel likeArticle);
        Task<List<BasicArticleWithDetails>> GetAllArticles();
        Task<List<BasicArticleWithDetails>> GetAllArticlesByUser(int userId );


        // Saved articles
        Task<List<BasicArticleWithDetails>> GetSavedArticlesByUser(int userId);
       
    }
}
