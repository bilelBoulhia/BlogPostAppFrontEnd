using System;
using System.Collections.Generic;

namespace articleapp.Models;

public partial class CommentModel
{
    public int CommentId { get; set; }
    public string CommentContent { get; set; } = string.Empty;
    public int ArticleId { get; set; }
    public int UserId { get; set; }
    public DateTime CommentCreatedAt { get; set; }
    public object? Article { get; set; } 
    public List<object> Reports { get; set; } = new List<object>();
    public object? User { get; set; }
    public List<object> Users { get; set; } = new List<object>();


}
