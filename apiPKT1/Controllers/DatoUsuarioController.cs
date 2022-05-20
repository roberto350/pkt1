using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace apiPKT1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatoUsuarioController : ControllerBase
    {
        private readonly ContextoUsuario _context;

        public DatoUsuarioController(ContextoUsuario context)
        {
            _context = context;
        }

        //-------------------------  Metodo Get -------------------------
        [HttpGet]
        public async Task<ActionResult<List<DatoUsuario>>> ObtDatos()
        {
            return Ok(await _context.DatosUsuarios.ToListAsync());
        }
        //------------------------- Fin Metodo -------------------------


        //-------------------------  Metodo Get ID -------------------------
        [HttpGet("{id}")]
        public async Task<ActionResult<DatoUsuario>> ObtDatosXId(int id)
        {
            var dbDato = await _context.DatosUsuarios.FindAsync(id);
            if (dbDato == null)
                return BadRequest("informacion no registrada");
            return Ok(dbDato);
        }
        //------------------------- Fin Metodo -------------------------

        //-------------------------  Metodo GetIdRol -------------------------
        [HttpGet("roles/{rol}")]
        public async Task<ActionResult<DatoUsuario>> ObtDatosXIdRol(int rol)
        {
            var dbDato = await _context.DatosUsuarios.FindAsync(rol);
            if (dbDato == null)
                return BadRequest("informacion no registrada");
            return Ok(dbDato);
        }
        //------------------------- Fin Metodo -------------------------


        //-------------------------  Metodo Get Rol -------------------------
        [HttpGet("Rol/{id}")]
        public async Task<ActionResult<DatoUsuario>> ObtDatosXRol(int id)
        {
            var dbDato = await _context.DatosUsuarios.FindAsync(id);
            if (dbDato == null)
                return BadRequest("informacion no registrada");
            return Ok(dbDato);
        }
        //------------------------- Fin Metodo -------------------------


        //-------------------------  Metodo Get ID -------------------------
        [HttpGet("sucursal/{id}")]
        public async Task<ActionResult<DatoUsuario>> ObtDatosXSucursal(int id)
        {
            var dbDato = await _context.DatosUsuarios.FindAsync(id);
            if (dbDato == null)
                return BadRequest("informacion no registrada");
            return Ok(dbDato);
        }
        //------------------------- Fin Metodo -------------------------


        //-------------------------  Metodo Post -------------------------
        [HttpPost]
        public async Task<ActionResult<List<DatoUsuario>>> RegDatos(DatoUsuario dato)
        {
            _context.DatosUsuarios.Add(dato);
            await _context.SaveChangesAsync();

            return Ok(await _context.DatosUsuarios.ToListAsync());
        }
        //------------------------- Fin Metodo -------------------------


        //-------------------------  Metodo Put -------------------------
        [HttpPut]
        public async Task<ActionResult<List<DatoUsuario>>> ModDatos(DatoUsuario req)
        {
            var dbDatos = await _context.DatosUsuarios.FindAsync(req.Id);

            if (dbDatos == null)
                return BadRequest("datos no encontrados");
            dbDatos.Usuario = req.Usuario;
            dbDatos.Nombre = req.Nombre;
            dbDatos.Apellidos = req.Apellidos;
            dbDatos.IdSucursal = req.IdSucursal;
            dbDatos.IdRol = req.IdRol;

            await _context.SaveChangesAsync();

            return Ok(await _context.DatosUsuarios.ToListAsync());
        }
        //------------------------- Fin Metodo -------------------------


        //-------------------------  Metodo Eliminar -------------------------
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<DatoUsuario>>> DelDatos(int id)
        {
            var dbDato = await _context.DatosUsuarios.FindAsync(id);
            if (dbDato == null)
                return BadRequest("datos no encontrados.");

            _context.DatosUsuarios.Remove(dbDato);
            await _context.SaveChangesAsync();

            return Ok(await _context.DatosUsuarios.ToListAsync());
        }

        //------------------------- Fin Metodo -------------------------
    }
}
