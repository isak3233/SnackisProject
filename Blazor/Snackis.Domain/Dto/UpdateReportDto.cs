using System;
using System.Collections.Generic;
using System.Text;

namespace Snackis.Domain.DTO
{
    public class UpdateReportDto
    {
        public int ReportId { get; set; }
        public bool Solved { get; set; }
    }
}
