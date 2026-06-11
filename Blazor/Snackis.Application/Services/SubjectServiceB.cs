using Snackis.Application.BlazorInterfaces;
using Snackis.Domain.DTO;
using Snackis.Domain.BlazorInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snackis.Application.BlazorServices
{
    public class SubjectServiceB : ISubjectService
    {
        private readonly ISubjectRepository _subjectService;
        public SubjectServiceB(ISubjectRepository subjectService)
        {
            _subjectService = subjectService;
        }
        public async Task<List<SubjectDto>?> GetSubjectsAsync()
        {
            var subjects = await _subjectService.GetSubjectsAsync();
            return subjects;
        }
        public async Task<SubSubjectDto?> GetSubSubjectByIdAsync(int id)
        {
            var subject = await _subjectService.GetSubSubjectByIdAsync(id);
            return subject;
        }
    }
}
