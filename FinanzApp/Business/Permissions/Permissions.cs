using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Permissions
{
    public static class Permissions
    {
        public const string PREFIJO_PERMISOS_MENU = "Permissions.Menu.";
        public const string CLAIMTYPE_PERMISOS = "Permission";

        public static List<Type> GetAllTypes()
        {
            return new List<Type>
            {
                //typeof (Reporte)
            };
        }
        //public static class Reporte
        //{
        //    public const string View = "Permissions.Reporte.View";
        //    public const string NomTodosLosColegios = "Permissions.Reporte.NominadosTodosLosColegios.View";
        //    public const string NomTodosXColegio = "Permissions.Reporte.NominadosPorColegio.View";
        //    public const string NoNomTodosLosColegios = "Permissions.Reporte.NoNominadosTodosLosColegios.View";
        //    public const string NoNomXColegio = "Permissions.Reporte.NoNominadosPorColegio.View";
        //}
    }
}
