using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Snackis.Domain.DTO
{
    public class CreateSubSubjectDto
    {
        [Required]
        public string Name { get; set; }
    }
}
