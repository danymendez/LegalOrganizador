﻿using Common.Entity.Attributes;
using Common.Utilities;
using Common.Data.SQLBuilders;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Common.Data.DAL
{
    public class DALBaseOrcl
    {
        private OracleConnection _sqlConnection;
        private OracleTransaction sqlTran;
        private OracleCommand command;

        private SqlQueryBuilder sqlQueryBuilder;
        private SqlParameterBuilder sqlParameterBuilder;
        public DALBaseOrcl(DALDBContext context)
        {
        
            this._sqlConnection = context._sqlConnection;
            this.command = context.command;
            this.sqlTran = context.sqlTran;
          
           
        }


        #region Create
        protected virtual T Create<T>(T entity)
        {
            sqlQueryBuilder = new SqlQueryBuilder();
            sqlParameterBuilder = new SqlParameterBuilder();

            using (OracleCommand command = new OracleCommand())
            {
                command.Connection = _sqlConnection;
                command.Transaction = sqlTran;
                command.CommandText = sqlQueryBuilder.InsertQuery<T>();

                foreach (OracleParameter param in sqlParameterBuilder.InsertParametersBuilder(entity))
                {
                    command.Parameters.Add(param.ParameterName, param.Value);
                }
                OracleParameter outputParameter = new OracleParameter("my_id_param", OracleDbType.Decimal);
                outputParameter.Direction = ParameterDirection.Output;
                command.Parameters.Add(outputParameter);
                //int a = 0;
                try
                {
                    command.ExecuteNonQuery();
                   // a = Convert.ToInt32((decimal)(OracleDecimal)outputParameter.Value);
                }
                catch (Exception ex)
                {
                    // Handle the exception if the transaction fails to commit.
                    ExceptionUtility.LogException(ex);

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
                        ExceptionUtility.LogException(exRollback);
                    }
                }

                PropertyInfo[] prop = entity.GetType().GetProperties();
                foreach (var p in prop)
                {
                    var attrPrimaryKey = p.GetCustomAttributes().Where(c => c.GetType().Name.Equals("PrimaryKey")).FirstOrDefault();
                    PrimaryKey pk = attrPrimaryKey as PrimaryKey;
                    if (pk != null)
                    {
                        p.SetValue(entity, (decimal)(OracleDecimal)outputParameter.Value);
                    }
                }
            }
            return entity;
        }
        #endregion

        #region Get
        protected virtual List<T> GetAll<T>() where T : new()
        {
            List<T> listEntity = new List<T>();
            sqlQueryBuilder = new SqlQueryBuilder();
        //    var valor = sqlQueryBuilder.GetFields<T>();
            using (OracleCommand command = new OracleCommand())
            {
                command.Connection = _sqlConnection;
                command.Transaction = sqlTran;
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



                            //int id;
                            //bool EsEntero = int.TryParse(reader[sqlQueryBuilder.tablesAttributesWithEncloseSign.FieldsDict[p.Name]].ToString(), out id);

                            //if (EsEntero && p.PropertyType == typeof(int))
                            //    p.SetValue(obj, id);
                            //else
                                p.SetValue(obj, reader[sqlQueryBuilder.tablesAttributesWithEncloseSign.FieldsDict[p.Name]] == DBNull.Value ? null : reader[sqlQueryBuilder.tablesAttributesWithEncloseSign.FieldsDict[p.Name]]);

                        }
                        listEntity.Add(obj);
                    }
                }
                catch (Exception ex)
                {
                    // Handle the exception if the transaction fails to commit.
                    ExceptionUtility.LogException(ex);

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
                        ExceptionUtility.LogException(exRollback);
                    }
                }
            }
            return listEntity;
        }

        protected virtual T Get<T>(decimal id) where T: new() {
           
           T entity = default(T);
            sqlQueryBuilder = new SqlQueryBuilder();
            sqlParameterBuilder = new SqlParameterBuilder();
          //  var valor = sqlQueryBuilder.GetFields<T>();
            using (OracleCommand command = new OracleCommand())
            {
                command.Connection = _sqlConnection;

                command.Transaction = sqlTran;
                command.CommandText = sqlQueryBuilder.SelectQuery<T>();

                foreach (OracleParameter param in sqlParameterBuilder.SelectOneParametersBuilder<T>(id))
                {
                    command.Parameters.Add(param.ParameterName, param.Value);
                }

                try
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var obj = new T();
                        PropertyInfo[] prop = obj.GetType().GetProperties();
                        foreach (var p in prop)
                        {



                            //int ids;
                            //bool EsEntero = int.TryParse(reader[sqlQueryBuilder.tablesAttributesWithEncloseSign.FieldsDict[p.Name]].ToString(), out ids);

                            //if (EsEntero && p.PropertyType == typeof(int))
                            //    p.SetValue(obj, ids);
                            //else
                                p.SetValue(obj, reader[sqlQueryBuilder.tablesAttributesWithEncloseSign.FieldsDict[p.Name]] == DBNull.Value ? null : reader[sqlQueryBuilder.tablesAttributesWithEncloseSign.FieldsDict[p.Name]]);

                        }
                        entity = obj;
                    }
                }
                catch (Exception ex)
                {
                    // Handle the exception if the transaction fails to commit.
                    ExceptionUtility.LogException(ex);

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
                        ExceptionUtility.LogException(exRollback);
                    }
                }
            }
            return entity;
        }
        #endregion

        #region Update
        protected virtual T Update<T>(decimal id, T entity) {
            T t = default(T);
            sqlQueryBuilder = new SqlQueryBuilder();
            sqlParameterBuilder = new SqlParameterBuilder();
            using (OracleCommand command = new OracleCommand())
            {
                command.Connection = _sqlConnection;
                command.Transaction = sqlTran;
                command.CommandText = sqlQueryBuilder.UpdateQuery<T>();
                command.BindByName = true;

                foreach (OracleParameter param in sqlParameterBuilder.UpdateParametersBuilder(id, entity))
                {
                    command.Parameters.Add(param.ParameterName, param.Value);
                }

                try
                {
                    int rowsUpdate = command.ExecuteNonQuery();
                    var i = command.CommandText;
                    //  sqlTran.Commit();
                    t = entity;
                }
                catch (Exception ex)
                {
                    // Handle the exception if the transaction fails to commit.
                    ExceptionUtility.LogException(ex);

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
                        ExceptionUtility.LogException(exRollback);
                    }
                }
            }
            return entity;
        }
        #endregion

        #region Delete
        protected virtual T Delete<T>(decimal id) where T : new() {
            T t = default(T);
            t = Get<T>(id);
            if (t != null)
            {
                sqlQueryBuilder = new SqlQueryBuilder();
                sqlParameterBuilder = new SqlParameterBuilder();

                using (OracleCommand command = new OracleCommand())
                {
                    command.Connection = _sqlConnection;
                    command.Transaction = sqlTran;
                    command.CommandText = sqlQueryBuilder.DeleteQuery<T>();
                    foreach (OracleParameter param in sqlParameterBuilder.DeleteParameterBuilder<T>(id))
                    {
                        command.Parameters.Add(param.ParameterName, param.Value);
                    }


                    try
                    {
                        command.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        t = default(T);
                        // Handle the exception if the transaction fails to commit.
                        ExceptionUtility.LogException(ex);

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
                            ExceptionUtility.LogException(exRollback);
                        }
                    }
                }
            }
            return t;
        }
        #endregion
    }
}
