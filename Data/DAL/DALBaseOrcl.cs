using Data.SQLBuilders;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Data.DAL
{
    public class DALBaseOrcl : IDisposable
    {
        private OracleConnection _sqlConnection;
        private OracleTransaction sqlTran;
        private OracleCommand command;
        private OracleDataReader sqlDataReader;
        private SqlQueryBuilder sqlQueryBuilder;
        private SqlParameterBuilder sqlParameterBuilder;
        public DALBaseOrcl()
        {
            Console.WriteLine("Abriendo Conexión");
            OpenConnection();
            sqlTran = _sqlConnection.BeginTransaction();
            command = new OracleCommand();
            command.Connection = _sqlConnection;
            command = _sqlConnection.CreateCommand();
            command.Transaction = sqlTran;
        }

        public DALBaseOrcl(string test) {

        }

        private void OpenConnection()
        {

            _sqlConnection = new OracleConnection(GetConnectionString());


            _sqlConnection.Open();

        }

        private void CloseConnection()
        {
            _sqlConnection.Close();
        }

        private string GetConnectionString()
        {

            string oradb = "Data Source=(DESCRIPTION="
    + "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=wkapp.southcentralus.cloudapp.azure.com)(PORT=1521)))"
    + "(CONNECT_DATA=(SERVER=DEDICATED)(SID=farmacia)));"
    + "User Id=dmendez;Password=1234;";
            return oradb;
        }

        public virtual T Create<T>(T entity)
        {
            sqlQueryBuilder = new SqlQueryBuilder();
            sqlParameterBuilder = new SqlParameterBuilder();
            command.CommandText = sqlQueryBuilder.InsertQuery<T>();

            foreach (OracleParameter param in sqlParameterBuilder.InsertParametersBuilder(entity))
            {
                command.Parameters.Add(param.ParameterName, param.Value);
            }

            command.ExecuteNonQuery();
            return entity;
        }

        public virtual List<T> GetAll<T>(T entity) where T : new()
        {
            List<T> listEntity = new List<T>();
            var type = typeof(T);
            var props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
           // command.CommandText = SqlSelectQueryBuilder(entity);
            var reader = command.ExecuteReader();
            PropertyInfo[] prop = entity.GetType().GetProperties();
            while (reader.NextResult())
            {
                var obj = new T();
                foreach (var p in prop)
                {

                }
            }


            return listEntity;
        }

        public void Dispose()
        {
            Console.WriteLine("Cerrando Conexion");
            Console.WriteLine("Both records were written to database.");
            try
            {

                sqlTran.Commit();
            }
            catch (Exception ex)
            {
                // Handle the exception if the transaction fails to commit.
                Console.WriteLine(ex.Message);

                try
                {
                    // Attempt to roll back the transaction.
                    sqlTran.Rollback();
                }
                catch (Exception exRollback)
                {
                    // Throws an InvalidOperationException if the connection 
                    // is closed or the transaction has already been rolled 
                    // back on the server.
                    Console.WriteLine(exRollback.Message);
                }
            }
            // CloseConnection();
        }
    }
}
