using Microsoft.EntityFrameworkCore;
using Snackis.Domain.Entities;
using Snackis.Domain.Interfaces;
using Snackis.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snackis.Infrastructure.Repositories
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly SnackisDbContext _dbContext;
        public SubjectRepository(SnackisDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<SnackisSubject>> GetSubjectsAsync()
        {
            return await _dbContext.SnackisSubjects
                .Include(x => x.SubSubjects)
                .ToListAsync();
        }
        public async Task<SnackisSubSubject?> GetSubSubjectAsync(int subSubjectId)
        {
            return await _dbContext.SnackisSubSubjects.Where(x => x.Id == subSubjectId).FirstOrDefaultAsync();
        }
        public async Task CreateSubjectAsync(SnackisSubject subject)
        {
            _dbContext.SnackisSubjects.Add(subject);
            await _dbContext.SaveChangesAsync();

        }
        public async Task<bool> UpdateSubjectAsync(int id, SnackisSubject updatedSubject)
        {
            var subject = await _dbContext.SnackisSubjects.FirstOrDefaultAsync(x => x.Id == id);
            if (subject == null) return false;
            subject.Name = updatedSubject.Name;

            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> CreateSubSubjectAsync(SnackisSubSubject subSubject)
        {
            var subject = await _dbContext.SnackisSubjects.FirstOrDefaultAsync(x => x.Id == subSubject.SnackisSubjectId);
            if(subject == null) return false;
            _dbContext.SnackisSubSubjects.Add(subSubject);
            await _dbContext.SaveChangesAsync();
            return true;

        }
        public async Task<bool> UpdateSubSubjectAsync(int id, SnackisSubSubject updatedSubSubject)
        {
            var subSubject = await _dbContext.SnackisSubSubjects.FirstOrDefaultAsync(x => x.Id == id);
            if (subSubject == null) return false;
            subSubject.Name = updatedSubSubject.Name;

            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
