using System;
using System.Collections.Generic;

namespace articleapp.Models;

public partial class ArticleModel
{
    public int ArticleId { get; set; }
    public string ArticleTitle { get; set; } 
    public string ArticleContent { get; set; }
    public DateTime ArticleCreatedAt { get; set; } 

    public int UserId { get; set; } 
    public int CategoryId { get; set; }






}
