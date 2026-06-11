using Snackis.Domain.BlazorInterfaces;
using Snackis.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;

namespace Snackis.Infrastructure.BlazorRepositories
{
    public class PostRepository : IPostRepository
    {
        private readonly HttpClient _http;
        public PostRepository(IHttpClientFactory factory)
        {
            _http = factory.CreateClient("SnackisApi");
        }
        public async Task<PostDto?> GetPostAsync(int postId)
        {
            var post = await _http.GetFromJsonAsync<PostDto>($"post?postId={postId}");
            return post;
        }
        public async Task<List<PostDto>?>GetPostFromSubSubjectAsync(int subsubjectId, int start, int count)
        {
            var posts = await _http.GetFromJsonAsync<List<PostDto>>($"posts?subSubjectId={subsubjectId}&start={start}&count={count}");
            return posts;
        }
        public async Task CreatePostAsync(CreatePostDto createPostDto, string userId)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "post");

            request.Headers.Add("User-Id", userId);

            request.Content = JsonContent.Create(createPostDto);

            var response = await _http.SendAsync(request);

            response.EnsureSuccessStatusCode();
        }
        public async Task<List<PostCommentDto>?> GetPostCommentsAsync(int postId, int start, int count)
        {
            var postComments = await _http.GetFromJsonAsync<List<PostCommentDto?>>($"post/postcomments?postid={postId}&start={start}&count={count}");
            return postComments;
        }
        public async Task CreatePostCommentAsync(CreatePostCommentDto postCommentDto, string userId)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "post/postcomment");

            request.Headers.Add("User-Id", userId);

            request.Content = JsonContent.Create(postCommentDto);

            var response = await _http.SendAsync(request);

            response.EnsureSuccessStatusCode();
        }
        public async Task CreateReportAsync(CreateReportDto createReportDto)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "post/report");

            request.Content = JsonContent.Create(createReportDto);

            var response = await _http.SendAsync(request);

            response.EnsureSuccessStatusCode();
        }
    }
}
