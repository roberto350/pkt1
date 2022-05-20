using Microsoft.EntityFrameworkCore;

namespace apiPKT1.Datos
{
    public class ContextoUsuario: DbContext
    {
        public ContextoUsuario(DbContextOptions<ContextoUsuario> options) : base(options) { }

        public DbSet<DatoUsuario> DatosUsuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Sucursal> Sucursales { get; set; }
    }
}
