﻿using Common.Attributes;
using Data.SQLBuilders;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Data.DAL
{
    public class DALBaseOrcl
    {
        private OracleConnection _sqlConnection;
        private OracleTransaction sqlTran;
        private OracleCommand command;
        private OracleDataReader sqlDataReader;
        private SqlQueryBuilder sqlQueryBuilder;
        private SqlParameterBuilder sqlParameterBuilder;
        public DALBaseOrcl(DALDBContext context)
        {
            Console.WriteLine("Abriendo Conexión");
            this._sqlConnection = context._sqlConnection;
            this.sqlTran = context.sqlTran;
         //   sqlTran = _sqlConnection.BeginTransaction();
       //     command = context.command;
           
        }




        public virtual T Create<T>(T entity)
        {
            sqlQueryBuilder = new SqlQueryBuilder();
            sqlParameterBuilder = new SqlParameterBuilder();

            command = new OracleCommand();
            command.Connection = _sqlConnection;
            command.Transaction = sqlTran;
                command.CommandText = sqlQueryBuilder.InsertQuery<T>();

                foreach (OracleParameter param in sqlParameterBuilder.InsertParametersBuilder(entity))
                {
                    command.Parameters.Add(param.ParameterName, param.Value);
                }
                OracleParameter outputParameter = new OracleParameter("my_id_param", OracleDbType.Int32);
                outputParameter.Direction = ParameterDirection.Output;
                command.Parameters.Add(outputParameter);
                int a = 0;
                try
                {
                    command.ExecuteNonQuery();
                    a = Convert.ToInt32((decimal)(OracleDecimal)outputParameter.Value);
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

                PropertyInfo[] prop = entity.GetType().GetProperties();
                foreach (var p in prop)
                {
                    var attrPrimaryKey = p.GetCustomAttributes().Where(c => c.GetType().Name.Equals("PrimaryKey")).FirstOrDefault();
                    PrimaryKey pk = attrPrimaryKey as PrimaryKey;
                    if (pk != null)
                    {
                        p.SetValue(entity, a);
                    }
                }
            
            return entity;
        }

        public virtual List<T> GetAll<T>() where T : new()
        {
            List<T> listEntity = new List<T>();
            sqlQueryBuilder = new SqlQueryBuilder();
            var valor = sqlQueryBuilder.GetFields<T>();
            
                command.CommandText = sqlQueryBuilder.SelectAllQuery<T>();

                try
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var obj = new T();
                        PropertyInfo[] prop = obj.GetType().GetProperties();
                        foreach (var p in prop)
                        {


                            if (p.PropertyType == typeof(Int32))
                            {
                                int id;
                                bool EsEntero = int.TryParse(reader["\"" + valor[p.Name] + "\""].ToString(), out id);

                                if (EsEntero)
                                    p.SetValue(obj, id);
                            }
                            if (p.PropertyType == typeof(System.String))
                            {
                                p.SetValue(obj, reader["\"" + valor[p.Name] + "\""] == DBNull.Value ? null : reader["\"" + valor[p.Name] + "\""]);
                            }
                        }
                        listEntity.Add(obj);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            
            return listEntity;
        }

        
    }
}
