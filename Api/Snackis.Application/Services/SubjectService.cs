using Snackis.Application.DTO;
using Snackis.Application.Interfaces;
using Snackis.Domain.Entities;
using Snackis.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snackis.Application.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;
        public SubjectService(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }
        public async Task<List<SubjectDto>> GetSubjectsAsync()
        {
            var subjects = await _subjectRepository.GetSubjectsAsync();
            var returnSubjects = subjects.Select(x => new SubjectDto
            {
                Id = x.Id,
                Name = x.Name,

                SubSubjects = x.SubSubjects
                    .Select(s => new SubSubjectDto
                    {
                        Id = s.Id,
                        Name = s.Name
                    })
                    .ToList()
            }).ToList();
            return returnSubjects;
        }
        public async Task<SubSubjectDto> GetSubSubjectAsync(int subSubjectId)
        {
            var subSubject = await _subjectRepository.GetSubSubjectAsync(subSubjectId);
            if (subSubject == null) throw new Exception("SubSubject not found");
            var subSubjectDto = new SubSubjectDto
            {
                Id = subSubject.Id,
                Name = subSubject.Name,
            };
            return subSubjectDto;
        }
        public async Task CreateSubjectAsync(CreateSubjectDto subjectDto)
        {
            var subject = new SnackisSubject
            {
                Name = subjectDto.Name
            };
            await _subjectRepository.CreateSubjectAsync(subject);
        }
        public async Task<bool> UpdateSubjectAsync(int id, UpdateSubjectDto subjectDto)
        {
            var subject = new SnackisSubject
            {
                Name = subjectDto.Name,
            };
            var result = await _subjectRepository.UpdateSubjectAsync(id, subject);
            return result;
        }
        public async Task<bool> CreateSubSubjectAsync(int subjectId, CreateSubSubjectDto subSubjectDto)
        {
            var subSubject = new SnackisSubSubject
            {
                Name = subSubjectDto.Name,
                SnackisSubjectId = subjectId,
            };
            var result = await _subjectRepository.CreateSubSubjectAsync(subSubject);
            return result;

        }
        public async Task<bool> UpdateSubSubjectAsync(int id, UpdateSubSubjectDto subSubjectDto)
        {
            var subject = new SnackisSubSubject
            {
                Name = subSubjectDto.Name,
            };
            var result = await _subjectRepository.UpdateSubSubjectAsync(id, subject);
            return result;
        }
    }
}
