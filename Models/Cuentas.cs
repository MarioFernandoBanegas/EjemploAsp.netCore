using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AAAAMMDDMFBO0801199707477.Models
{
    public class Cuentas
    {
        [Key]
        public int id { get; set; }
        public int id_cliente { get; set; }
        public string numero_cuenta { get; set; }
        public decimal saldo { get; set; }
        public string estado { get; set; }
        public int id_tipocuenta { get; set; }
        public int id_moneda { get; set; }

            
    }
}
