using Domain.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DBConnection
{
    public class SQLDBConnection : IConexion
    {

        protected SqlConnection connection;
        protected SqlCommand command;
        protected SqlTransaction transaction;

        string connectionString = "Server=DESKTOP-UMTV5OF\\SQLEXPRESS;database=DB_FinanzApp;Integrated Security=true";

        public SQLDBConnection(string connectionString)
        {
            connection = new SqlConnection(connectionString);
        }

        public bool ManejarAperturaTransaccion()
        {
            if (connection.State != ConnectionState.Open)
            {
                AbrirConexion();
                return false;
            }
            return true;
        }

        public bool ManejarCierreTransaccion(bool conexionEstabaAbierta, bool resultado)
        {
            if (!conexionEstabaAbierta)
            {
                if (resultado)
                    HacerCommit();
                else
                    HacerRollBack();
            }
            return resultado;
        }

        public void CerrarConexion()
        {
            connection.Close();
            command.Dispose();
            transaction.Dispose();
        }

        public void AbrirConexion()
        {
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
                command = connection.CreateCommand();
                transaction = connection.BeginTransaction();
                command.Connection = connection;
                command.Transaction = transaction;
            }
        }

        public void HacerCommit()
        {
            transaction.Commit();
            CerrarConexion();
        }

        public void HacerRollBack()
        {
            transaction.Rollback();
            CerrarConexion();
        }

        public decimal Ejecutar(string nombreStoredProcedure, SqlParameter[] parametros, int TiempoDeEspera = 0)
        {
            try
            {
                object ret;
                bool conexionAbierta = false;

                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                    command = connection.CreateCommand();
                    command.Connection = connection;
                    conexionAbierta = true;
                }

                SetearComando(nombreStoredProcedure, parametros, TiempoDeEspera);
                ret = command.ExecuteScalar();

                if (conexionAbierta)
                    connection.Close();

                if (ret == null)
                    return 0;
                else
                    return decimal.Parse(ret.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet Obtener(string nombreStoredProcedure, SqlParameter[] parametros, int TiempoDeEspera = 0)
        {
            try
            {
                bool conexionEstabaAbierta = ManejarAperturaTransaccion();

                SetearComando(nombreStoredProcedure, parametros, TiempoDeEspera);

                DataSet ds = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                using (adapter)
                    adapter.Fill(ds, "Resultado");

                if (!conexionEstabaAbierta)
                    HacerCommit();

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetearComando(string nombreStoredProcedure, SqlParameter[] parametros, int tiempoDeEspera)
        {
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = nombreStoredProcedure;

            if (tiempoDeEspera != 0)
                command.CommandTimeout = tiempoDeEspera;
            SetearParametros(parametros);
        }

        private void SetearParametros(SqlParameter[] parametros)
        {
            command.Parameters.Clear();
            if ((parametros != null))
            {
                if (parametros.Length != 0)
                {
                    if ((parametros[0] != null))
                        command.Parameters.AddRange(parametros);
                }
            }
        }

        public DataSet EjecutarQuery(string consulta)
        {
            command.CommandType = CommandType.Text;
            command.CommandText = consulta;
            command.Connection = connection;

            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(command);

            using (adapter)
                adapter.Fill(ds, "tHorasxTeam");

            return ds;
        }

    }
}
