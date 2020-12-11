using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AAAAMMDDMFBO0801199707477.Context;
using AAAAMMDDMFBO0801199707477.Models;

namespace AAAAMMDDMFBO0801199707477.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly BancoContext _context;

        public ClientesController(BancoContext context)
        {
            _context = context;
        }

        // GET: api/Clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Clientes>>> Getclientes()
        {
            return await _context.clientes.ToListAsync();
        }

        // GET: api/Clientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Clientes>> GetClientes(int id)
        {
            var clientes = await _context.clientes.FindAsync(id);

            if (clientes == null)
            {
                return Content("Usuario Inexistente");
            }

            return clientes;
        }

        // PUT: api/Clientes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClientes(int id, Clientes clientes)
        {
            //var resultado = _context.clientes.Where(x => x.id == clientes.id).ToList();

            if (id != clientes.id)
            {
                return Content("Usuario Inexistente");
            }
            /*else if (resultado.Count == 0)
            {
                return Content("Usuario Inexistente ");
            }*/

            _context.Entry(clientes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                /*if (!ClientesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }*/
            }

            return Content("Usuario Modificado");
        }

        // POST: api/Clientes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Clientes>> PostClientes(Clientes clientes)
        {
            if (IdentidadExists(clientes.identidad))
            {
                return Content("Usuario ya existente");
            }

            _context.clientes.Add(clientes);
            await _context.SaveChangesAsync();

            return Content("Usuario Ingresado");

            //return CreatedAtAction("GetClientes", new { id = clientes.id }, clientes);
        }

        // DELETE: api/Clientes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Clientes>> DeleteClientes(int id)
        {
            var clientes = await _context.clientes.FindAsync(id);
            if (clientes == null)
            {
                return Content("Usuario Inexistente");
            }

            _context.clientes.Remove(clientes);
            await _context.SaveChangesAsync();

            return Content("Usuario Eliminado");
        }

        private bool IdentidadExists(string id)
        {
            return _context.clientes.Any(e => e.identidad == id);
        }
    }
}
