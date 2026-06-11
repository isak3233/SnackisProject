using Snackis.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snackis.Domain.Interfaces
{
    public interface IPostRepository 
    {
        Task<SnackisPost?> GetPostByIdAsync(int postId);
        Task<List<SnackisPost>> GetPostsAsync(int subSubjectId, int start, int count);
        Task CreatePostAsync(SnackisPost post);
        Task<SnackisPostComment?> GetPostCommentByIdAsync(int postCommentId);
        Task<List<SnackisPostComment>> GetPostCommentsAsync(int postId, int start, int count);
        Task<bool> CreatePostCommentAsync(SnackisPostComment postComment);
        Task<SnackisReport?> GetReportByIdAsync(int reportId);
        Task<List<SnackisReport>> GetReportsFromPostAsync(int postId);
        Task<List<SnackisReport>> GetReportsFromPostCommentAsync(int postCommentId);
        Task<List<SnackisReport>> GetReportsAsync(int count, bool solved);
        Task CreateReportAsync(SnackisReport report);
        Task SaveChangesAsync();

    }
}
