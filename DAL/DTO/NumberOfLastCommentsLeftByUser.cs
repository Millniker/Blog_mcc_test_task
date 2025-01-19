using System;

namespace Blog.DAL.DTO;

public class NumberOfLastCommentsLeftByUser
{
    public string User { get; set; }
    
    public int LastCommentCount { get; set; }
}