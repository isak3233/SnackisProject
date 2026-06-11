
using Microsoft.AspNetCore.Identity;

namespace Snackis.Domain.Entities
{
    public class SnackisUser : IdentityUser
    {
        public string AvatarUrl { get; set; }
        public string DisplayName { get; set; }
        public ICollection<SnackisMessage> SentMessages { get; set; }
        public ICollection<SnackisMessage> ReceivedMessages { get; set; }
    }
}
