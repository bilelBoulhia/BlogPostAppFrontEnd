

namespace articleapp.DTO
{
    class CommentDTO
    {
        public string CommentContent { get; set; } = string.Empty;
        public int ArticleId { get; set; }
        public int UserId { get; set; }
    }
}
