using System;

namespace Blog.DAL.DTO;

public class PostsOrderedByLastCommentDate
{
    public string Post { get; set; }
    
    public LastComment LastComment { get; set; }
}