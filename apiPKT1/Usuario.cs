namespace apiPKT1
{
    public class Usuario
    {
        public string Usuari { get; set; } = string.Empty;
        public byte[] PaHash { get; set; }
        public byte[] PaSalt { get; set; }
        public string ActToken { get; set; }
        public DateTime Creado { get; set; }
        public DateTime Expirado { get; set; }

    }
}
