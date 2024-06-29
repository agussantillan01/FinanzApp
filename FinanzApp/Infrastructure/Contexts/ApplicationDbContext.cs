using Infrastructure.Configuration;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DBConnection
{
    public class ApplicationDbContext : DbContext
    {
        private readonly ILoggerFactory logger;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ILoggerFactory logger) : base(options)
        {
            this.logger = logger;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

        }


        #region DataSets
        public DbSet<UsuarioLogin> Usuarios { get; set; }
        public DbSet<Permiso> Permisos { get; set; }
        public DbSet<PermisoGrupo> PermisosGrupo { get; set; }
        public DbSet<PermisoUsuario> PermisosUsuario { get; set; }
        #endregion


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseLoggerFactory(logger);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //llamo a los Configurations
            modelBuilder.ApplyConfiguration(new UsuarioLoginConfiguration());
        }

    }
}
