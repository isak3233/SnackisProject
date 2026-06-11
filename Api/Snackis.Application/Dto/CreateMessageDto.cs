using System;
using System.Collections.Generic;
using System.Text;

namespace Snackis.Application.DTO
{
    public class CreateMessageDto
    {
        public string ReceiverUserId { get; set; }
        public string TextContent { get; set; }
    }
}
