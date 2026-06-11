using Snackis.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snackis.Application.Interfaces
{
    public interface ISubjectService
    {
        Task<List<SubjectDto>> GetSubjectsAsync();
        Task<SubSubjectDto> GetSubSubjectAsync(int subSubjectId);
        Task CreateSubjectAsync(CreateSubjectDto subjectDto);
        Task<bool> UpdateSubjectAsync(int id, UpdateSubjectDto subjectDto);
        Task<bool> CreateSubSubjectAsync(int subjectId, CreateSubSubjectDto subSubjectDto);
        Task<bool> UpdateSubSubjectAsync(int id, UpdateSubSubjectDto subSubjectDto);
    }
}
