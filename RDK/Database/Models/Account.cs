using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RDK.Database.Core;

namespace RDK.Database.Models
{
    public class Account : IEntity
    {
        [Key][Required] public int Id { get; set; }
        [Required] public string Username { get; set; }
        [Required] public string PasswordHash { get; set; }
        [Required] public string Email { get; set; }
        [Timestamp] public DateTime CreationTime { get; set; }

        public static void Build(EntityTypeBuilder<Account> entity)
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Username).IsUnique();
            entity.Property(e => e.Username).IsRequired().HasMaxLength(25);
            entity.HasIndex(e => e.Email).IsUnique();
            entity.Property(e => e.Email).IsRequired().HasMaxLength(50);
            entity.Property(e => e.PasswordHash).IsRequired().HasMaxLength(255);
            entity.Property(e => e.CreationTime).ValueGeneratedOnAdd();
        }
    }
}