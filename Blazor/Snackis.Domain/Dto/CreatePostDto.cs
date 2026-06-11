using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Snackis.Domain.DTO
{
    public class CreatePostDto
    {
        [Required(ErrorMessage = "Rubrik måste anges")]
        public string Header { get; set; } = string.Empty;

        [Required(ErrorMessage = "Innehåll måste anges")]
        public string TextContent { get; set; } = string.Empty;

        public int SubSubjectId { get; set; }
    }
}
