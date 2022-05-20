using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace apiPKT1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {

        private readonly ContextoUsuario _context;

        public RolController(ContextoUsuario context)
        {
            _context = context;
        }

        //-------------------------  Metodo Get -------------------------
        [HttpGet]
        public async Task<ActionResult<List<Rol>>> ObtRol()
        {
            return Ok(await _context.Roles.ToListAsync());
        }
        //------------------------- Fin Metodo -------------------------


        //-------------------------  Metodo Get ID -------------------------
        [HttpGet("{id}")]
        public async Task<ActionResult<Rol>> ObtRolXId(int id)
        {
            var dbRol = await _context.Roles.FindAsync(id);
            if (dbRol == null)
                return BadRequest("Rol no registrada");
            return Ok(dbRol);
        }
        //------------------------- Fin Metodo -------------------------


        //-------------------------  Metodo Post -------------------------
        [HttpPost]
        public async Task<ActionResult<List<Rol>>> RegRol(Rol rol)
        {
            _context.Roles.Add(rol);
            await _context.SaveChangesAsync();

            return Ok(await _context.Roles.ToListAsync());
        }
        //------------------------- Fin Metodo -------------------------


        //-------------------------  Metodo Put -------------------------
        [HttpPut]
        public async Task<ActionResult<List<Rol>>> ModRol(Rol req)
        {
            var dbRol = await _context.Roles.FindAsync(req.Id);

            if (dbRol == null)
                return BadRequest("rol no encontrada");

            dbRol.Descripcion = req.Descripcion;
            

            await _context.SaveChangesAsync();

            return Ok(await _context.Roles.ToListAsync());
        }
        //------------------------- Fin Metodo -------------------------


        //-------------------------  Metodo Eliminar -------------------------
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Rol>>> DelRol(int id)
        {
            var dbRol = await _context.Roles.FindAsync(id);
            if (dbRol == null)
                return BadRequest("Rol no encontrada.");

            _context.Roles.Remove(dbRol);
            await _context.SaveChangesAsync();

            return Ok(await _context.Roles.ToListAsync());
        }

        //------------------------- Fin Metodo -------------------------
    }
}
