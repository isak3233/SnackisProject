using System;
using System.Collections.Generic;
using System.Text;

namespace Snackis.Domain.DTO
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string TextContent { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UserId { get; set; }
        public string DisplayName {  get; set; }
        public string AvatarUrl { get; set; }
        public bool IsTextDeleted { get; set; }
    }
}
