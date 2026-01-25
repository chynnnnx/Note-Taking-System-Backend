using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstAngular.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FirstAngular.Persistence.Configurations
{
    public class NoteEntityConfiguration: IEntityTypeConfiguration<NoteEntity>
    {
        public void Configure(EntityTypeBuilder<NoteEntity> builder)
        {
            builder.HasKey(n => n.Id);
            builder.Property(n=>n.UserId)
                .IsRequired()
                .HasMaxLength(450);
            builder.Property(n => n.Title)
                .IsRequired()
                .HasMaxLength(300);
            builder.Property(n => n.Content)
                .IsRequired();
            builder.Property(n=>n.IsPinned)
                 .HasDefaultValue(false);
            builder.Property(n => n.IsFavorite)
                .HasDefaultValue(false);
            builder.Property(n=>n.IsArchived)
                .HasDefaultValue(false);
            builder.Property(n => n.CreatedAt)
                .IsRequired()
            .HasDefaultValueSql("GETUTCDATE()");
            builder.Property(n => n.UpdatedAt)
                .IsRequired()
            .HasDefaultValueSql("GETUTCDATE()");

        }
    }
}
