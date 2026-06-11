using System;
using System.Collections.Generic;
using System.Text;

namespace Snackis.Domain.DTO
{
    public class DeletePostDto
    {
        public bool RemovePost { get; set; }
        public bool RemoveTextContent { get; set; }
    }
}
