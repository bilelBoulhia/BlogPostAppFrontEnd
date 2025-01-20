using articleapp.DTO;
using articleapp.Models;


namespace articleapp.Interfaces
{
    interface IComments
    {
        Task<CommentDTO> PostComment(ArticleModel articleModel, string? token = null);
        Task<LikeCommentModel> PostCommentLike(LikeCommentModel likecomment, string? token = null);
        Task<string> DeleteComment(int commentId, string? token = null);
        Task<string> DeleteLikeComment(LikeCommentModel commentlike, string? token = null);
        Task<ArticleModel> GetCommentLikes(int commentId, string? token = null);

    }
}
