using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AAAAMMDDMFBO0801199707477.Models
{
    public class Clientes
    {
        [Key]
        public int id { get; set; }
        public string p_nombre { get; set; }

        public string s_nombre { get; set; }
        public string p_apellido { get; set; }
        public string s_apellido { get; set; }
        public string direccion { get; set; }
        public string fecha_nac { get; set; }
        public string identidad { get; set; }
        public string genero { get; set; }
        public int id_cuidad { get; set; }
        public int id_profesion { get; set; }
        public string estado { get; set; }
    }
}
