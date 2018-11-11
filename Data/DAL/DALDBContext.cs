using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.DAL
{
    public class DALDBContext : IDisposable
    {

        public OracleConnection _sqlConnection { get; private set; }
        public OracleTransaction sqlTran { get; private set; }
        public OracleCommand command { get; private set; }
       // private OracleDataReader sqlDataReader;
        

        public DALDBContext()
        {
            Console.WriteLine("Abriendo Conexión");
            OpenConnection();
          //  command = new OracleCommand();
          //  command.Connection = _sqlConnection;
            sqlTran = _sqlConnection.BeginTransaction();
           // command = _sqlConnection.CreateCommand();
           // command.Transaction = sqlTran;
           

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

            //         string oradb = "Data Source=(DESCRIPTION="
            //+ "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.0.40)(PORT=1521)))"
            //+ "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=PREORCLPDB)));"
            //+ "User Id=dmendez;Password=1234;";
            string oradb = "Data Source=(DESCRIPTION="
           + "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=wkapp.southcentralus.cloudapp.azure.com)(PORT=1521)))"
           + "(CONNECT_DATA=(SERVER=DEDICATED)(SID=farmacia)));"
           + "User Id=dmendez;Password=1234;";

            return oradb;
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
            finally
            {
              //  command.Dispose();
                sqlTran.Dispose();
                CloseConnection();
                _sqlConnection.Dispose();
            }
        }
    }
}
