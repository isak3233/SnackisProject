using Snackis.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snackis.Application.DTO
{
    public class SubjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<SubSubjectDto> SubSubjects { get; set; } = [];
    }
}
