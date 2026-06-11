using System;
using System.Collections.Generic;
using System.Text;

namespace Snackis.Domain.Entities
{
    public class SnackisSubSubject
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int SnackisSubjectId { get; set; }

        public SnackisSubject SnackisSubject { get; set; }

        public ICollection<SnackisPost> Posts { get; set; } = new List<SnackisPost>();
    }
}
