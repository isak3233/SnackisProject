using Snackis.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snackis.Application.BlazorInterfaces
{
    public interface ISubjectService
    {
        Task<List<SubjectDto>?> GetSubjectsAsync();
        Task<SubSubjectDto?> GetSubSubjectByIdAsync(int id);
    }
}
