using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Snackis.Application.DTO
{
    public class CreatePostDto
    {
        [Required]
        public string Header { get; set; }
        [Required]
        public string TextContent { get; set; }
        [Required]
        public int SubSubjectId { get; set; }
    }
}
