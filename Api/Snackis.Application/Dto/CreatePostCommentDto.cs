using Snackis.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Snackis.Application.DTO
{
    public class CreatePostCommentDto
    {
        [Required]
        public string TextContent { get; set; }
        [Required]
        public int SnackisPostId { get; set; }
    }
}
