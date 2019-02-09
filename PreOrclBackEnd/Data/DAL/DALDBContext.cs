using Common.Utilities;
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

        

        public DALDBContext()
        {
            try
            {
                OpenConnection();
                sqlTran = _sqlConnection.BeginTransaction();
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogException(ex);
            }

        }

        public DALDBContext(string oradbString)
        {
            try
            {
                OpenConnection();
                sqlTran = _sqlConnection.BeginTransaction();
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogException(ex);
            }

        }


        private void OpenConnection()
        {
           
                _sqlConnection = new OracleConnection(GetConnectionString());


                _sqlConnection.Open();
         

            

        }


        private void OpenConnection(string oradbString)
        {
            _sqlConnection = new OracleConnection(GetConnectionString(oradbString));
            _sqlConnection.Open();
        }

        private void CloseConnection()
        {
            _sqlConnection.Close();
        }

        private string GetConnectionString(string oradbString = null)
        {

            //         string oradb = "Data Source=(DESCRIPTION="
            //+ "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.0.40)(PORT=1521)))"
            //+ "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=PREORCLPDB)));"
            //+ "User Id=dmendez;Password=1234;";
            
            string oradb = oradbString ?? "Data Source=(DESCRIPTION="
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
                ExceptionUtility.LogException(ex);
                try
                {
                    sqlTran.Rollback();
                }
                catch (Exception exRollback)
                {
                    ExceptionUtility.LogException(exRollback);
                }
            }
            finally
            {
                sqlTran.Dispose();
                CloseConnection();
                _sqlConnection.Dispose();
            }
        }
    }
}
