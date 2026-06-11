using System;
using System.Collections.Generic;
using System.Text;

namespace Snackis.Domain.Entities
{
    public class SnackisPost
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string TextContent { get; set; } 
        public DateTime CreatedAt { get; set; }
        public string UserId { get; set; }
        public SnackisUser User { get; set; }
        public int SnackisSubSubjectId { get; set; }
        public SnackisSubSubject SnackisSubSubject { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsTextDeleted { get; set; }
        public ICollection<SnackisPostComment> Comments { get; set; } = new List<SnackisPostComment>();
        public ICollection<SnackisReport> Reports { get; set; } =  new List<SnackisReport>();
    }
}
