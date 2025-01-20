
using articleapp.DTO;
using articleapp.Helpers;
using articleapp.Interfaces;
using articleapp.Models;
namespace articleapp.Repo
{
    class CommentRepo : IComments
    {

        string temptoken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJhdWQiOiJ1c2VycyIsImlzcyI6InNlcnZlciIsImV4cCI6MTc0MDE2MDc0NCwiaWF0IjoxNzM3MTYwNzQ0LCJuYmYiOjE3MzcxNjA3NDQsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJzdHJpbmciLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjE0IiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvZW1haWxhZGRyZXNzIjoiZEBnLmMifQ.9jZ7TDu7fqariOxHDSJ0bP8HiENNucR34fEc_nTj0LQ";
        public async Task<CommentDTO> PostComment(ArticleModel articleModel, string? token = null)
        {
            string url = $"https://articlesapp.onrender.com/api/Comment/CreateComment";
            return await HttpRequestHelper.PostRequest<CommentDTO>(url, articleModel, temptoken);
        }

        public async Task<LikeCommentModel> PostCommentLike(LikeCommentModel likecomment, string? token = null)
        {
            string url = $"https://articlesapp.onrender.com/api/Comment/addlike";
            return await HttpRequestHelper.PostRequest<LikeCommentModel>(url, likecomment, temptoken);
        }

        public async Task<string> DeleteComment(int commentId, string? token = null)
        {
            string url = $"https://articlesapp.onrender.com/api/Article/DeleteComment?commentId={commentId}";
            return await HttpRequestHelper.DeleteRequest(url, temptoken);
        }

        public async Task<string> DeleteLikeComment(LikeCommentModel commentlike, string? token = null)
        {
            string url = $"https://articlesapp.onrender.com/api/Article/RemoveCommentLike";
            return await HttpRequestHelper.DeleteRequest(url, temptoken, commentlike);
        }


        public async Task<ArticleModel> GetCommentLikes(int commentId, string? token = null)
        {
            string url = $"https://articlesapp.onrender.com/api/Article/GetAllLikesOfaComment?commentId={commentId}";
            return await HttpRequestHelper.GetRequest<ArticleModel>(url, temptoken  );
        }
    }
}
