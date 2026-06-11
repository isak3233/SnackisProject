using System;
using System.Collections.Generic;
using System.Text;

namespace Snackis.Application.DTO
{
    public class GetPostCommentDto
    {
        public int PostId { get; set; }
        public int Start { get; set; } = 0;
        public int Count { get; set; } = 10;
    }
}
