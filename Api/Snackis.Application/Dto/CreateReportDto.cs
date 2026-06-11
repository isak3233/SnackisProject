using System;
using System.Collections.Generic;
using System.Text;

namespace Snackis.Application.DTO
{
    public class CreateReportDto
    {
        public int? PostId { get; set; }
        public int? PostCommentId { get; set; }
        public string? Comment { get; set; }
    }
}
