using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Menu : AuditableBaseEntity
    {
        public string? Descripcion { get; set; }
        public string? DisplayName { get; set; }
        public bool Alta { get; set; }
        public int? Indice { get; set; }
        public string? Ruta { get; set; }
        public string? Icono { get; set; }
        public List<Menu>? SubMenus { get; set; }
        public Menu? MenuPadre { get; set; }
        public int? IdPadre { get; set; }
    }
}
