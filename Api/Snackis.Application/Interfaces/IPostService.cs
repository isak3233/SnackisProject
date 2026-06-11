using Snackis.Application.DTO;
using Snackis.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snackis.Application.Interfaces
{
    public interface IPostService
    {
        Task<PostDto> GetPostAsync(int postId);
        Task<List<PostDto>> GetPostsAsync(int subSubjectId, int start, int count);
        Task CreatePostAsync(CreatePostDto postDto, string userId);
        Task DeletePostAsync(DeletePostDto deletePostDto, int postId);
        Task<List<PostCommentDto>> GetPostCommentsAsync(int postId, int start, int count);
        Task<bool> CreatePostCommentAsync(CreatePostCommentDto postCommentDto, string userId);
        Task DeletePostCommentAsync(int postCommentId);

        Task<List<ReportDto>> GetReportsAsync(int count, bool solved);
        Task CreateReportAsync(CreateReportDto reportDto);
        Task UpdateReportAsync(UpdateReportDto updateReportDto);
    }
}
