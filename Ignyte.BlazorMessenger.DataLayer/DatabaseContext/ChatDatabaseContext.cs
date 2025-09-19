using Ignyte.BlazorMessenger.DataLayer.DatabaseContext.Configurations;
using Ignyte.BlazorMessenger.DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace Ignyte.BlazorMessenger.DataLayer.DatabaseContext;

public class ChatDatabaseContext(DbContextOptions<ChatDatabaseContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<ChatRoom> ChatRooms { get; set; } = null!;
    public DbSet<Message> Messages { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // User Configuration

        modelBuilder.Entity<User>(builder =>
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.DisplayName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(u => u.Email)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(u => u.ProfilePicture)
                   .HasMaxLength(500);

            // Many-to-many: Users <-> ChatRooms
            builder.HasMany<ChatRoom>()
                   .WithMany()
                   .UsingEntity(j => j.ToTable("UserChatRooms"));
        });

        // ChatRoom Configuration
        modelBuilder.Entity<ChatRoom>(builder =>
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(c => c.CreatedById)
                   .IsRequired();

            builder.Property(c => c.CreatedAt)
                   .IsRequired();

            // One-to-many: ChatRoom -> Messages
            builder.HasMany(c => c.Messages)
                   .WithOne()
                   .HasForeignKey(m => m.ChatRoomId)
                   .OnDelete(DeleteBehavior.Cascade);
        });

        // Message Configuration
        modelBuilder.Entity<Message>(builder =>
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Text)
                   .IsRequired()
                   .HasMaxLength(1000);

            builder.Property(m => m.DisplayName)
                   .HasMaxLength(100);

            builder.Property(m => m.NameIdentifier)
                   .HasMaxLength(200);

            builder.Property(m => m.ProfilePicture)
                   .HasMaxLength(500);

            builder.Property(m => m.DateTimeSent)
                   .IsRequired();

            builder.Property(m => m.Status)
                   .HasConversion<string>(); // Store enum as string

            // RELATIONSHIP: Message belongs to User
            builder.HasOne<User>()
                   .WithMany()
                   .HasForeignKey(m => m.UserId)
                   .OnDelete(DeleteBehavior.Restrict);
        });

        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UserConfiguration());

    }
}
