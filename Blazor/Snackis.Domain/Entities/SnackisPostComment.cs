using System;
using System.Collections.Generic;
using System.Text;

namespace Snackis.Domain.Entities
{
    public class SnackisPostComment
    {
        public int Id { get; set; }
        public string TextContent { get; set; }
        public string UserId { get; set; }
        public SnackisUser User { get; set; }
        public DateTime CreatedAt { get; set; }
        public int SnackisPostId { get; set; }
        public SnackisPost SnackisPost { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<SnackisReport> Reports { get; set; } = new List<SnackisReport>();
    }
}
