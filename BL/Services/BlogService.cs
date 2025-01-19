using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.DAL.DTO;
using Microsoft.EntityFrameworkCore;

namespace Blog.BL.Services;

public class BlogService
{
    public async Task<List<NumberOfCommentsPerUser>> GetNumberOfCommentsPerUserAsync(MyDbContext context)
    {
        return await context.BlogComments.GroupBy(u => u.UserName).Select(c => new NumberOfCommentsPerUser()
        {
            Name = c.Key,
            CommentCount = c.Count()
        }).ToListAsync();
    }

    public async Task<List<PostsOrderedByLastCommentDate>> GetPostsOrderedByLastCommentDateAsync(MyDbContext context)
    {
        return await context.BlogPosts.Select(p => new PostsOrderedByLastCommentDate {
                Post = p.Title,
                LastComment = p.Comments
                    .OrderByDescending(c => c.CreatedDate)
                    .Select(c => new LastComment
                    {
                        Date = c.CreatedDate,
                        Text = c.Text
                    })
                    .FirstOrDefault()
            }).OrderByDescending(post => post.LastComment.Date)
            .ToListAsync();
    }

    public async Task<List<NumberOfLastCommentsLeftByUser>> GetNumberOfLastCommentsLeftByUserAsync(MyDbContext context)
    {
        return await context.BlogPosts
            .Select(post => post.Comments
                .OrderByDescending(c => c.CreatedDate)
                .FirstOrDefault())
            .Where(lastComment => lastComment != null)
            .GroupBy(lastComment => lastComment.UserName)
            .Select(group => new NumberOfLastCommentsLeftByUser
            {
                User = group.Key,
                LastCommentCount = group.Count()
            })
            .ToListAsync();
    }
}