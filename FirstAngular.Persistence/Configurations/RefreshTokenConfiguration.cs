using FirstAngular.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace FirstAngular.Persistence.Configurations
{
    public class RefreshTokenConfiguration: IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure (EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasKey(rt => rt.Id);

            builder.Property(rt => rt.TokenHash)
                .IsRequired()
                .HasMaxLength(500);
            builder.Property(rt => rt.UserId)
                .IsRequired()
                 .HasMaxLength(450);
            builder.Property(rt => rt.Expiration)
                .IsRequired();
            builder.Property(rt => rt.IsRevoked)
                .IsRequired();
            builder.Ignore(rt => rt.IsActive);


        }
    }
}
