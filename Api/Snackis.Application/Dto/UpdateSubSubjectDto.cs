using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Snackis.Application.DTO
{
    public class UpdateSubSubjectDto
    {
        [Required]
        public string Name { get; set; }
    }
}
