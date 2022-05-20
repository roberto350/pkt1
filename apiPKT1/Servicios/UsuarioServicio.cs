using System.Security.Claims;

namespace apiPKT1.Servicios
{
    public class UsuarioServicio : IUsuarioServicio
    {
        
        private readonly IHttpContextAccessor _contAcc;
        public UsuarioServicio(IHttpContextAccessor httpContAcce)
        {
            _contAcc = httpContAcce;
        }

        public string MiNombre()
        {
            var result = string.Empty;

            if( _contAcc.HttpContext != null)
            {
                result = _contAcc.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            }
            return result;
        }


    }
}
