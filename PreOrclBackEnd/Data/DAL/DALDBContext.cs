using Common.Utilities;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using Microsoft.Extensions.Configuration.FileExtensions;
using System;
using System.Collections.Generic;
using System.IO;
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
            string oradb = oradbString ?? new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json").Build()
                        .GetConnectionString("DBConnectionOrcl");

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
