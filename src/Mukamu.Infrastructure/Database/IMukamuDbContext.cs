using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mukamu.Core.Models;

namespace Mukamu.Infrastructure.Database
{
    public interface IMukamuDbContext : IDisposable
    {
        DbSet<Attachment> Attachments { get; set; }
        DbSet<Comment> Comments { get; set; }
        DbSet<Conversation> Conversations { get; set; }
        DbSet<Message> Messages { get; set; }
        DbSet<Post> Posts { get; set; }
        DbSet<User> Users { get; set; }
        Task<int> SaveChangesAsync(System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
    }
}
