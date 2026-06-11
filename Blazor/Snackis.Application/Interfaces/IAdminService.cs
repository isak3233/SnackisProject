using Snackis.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snackis.Application.BlazorInterfaces
{
    public interface IAdminService
    {
        Task<List<SubjectDto>?> GetSubjectsAsync();

        Task CreateSubjectAsync(string name, string userId);

        Task UpdateSubjectAsync(int id, string name, string userId);

        Task CreateSubSubjectAsync(int subjectId, string name, string userId);

        Task UpdateSubSubjectAsync(int id, string name, string userId);

        Task<List<ReportDto>?> GetReportsAsync(string userId);

        Task SolveReportAsync(int reportId, string userId);

        Task DeletePostAsync(int postId, bool removePost, bool removeTextContent, string userId);

        Task DeleteCommentAsync(int commentId, string userId);
    }
}
