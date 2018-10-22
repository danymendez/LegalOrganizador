using FamaciasApi.Attributes;
using FamaciasApi.Utils;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace FamaciasApi.DAL
{
    public class DALBaseOrcl : IDisposable
    {
        private OracleConnection _sqlConnection;
        private OracleTransaction sqlTran;
        private OracleCommand command;
        private OracleDataReader sqlDataReader;
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
            command.CommandText = SqlInsertQueryBuilder(entity);

            foreach (OracleParameter param in SqlParametersBuilder(entity))
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
            command.CommandText = SqlSelectQueryBuilder(entity);
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

        public string SqlSelectQueryBuilder<T>(T entity)
        {
            PropertyInfo[] prop = entity.GetType().GetProperties();
            string query = "Select ";
            foreach (var p in prop)
            {
                
                query += p.Name;
                if (!p.Equals(prop.Last()))
                {
                    query += ",";
                }
            }
            query += "from " + entity.GetType().Name;

            return query;
        }

        public TableSerialize GetTableAttributes<T>() {
            Type entity = typeof(T);
            TableSerialize tableSerialize = new TableSerialize();
            PropertyInfo[] prop = entity.GetProperties();

            if (entity.GetCustomAttributes().Count() != 0)
            {
                foreach (var attribute in entity.GetCustomAttributes())
                {

                    if (attribute.GetType().Name.Equals("Table"))
                    {
                        Table tb = attribute as Table;
                        if (tb != null && tb.Name != null)
                        {
                            tableSerialize.TableName = tb.Name;
                            continue;
                        }

                        tableSerialize.TableName = entity.Name;
                    }

                }
            }
            else {
                tableSerialize.TableName = entity.Name;
            }

            foreach (var p in prop)
            {
                if (p.GetCustomAttributes().Count() != 0)
                {


                    foreach (var attribute in p.GetCustomAttributes())
                    {



                        if (attribute.GetType().Name.Equals("PrimaryKey"))
                        {
                            PrimaryKey primaryKey = attribute as PrimaryKey;
                            if (primaryKey != null)
                            {
                               
                                tableSerialize.PrimaryKeyName.Add(p.Name, new Tuple<string,bool>("",primaryKey.AutoIncrement));
                                continue;
                            }
                            else if (primaryKey != null) {
                                tableSerialize.PrimaryKeyName.Add(p.Name, new Tuple<string, bool>(p.Name,false));
                            }
                        }

                        if (attribute.GetType().Name.Equals("Field"))
                        {
                            Field field = attribute as Field;
                            if (field != null && field.Name != null)
                            {
                                tableSerialize.FieldName.Add(p.Name,field.Name);
                                continue;
                            }
                        }

                        
                    }
                }
                else {
                    tableSerialize.FieldName.Add(p.Name,p.Name);
                }
           
            }

            return tableSerialize;
        }


        public string SqlInsertQueryBuilder<T>(T entity)
        {
            TableSerialize tableSerialize = GetTableAttributes<T>();
            string query = String.Format("Insert into {0} ({1},{2}) values("
                                        , tableSerialize.TableName, string.Join(",", tableSerialize.PrimaryKeyName.Values)
                                        , string.Join(",", tableSerialize.FieldName.Values));

            foreach (var valor in tableSerialize.PrimaryKeyName)
            {
                query += ":" + valor.Value.Item1;
                
                
                    query += ",";
                
            }


            foreach (var valor in tableSerialize.FieldName) {
                query += ":" + valor.Value;
                if (!valor.Equals(tableSerialize.FieldName.Last()))
                {
                    query += ",";
                }
            }
            query += ")";                     
            return query;
        }

        public OracleParameterCollection SqlParametersBuilder<T>(T entity)
        {

            OracleParameterCollection sqlParameter = new OracleCommand().Parameters;
            PropertyInfo[] prop = entity.GetType().GetProperties();
            TableSerialize tableSerialize = GetTableAttributes<T>();
            foreach (var p in prop)
            {
                sqlParameter.Add(":" + (!tableSerialize.PrimaryKeyName.ContainsKey(p.Name) ? tableSerialize.FieldName[p.Name] 
                                    : tableSerialize.PrimaryKeyName[p.Name].Item1), p.GetValue(entity));
               
                string a = p.GetValue(entity).ToString();
            }

            return sqlParameter;

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
