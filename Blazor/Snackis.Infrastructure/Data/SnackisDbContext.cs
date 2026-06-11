using Snackis.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace Snackis.Infrastructure.Data
{
    public class SnackisDbContext : IdentityDbContext<SnackisUser>
    {
        public SnackisDbContext(DbContextOptions<SnackisDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<SnackisPostComment>()
                .HasOne(x => x.SnackisPost)
                .WithMany(x => x.Comments)
                .HasForeignKey(x => x.SnackisPostId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<SnackisPostComment>()
                .HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<SnackisMessage>()
                .HasOne(m => m.ReceiverUser)
                .WithMany(u => u.ReceivedMessages)
                .HasForeignKey(m => m.ReceiverUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<SnackisMessage>()
                .HasOne(m => m.SenderUser)
                .WithMany(u => u.SentMessages)
                .HasForeignKey(m => m.SenderUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        public DbSet<SnackisSubject> SnackisSubjects => Set<SnackisSubject>();
        public DbSet<SnackisSubSubject> SnackisSubSubjects => Set<SnackisSubSubject>();
        public DbSet<SnackisPost> SnackisPosts => Set<SnackisPost>();
        public DbSet<SnackisPostComment> SnackisPostComments => Set<SnackisPostComment>();
        public DbSet<SnackisMessage> SnackisMessages => Set<SnackisMessage>();
        public DbSet<SnackisReport> SnackisReports => Set<SnackisReport>();
    }
}

