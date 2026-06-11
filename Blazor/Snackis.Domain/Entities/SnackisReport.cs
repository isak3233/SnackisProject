using System;
using System.Collections.Generic;
using System.Text;

namespace Snackis.Domain.Entities
{
    public class SnackisReport
    {
        public int Id { get; set; }
        public int? PostId { get; set; }
        public SnackisPost? Post { get; set; }
        public int? PostCommentId { get; set; }
        public SnackisPostComment? PostComment { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Solved { get; set; }
    }
}
