using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace apiPKT1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SucursalController : ControllerBase
    {


        private readonly ContextoUsuario _context;

        public SucursalController(ContextoUsuario context)
        {
            _context = context;
        }

        //-------------------------  Metodo Get -------------------------
        [HttpGet]
        public async Task<ActionResult<List<Sucursal>>> GetSucursal()
        {
            return Ok(await _context.Sucursales.ToListAsync());
        }
        //------------------------- Fin Metodo -------------------------


        //-------------------------  Metodo Get ID -------------------------
        [HttpGet("{id}")]
        public async Task<ActionResult<Sucursal>> GetSucursalXId(int id)
        {
            var dbSuc = await _context.Sucursales.FindAsync(id);
            if (dbSuc == null)
                return BadRequest("Sucursal no registrada");
            return Ok(dbSuc);
        }
        //------------------------- Fin Metodo -------------------------


        //-------------------------  Metodo Post -------------------------
        [HttpPost]
        public async Task<ActionResult<List<Sucursal>>> PostSucursal(Sucursal suc)
        {
            _context.Sucursales.Add(suc);
            await _context.SaveChangesAsync();

            return Ok(await _context.Sucursales.ToListAsync());
        }
        //------------------------- Fin Metodo -------------------------


        //-------------------------  Metodo Put -------------------------
        [HttpPut]
        public async Task<ActionResult<List<Sucursal>>> PutSucursal(Sucursal req)
        {
            var dbSuc = await _context.Sucursales.FindAsync(req.Id);

            if (dbSuc == null)
                return BadRequest("sucursal no encontrada");

            dbSuc.Descripcion = req.Descripcion;
            dbSuc.Direccion = req.Direccion;
            dbSuc.NumeroExterior = req.NumeroExterior;
            dbSuc.Cp = req.Cp;
            dbSuc.Email = req.Email;
            dbSuc.Telefono = req.Telefono;

            await _context.SaveChangesAsync();

            return Ok(await _context.Sucursales.ToListAsync());
        }
        //------------------------- Fin Metodo -------------------------


        //-------------------------  Metodo Eliminar -------------------------
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Sucursal>>> DeleteSucursal(int id)
        {
            var dbSuc = await _context.Sucursales.FindAsync(id);
            if (dbSuc == null)
                return BadRequest("Sucursal no encontrada.");

            _context.Sucursales.Remove(dbSuc);
            await _context.SaveChangesAsync();

            return Ok(await _context.Sucursales.ToListAsync());
        }

        //------------------------- Fin Metodo -------------------------


    }
}
