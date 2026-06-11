using System;
using System.Collections.Generic;
using System.Text;

namespace Snackis.Domain.Entities
{
    public class SnackisMessage
    {
        public int Id { get; set; }
        public string ReceiverUserId { get; set; }
        public SnackisUser ReceiverUser { get; set; }
        public string SenderUserId { get; set; }
        public SnackisUser SenderUser { get; set; }

        public DateTime SentAt { get; set; }

        public string TextContent { get; set; }
    }
}
