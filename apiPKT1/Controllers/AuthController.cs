using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;


namespace apiPKT1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

       public static Usuario usuario = new Usuario();
       private readonly IConfiguration _configuration;
        private readonly IUsuarioServicio _usuarioServicio;

        public AuthController(IConfiguration configuration, IUsuarioServicio usuarioServicio)
        {
            _configuration = configuration;
            _usuarioServicio = usuarioServicio;
        }

        [HttpGet, Authorize]
        public ActionResult<object> GetMe()
        {
           var usuar = _usuarioServicio.MiNombre();
            return Ok(usuar);
        }


        //-------------------------  Metodo Post de registro -------------------------
        [HttpPost("registrar")]
        public async Task<ActionResult<Usuario>> Registrar(RegistrarUsuario req) {

            CrearHash(req.Clave, out byte[] passHash, out byte[] passSalt);

            usuario.Usuari = req.Usuario;
            usuario.PaHash = passHash;
            usuario.PaSalt = passSalt;

            return base.Ok(usuario);
        }


        //------------------------- Fin Metodo -------------------------


        //------------------------- Metodo login de usuario -------------------------
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(RegistrarUsuario req)
        {
            if(usuario.Usuari != req.Usuario)
            {
                return BadRequest("usuario no encontrado");
            }

            if (!VerHash(req.Clave, usuario.PaHash, usuario.PaSalt))
            {
                return BadRequest("error en la contraseña.");
            }

            string token = CrearToken(usuario);

            var actToken = GenTokenAct();
            SetActToken(actToken);

            return Ok(token);
        }


        //------------------------- Fin del metodo -------------------------


        //------------------------- Metodo Post de Actualizar Token -------------------------
        [HttpPost("actToken")]
        public async Task<ActionResult<string>> ActuaToken()
        {
            var actToken = Request.Cookies["actualizarToken"];

            if (!usuario.ActToken.Equals(actToken)){
                return Unauthorized("Token nuevo invalido");
            }
            else if(usuario.Expirado < DateTime.Now){
                return Unauthorized("Token Expirado");
            }

            string token = CrearToken(usuario);
            var nActToken = GenTokenAct();
            SetActToken(nActToken);

            return Ok(token);
        }
        //------------------------- Fin del metodo -------------------------


        //------------------------- Metodo crear Token -------------------------
        private string CrearToken(Usuario usua)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usua.Usuari),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
               _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        //------------------------- Metodo para general un nuevo token -------------------------
        private ActualizarToken GenTokenAct()
        {
            var actToken = new ActualizarToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expirado = DateTime.Now.AddDays(7),
                Creado = DateTime.Now.AddDays(7)

            };
            return actToken;

        }

        //------------------------- Fin del metodo -------------------------


        //------------------------- Metodo Obtener el Token Actualizado -------------------------
        private void SetActToken( ActualizarToken nActToken) {
            var cookieOPtions = new CookieOptions
            {
                HttpOnly = true,
                Expires = nActToken.Expirado
            };
            Response.Cookies.Append("actualizarToken", nActToken.Token, cookieOPtions);
            usuario.ActToken = nActToken.Token;
            usuario.Creado = nActToken.Creado;
            usuario.Expirado = nActToken.Expirado;

        }

        //------------------------- Fin del metodo -------------------------


       

        //------------------------- Metodo de Creacion Hash -------------------------
        private void CrearHash(string clave, out byte[] passHash, out byte[] passSalt)
        {
            using( var hmac= new HMACSHA512())
            {
                passSalt = hmac.Key;
                passHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(clave));
            }
        }
        //------------------------- Fin Metodo -------------------------


        //------------------------- Metodo de verificacion de Hash -------------------------
        private bool VerHash(string pass, byte[] passHash, byte[] passSalt)
        {
            using (var hmac = new HMACSHA512(passSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(pass));
                return computedHash.SequenceEqual(passHash);
            }
        }
        //------------------------- Fin del metodo -------------------------


    }
}
