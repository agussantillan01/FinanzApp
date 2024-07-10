using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class UsuarioLoginConfiguration : IEntityTypeConfiguration<UsuarioLogin>
    {
        public void Configure(EntityTypeBuilder<UsuarioLogin> builder)
        {
            builder.ToTable("Usuarios");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nombre).HasColumnName("Nombre").HasColumnType("varchar").HasMaxLength(500).IsRequired();
            builder.Property(x => x.Apellido).HasColumnName("Apellido").HasColumnType("varchar").HasMaxLength(500).IsRequired();
            builder.Property(x => x.PasswordHash).HasColumnName("PasswordHash").HasColumnType("varchar").IsRequired();
            builder.Property(x => x.UserName).HasColumnName("UserName").HasMaxLength(500).HasColumnType("varchar").IsRequired();
            builder.Property(x => x.NormalizedUserName).HasColumnName("NormalizedUserName").HasColumnType("varchar").HasMaxLength(500).IsRequired();
            builder.Property(x => x.ConcurrencyStamp).HasColumnName("ConcurrencyStamp").HasColumnType("varchar").IsRequired();
            builder.Property(x => x.Email).HasColumnName("Email").HasMaxLength(500).HasColumnType("varchar").IsRequired();
            builder.Property(x => x.NormalizedEmail).HasColumnName("NormalizedEmail").HasColumnType("varchar").HasMaxLength(500).IsRequired();
            builder.Property(x => x.SecurityStamp).HasColumnName("SecurityStamp").HasColumnType("varchar").IsRequired(false);
            builder.Property(x => x.EsUserSistema).HasColumnName("EsUserAdmin").HasColumnType("bit").IsRequired();
       

            //builder.Ignore(c => c.NormalizedUserName);
            //builder.Ignore(c => c.AccessFailedCount);
            ////builder.Ignore(c => c.ConcurrencyStamp);

            builder.Ignore(c => c.EmailConfirmed);
            builder.Ignore(c => c.LockoutEnabled);
            builder.Ignore(c => c.LockoutEnd);

            //builder.Ignore(c => c.NormalizedEmail);
            builder.Ignore(c => c.PhoneNumber);
            builder.Ignore(c => c.PhoneNumberConfirmed);
            ////builder.Ignore(c => c.SecurityStamp);
            builder.Ignore(c => c.TwoFactorEnabled);
            builder.Ignore(c => c.AccessFailedCount);
            //builder.Ignore(c => c.Pass);
        }
    }
}
