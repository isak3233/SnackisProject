
using Snackis.Domain.DTO;
using Snackis.Domain.BlazorInterfaces;

using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;


namespace Snackis.Infrastructure.BlazorRepositories
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly HttpClient _http;
        public SubjectRepository(IHttpClientFactory factory)
        {
            _http = factory.CreateClient("SnackisApi");
        }
        public async Task<List<SubjectDto>?> GetSubjectsAsync()
        {
            var subjects = await _http.GetFromJsonAsync<List<SubjectDto>>($"subject");
            return subjects;
        }
        public async Task<SubSubjectDto?> GetSubSubjectByIdAsync(int id)
        {
            var subject = await _http.GetFromJsonAsync<SubSubjectDto>($"subsubject/{id}");
            return subject;
        }
    }
}
