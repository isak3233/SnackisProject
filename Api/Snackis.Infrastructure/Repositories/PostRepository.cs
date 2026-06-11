using Microsoft.EntityFrameworkCore;
using Snackis.Domain.Entities;
using Snackis.Domain.Interfaces;
using Snackis.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snackis.Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly SnackisDbContext _dbContext;
        public PostRepository(SnackisDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<SnackisPost?> GetPostByIdAsync(int postId)
        {
            var post = await _dbContext.SnackisPosts
                .Where(x => x.IsDeleted == false)
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == postId);

            return post;
        }
        public async Task<List<SnackisPost>> GetPostsAsync(int subSubjectId, int start, int count)
        {
            var posts = await _dbContext.SnackisPosts
                .Include(x => x.User)
                .Where(x => x.SnackisSubSubjectId == subSubjectId && x.IsDeleted == false)
                .OrderByDescending(x => x.CreatedAt)
                .Skip(start)
                .Take(count)
                .ToListAsync();
            return posts;

        }
        public async Task CreatePostAsync(SnackisPost post)
        {
            _dbContext.SnackisPosts.Add(post);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<SnackisPostComment?> GetPostCommentByIdAsync(int postCommentId)
        {
            var postComment = await _dbContext.SnackisPostComments
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == postCommentId);
            return postComment;
        }
        public async Task<List<SnackisPostComment>> GetPostCommentsAsync(int postId, int start, int count)
        {
            var postComments = await _dbContext.SnackisPostComments
                .Include(x => x.User)
                .Where(x => x.SnackisPostId == postId)
                .OrderBy(x => x.CreatedAt)
                .Skip(start)
                .Take(count)
                .ToListAsync();
            return postComments;
                
        }
        public async Task<bool> CreatePostCommentAsync(SnackisPostComment postComment)
        {
            var post = await _dbContext.SnackisPosts.FirstOrDefaultAsync(x => x.Id == postComment.SnackisPostId);
            if (post == null)
            {
                return false;
            }
            _dbContext.SnackisPostComments.Add(postComment);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<SnackisReport?> GetReportByIdAsync(int reportId)
        {
            return await _dbContext.SnackisReports.FirstOrDefaultAsync(x => x.Id == reportId);

        }
        public async Task<List<SnackisReport>> GetReportsFromPostAsync(int postId)
        {
            return await _dbContext.SnackisReports.Where(x => x.PostId == postId).ToListAsync();
        }
        public async Task<List<SnackisReport>> GetReportsFromPostCommentAsync(int postCommentId)
        {
            return await _dbContext.SnackisReports.Where(x => x.PostCommentId == postCommentId).ToListAsync();
        }
        public async Task<List<SnackisReport>> GetReportsAsync(int count, bool solved)
        {
            return await _dbContext.SnackisReports
                .Where(x => x.Solved == solved)
                .Include(x => x.Post)
                    .ThenInclude(p => p.User)
                .Include(x => x.PostComment)
                    .ThenInclude(p => p.User)
                .OrderByDescending(x => x.CreatedAt)
                .Take(count)
                .ToListAsync();
        }
        public async Task CreateReportAsync(SnackisReport report)
        {
            _dbContext.SnackisReports.Add(report);
            await _dbContext.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

    }
}
