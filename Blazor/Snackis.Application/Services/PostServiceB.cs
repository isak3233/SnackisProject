using Snackis.Domain.DTO;
using Snackis.Domain.BlazorInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

using Snackis.Application.BlazorInterfaces;

namespace Snackis.Application.BlazorServices
{
    public class PostServiceB : IPostService
    {
        private readonly IPostRepository _postRepository;
        public PostServiceB(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }
        public async Task<PostDto?> GetPostAsync(int postId)
        {
            var post = await _postRepository.GetPostAsync(postId);
            return post;
        }
        public async Task<List<PostDto>?> GetPostsAsync(int subSubjectId, int start, int count)
        {
            var posts = await _postRepository.GetPostFromSubSubjectAsync(subSubjectId, start, count);
            return posts;
        }
        public async Task CreatePostAsync(CreatePostDto createPostDto, string userId)
        {
            await _postRepository.CreatePostAsync(createPostDto, userId);
        }
        public async Task<List<PostCommentDto>?> GetPostCommentsAsync(int postId, int start, int count)
        {
            var postComments = await _postRepository.GetPostCommentsAsync(postId, start, count);
            return postComments;
        }
        public async Task CreatePostCommentAsync(int postId, string textContent, string userId)
        {
            var postCommentDto = new CreatePostCommentDto()
            {
                TextContent = textContent,
                SnackisPostId = postId,
            };
            await _postRepository.CreatePostCommentAsync(postCommentDto, userId);
        }
        public async Task CreateReportAsync(int? postId, int? postCommentId, string comment)
        {
            var reportDto = new CreateReportDto()
            {
                PostId = postId,
                PostCommentId = postCommentId,
                Comment = comment,
            };
            await _postRepository.CreateReportAsync(reportDto);
        }
    }
}
