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



            //builder.Ignore(c => c.NormalizedUserName);
            //builder.Ignore(c => c.AccessFailedCount);
            ////builder.Ignore(c => c.ConcurrencyStamp);

            builder.Ignore(c => c.EmailConfirmed);
            builder.Ignore(c => c.LockoutEnabled);
            builder.Ignore(c => c.LockoutEnd);

            ////builder.Ignore(c => c.NormalizedEmail);
            builder.Ignore(c => c.PhoneNumber);
            builder.Ignore(c => c.PhoneNumberConfirmed);
            ////builder.Ignore(c => c.SecurityStamp);
            builder.Ignore(c => c.TwoFactorEnabled);
            builder.Ignore(c => c.AccessFailedCount);
            //builder.Ignore(c => c.PasswordHash);
        }
    }
}
