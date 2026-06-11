using System;
using System.Collections.Generic;
using System.Text;

namespace Snackis.Domain.Entities
{
    public class SnackisSubject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<SnackisSubSubject> SubSubjects { get; set; } = new List<SnackisSubSubject>();
    }
}
