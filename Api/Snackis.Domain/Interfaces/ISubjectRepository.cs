using Snackis.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snackis.Domain.Interfaces
{
    public interface ISubjectRepository
    {
        Task<List<SnackisSubject>> GetSubjectsAsync();
        Task<SnackisSubSubject?> GetSubSubjectAsync(int subSubjectId);
        Task CreateSubjectAsync(SnackisSubject subject);
        Task<bool> UpdateSubjectAsync(int id, SnackisSubject updatedSubject);
        Task<bool> CreateSubSubjectAsync(SnackisSubSubject subSubject);
        Task<bool> UpdateSubSubjectAsync(int id, SnackisSubSubject updatedSubSubject);

    }
}
