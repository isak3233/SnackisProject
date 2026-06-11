using Snackis.Domain.BlazorInterfaces;
using Snackis.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;

namespace Snackis.Infrastructure.BlazorRepositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly HttpClient _http;

        public AdminRepository(IHttpClientFactory factory)
        {
            _http = factory.CreateClient("SnackisApi");
        }

        public async Task<List<SubjectDto>?> GetSubjectsAsync()
        {
            return await _http.GetFromJsonAsync<List<SubjectDto>>("subject");
        }

        public async Task CreateSubjectAsync(string name, string userId)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "subject");

            request.Headers.Add("User-Id", userId);

            request.Content = JsonContent.Create(new CreateSubjectDto
            {
                Name = name
            });

            var response = await _http.SendAsync(request);

            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateSubjectAsync(int id, string name, string userId)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"subject/{id}");

            request.Headers.Add("User-Id", userId);

            request.Content = JsonContent.Create(new UpdateSubjectDto
            {
                Name = name
            });

            var response = await _http.SendAsync(request);

            response.EnsureSuccessStatusCode();
        }

        public async Task CreateSubSubjectAsync(int subjectId, string name, string userId)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"subsubject/{subjectId}");

            request.Headers.Add("User-Id", userId);

            request.Content = JsonContent.Create(new CreateSubSubjectDto
            {
                Name = name
            });

            var response = await _http.SendAsync(request);

            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateSubSubjectAsync(int id, string name, string userId)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"subsubject/{id}");

            request.Headers.Add("User-Id", userId);

            request.Content = JsonContent.Create(new UpdateSubSubjectDto
            {
                Name = name
            });

            var response = await _http.SendAsync(request);

            response.EnsureSuccessStatusCode();
        }

        public async Task<List<ReportDto>?> GetReportsAsync(string userId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "post/report");

            request.Headers.Add("User-Id", userId);

            var response = await _http.SendAsync(request);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<List<ReportDto>>();
        }

        public async Task SolveReportAsync(int reportId, string userId)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, "post/report");

            request.Headers.Add("User-Id", userId);

            request.Content = JsonContent.Create(new UpdateReportDto
            {
                ReportId = reportId,
                Solved = true
            });

            var response = await _http.SendAsync(request);

            response.EnsureSuccessStatusCode();
        }

        public async Task DeletePostAsync(int postId, bool removePost, bool removeTextContent, string userId)
        {
            var request =
                new HttpRequestMessage(HttpMethod.Delete, $"post/{postId}");

            request.Headers.Add("User-Id", userId);

            request.Content = JsonContent.Create(new DeletePostDto
            {
                RemovePost = removePost,
                RemoveTextContent = removeTextContent
            });

            var response = await _http.SendAsync(request);

            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteCommentAsync(int commentId, string userId)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete,
                $"post/postcomment/{commentId}");

            request.Headers.Add("User-Id", userId);

            var response = await _http.SendAsync(request);

            response.EnsureSuccessStatusCode();
        }
    }
}
