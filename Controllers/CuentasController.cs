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
    public class CuentasController : ControllerBase
    {
        private readonly BancoContext _context;

        public CuentasController(BancoContext context)
        {
            _context = context;
        }

        // GET: api/Cuentas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cuentas>>> GetCuentas()
        {
            return await _context.Cuentas.ToListAsync();
        }

        // GET: api/Cuentas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cuentas>> GetCuentas(int id)
        {
            var cuentas = await _context.Cuentas.FindAsync(id);

            if (cuentas == null)
            {
                return Content("Cuenta Inexistente");
            }

            return cuentas;
        }

        // PUT: api/Cuentas/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCuentas(int id, Cuentas cuentas)
        {
            if (id != cuentas.id)
            {
                return Content("Cuenta Inexistente");
            }
            else if (cuentas.id_moneda == 1 && cuentas.saldo < 200)
            {
                return Content("El saldo debe ser mayor a 200 lempiras para poder guardar cambios");
            }
            else if (cuentas.id_moneda == 2 && cuentas.saldo < 10)
            {
                return Content("El saldo debe ser mayor a 10 dolares para poder guardar cambios");
            }

            _context.Entry(cuentas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CuentasExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Content("Cuenta Modificada");
        }


        // PUT: api/deposito/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("deposito/{id}")]
        public async Task<IActionResult> Putcuentas(int id, Cuentas cuentas)
        {
            var estado = await _context.Cuentas.AsNoTracking().FirstOrDefaultAsync(m => m.id == id);

            if (estado.estado == "B")
            {
                return Content("Cuenta Bloqueada");
            }

            var saldofinal = estado.saldo + cuentas.saldo;

            _context.Entry(cuentas);
            _context.Entry(cuentas).Property(x => x.saldo).IsModified = true;

            try
            {
                await _context.SaveChangesAsync();
                return Content("Modificacion Exitosa:  " + saldofinal);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
        }

        // PUT: api/deposito/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("retiro/{id}")]
        public async Task<IActionResult> Putcuentasretiro(int id, Cuentas cuentas)
        {
            var estado = await _context.Cuentas.AsNoTracking().FirstOrDefaultAsync(m => m.id == id);


            var saldofinal = estado.saldo - cuentas.saldo;

            /*_context.Entry(cuentas);
            _context.Entry(cuentas).Property(x => x.saldo).IsModified = true;*/

            if(saldofinal<100 && estado.id_tipocuenta==1)
            {
                return Content("Cuenta Inactivada con: " + saldofinal + " saldo debe ser mayor a 100 lempiras");
            }
            else if (saldofinal < 5 && estado.id_tipocuenta == 2)
            {
                return Content("Cuenta Inactivada con: " + saldofinal + " saldo debe ser mayor a 5 dolares");
            }

            return Content("Retiro Exitoso");

        }

        // POST: api/Cuentas
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Cuentas>> PostCuentas(Cuentas cuentas)
        {

            var cliente = await _context.clientes.FindAsync(cuentas.id_cliente);

            var cuentaexistente = _context.Cuentas.Where(x => x.id_cliente == cuentas.id_cliente && x.id_tipocuenta == cuentas.id_tipocuenta).ToList();

            if (cliente.estado == "B")
            {
                return Content("Usuario Bloqueado");
            }
            else if (cliente.estado == "C")
            {
                return Content("Usuario Inactivo");
            }
            else if (cuentas.id_moneda == 1 && cuentas.saldo < 200)
            {
                return Content("El saldo debe ser mayor a 200 lempiras para poder crear la cuenta");
            }
            else if (cuentas.id_moneda == 2 && cuentas.saldo < 10)
            {
                return Content("El saldo debe ser mayor a 10 dolares para poder crear la cuenta");
            }
            else if (cuentaexistente.Count() == 1)
            {
                return Content("Ya existe una cuenta");
            }
            else
            {
                _context.Cuentas.Add(cuentas);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetCuentas", new { id = cuentas.id }, cuentas);


            return Content("Cuenta Creada");

            }
        }

        // DELETE: api/Cuentas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Cuentas>> DeleteCuentas(int id)
        {
            var cuentas = await _context.Cuentas.FindAsync(id);
            if (cuentas == null)
            {
                return NotFound();
            }

            _context.Cuentas.Remove(cuentas);
            await _context.SaveChangesAsync();

            return cuentas;
        }

        private bool CuentasExists(int id)
        {
            return _context.Cuentas.Any(e => e.id == id);
        }

    }
}
