using Snackis.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snackis.Application.BlazorInterfaces
{
    public interface IPostService
    {
        Task<PostDto?> GetPostAsync(int postId);
        Task<List<PostDto>?> GetPostsAsync(int subSubjectId, int start, int count);
        Task CreatePostAsync(CreatePostDto createPostDto, string userId);
        Task<List<PostCommentDto>?> GetPostCommentsAsync(int postId, int start, int count);
        Task CreatePostCommentAsync(int postId, string textContent, string userId);
        Task CreateReportAsync(int? postId, int? postCommentId, string comment);
    }
}
