
using Snackis.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snackis.Domain.BlazorInterfaces
{
    public interface ISubjectRepository
    {
        Task<List<SubjectDto>?> GetSubjectsAsync();
        Task<SubSubjectDto?> GetSubSubjectByIdAsync(int id);
    }
}
