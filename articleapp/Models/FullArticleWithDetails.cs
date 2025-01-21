using System.Text.Json.Serialization;

namespace articleapp.Models
{
    public class FullArticleWithDetails
    {
        [JsonPropertyName("articleId")]
        public int ArticleId { get; set; }

        [JsonPropertyName("articleTitle")]
        public string ArticleTitle { get; set; }

        [JsonPropertyName("articleContent")]
        public string ArticleContent { get; set; }

        [JsonPropertyName("articleCreatedAt")]
        public DateTime ArticleCreatedAt { get; set; }

        [JsonPropertyName("numberOfComments")]
        public int NumberOfComments { get; set; }

        [JsonPropertyName("numberOfLikes")]
        public int NumberOfLikes { get; set; }

        [JsonPropertyName("categoryName")]
        public string CategoryName { get; set; }

        [JsonPropertyName("userName")]
        public string UserName { get; set; }

        [JsonPropertyName("userId")]
        public int UserId { get; set; }

        [JsonPropertyName("comments")]
        public List<CommentsWithDetails> Comments { get; set; }
    }

   
}
