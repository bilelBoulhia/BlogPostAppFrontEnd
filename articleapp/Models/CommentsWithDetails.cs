using System.Text.Json.Serialization;

namespace articleapp.Models
{
    public class CommentsWithDetails
    {
        [JsonPropertyName("commentId")]
        public int CommentId { get; set; }

        [JsonPropertyName("commentContent")]
        public string CommentContent { get; set; }

        [JsonPropertyName("userId")]
        public int UserId { get; set; }

        [JsonPropertyName("userName")]
        public string UserName { get; set; }

        [JsonPropertyName("numberOfLikes")]
        public int NumberOfLikes { get; set; }
    }
}
