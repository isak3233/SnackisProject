using Snackis.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snackis.Domain.BlazorInterfaces
{
    public interface IPostRepository
    {
        Task<PostDto?> GetPostAsync(int postId);
        Task<List<PostDto>?> GetPostFromSubSubjectAsync(int subsubjectId, int start, int count);
        Task CreatePostAsync(CreatePostDto createPostDto, string userId);
        Task<List<PostCommentDto>?> GetPostCommentsAsync(int postId, int start, int count);
        Task CreatePostCommentAsync(CreatePostCommentDto postCommentDto, string userId);
        Task CreateReportAsync(CreateReportDto createReportDto);
    }
}
