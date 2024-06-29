using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface
{
    public interface IConexion
    {
        DataSet Obtener(string nombre, SqlParameter[] parametros, int tiempoDeEspera = 0);
        bool ManejarAperturaTransaccion();
        bool ManejarCierreTransaccion(bool ConexionEstabaAbierta, bool Resultado);
        void AbrirConexion();
        void HacerCommit();
        void HacerRollBack();
        decimal Ejecutar(string nombre, SqlParameter[] parametros, int tiempoDeEspera);
        DataSet EjecutarQuery(string consulta);
    }
}
