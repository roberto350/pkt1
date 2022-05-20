namespace apiPKT1
{
    public class DatoUsuario
    {
        public int Id { get; set; }
        public string Usuario { get; set; } = String.Empty;
        public string Nombre { get; set; } = String.Empty;
        public string Apellidos { get; set; } = String.Empty;
        public int IdSucursal { get; set; }
        public int IdRol { get; set; }
    }
}
