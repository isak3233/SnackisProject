using Snackis.Application.BlazorInterfaces;
using Snackis.Domain.BlazorInterfaces;
using Snackis.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snackis.Application.BlazorServices
{
    public class AdminServiceB : IAdminService
    {
        private readonly IAdminRepository _adminRepository;

        public AdminServiceB(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public async Task<List<SubjectDto>?> GetSubjectsAsync()
        {
            var subjects = await _adminRepository.GetSubjectsAsync();
            return subjects;
        }
            

        public async Task CreateSubjectAsync(string name, string userId)
        {
            await _adminRepository.CreateSubjectAsync(name, userId);
        }
            

        public async Task UpdateSubjectAsync(int id, string name, string userId)
        {
            await _adminRepository.UpdateSubjectAsync(id, name, userId);
        }

        public async Task CreateSubSubjectAsync(int subjectId, string name, string userId)
        {
            await _adminRepository.CreateSubSubjectAsync(subjectId, name, userId);
        }
            

        public async Task UpdateSubSubjectAsync(int id, string name, string userId)
        {
            await _adminRepository.UpdateSubSubjectAsync(id, name, userId);
        }
           

        public async Task<List<ReportDto>?> GetReportsAsync(string userId)
        {
            var reports = await _adminRepository.GetReportsAsync(userId);
            return reports;
        }


        public async Task SolveReportAsync(int reportId, string userId)
        {
            await _adminRepository.SolveReportAsync(reportId, userId); 
        }
            

        public async Task DeletePostAsync(int postId, bool removePost, bool removeTextContent, string userId)
        {
            await _adminRepository.DeletePostAsync(postId, removePost, removeTextContent, userId);
        }
            

        public async Task DeleteCommentAsync(int commentId, string userId)
        {
            await _adminRepository.DeleteCommentAsync(commentId, userId);
        }
    }
}
