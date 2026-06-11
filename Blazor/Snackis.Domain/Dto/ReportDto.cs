using Snackis.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snackis.Domain.DTO
{
    public class ReportDto
    {
        public int ReportId { get; set; }
        public int? PostId {  get; set; }
        public int? PostCommentId { get; set; }

        public string? ReportedHeader { get; set; }
        public string ReportedText { get; set; }

        public string ReportedUserId { get; set; }
        public string ReportedUserDisplayName { get; set; }
        public string ReportedUserAvatarUrl { get; set; }

        public string? Comment { get; set; }
        public bool Solved  { get; set; }
    }
}
