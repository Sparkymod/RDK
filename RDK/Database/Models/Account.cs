using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RDK.Database.Core;

namespace RDK.Database.Models
{
    public class Account : IEntity
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public DateTime CreationTime { get; set; }
        public string SecretQuestion { get; set; }
        public string SecretAnswer { get; set; }
        public DateTime LastConnection { get; set; }


        public static void Build(EntityTypeBuilder<Account> entity)
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Username).IsUnique();
            entity.HasIndex(e => e.Email).IsUnique();
            // Properties
            entity.Property(e => e.Id).IsRequired();
            entity.Property(e => e.Username).IsRequired().HasMaxLength(30);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(50);
            entity.Property(e => e.PasswordHash).IsRequired().HasMaxLength(255);
            entity.Property(e => e.SecretQuestion).HasMaxLength(255);
            entity.Property(e => e.SecretAnswer).HasMaxLength(255);
            entity.Property(e => e.LastConnection).ValueGeneratedOnAdd();
            entity.Property(e => e.CreationTime).ValueGeneratedOnAdd();
        }
    }
}