using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class Permiso
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int EstadoOrigen { get; set; }
        public int EstadoDestino { get; set; }
        public string Tipo { get; set; }
        //public int IdSistema { get; set; }
        public string IndiceMenu { get; set; }
        public int? NivelMenu { get; set; }
        public string Operacion { get; set; }
        public string Campo { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
    }
}
