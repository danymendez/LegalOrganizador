using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Data.SQLBuilders
{
    public class SqlParameterBuilder
    {
        SqlQueryBuilder sqlQueryBuilder = new SqlQueryBuilder();
        public OracleParameterCollection InsertParametersBuilder<T>(T entity)
        {

            OracleParameterCollection sqlParameter = new OracleCommand().Parameters;
            Dictionary<string, bool> dictKeys = sqlQueryBuilder.GetPrimaryKeysAutoIncrem<T>();
            Dictionary<string, string> dictFields = sqlQueryBuilder.GetFields<T>();
            PropertyInfo[] prop = entity.GetType().GetProperties();
            foreach (var p in prop)
            {
                if (dictKeys.ContainsKey(dictFields[p.Name]))
                {
                    if (!dictKeys[dictFields[p.Name]])
                        sqlParameter.Add(dictFields[p.Name], p.GetValue(entity));
                }
                else {
                   
                        sqlParameter.Add(dictFields[p.Name], p.GetValue(entity));
                }
            }

            return sqlParameter;
        }

        public OracleParameterCollection SelectOrUpdateParametersBuilder<T>(T entity) {
           
                OracleParameterCollection sqlParameter = new OracleCommand().Parameters;
                Dictionary<string, string> dictFields = sqlQueryBuilder.GetFields<T>();
                PropertyInfo[] prop = entity.GetType().GetProperties();
                foreach (var p in prop)
                {
                        sqlParameter.Add(":" + dictFields[p.Name], p.GetValue(entity));
                }

                return sqlParameter;            
        }

        public OracleParameterCollection SelectOneParametersBuilder<T>(long id)
        {
            Type entity = typeof(T);
            OracleParameterCollection sqlParameter = new OracleCommand().Parameters;
            Dictionary<string, bool> dictKeys = sqlQueryBuilder.GetPrimaryKeysAutoIncrem<T>();
            Dictionary<string, string> dictFields = sqlQueryBuilder.GetFields<T>();
            PropertyInfo[] prop = entity.GetProperties();
            foreach (var p in prop)
            {
                if (dictKeys.ContainsKey(dictFields[p.Name]))
                    sqlParameter.Add(":" + dictFields[p.Name], id);

            }


            return sqlParameter;
        }

        public OracleParameterCollection DeleteParameterBuilder<T>(long id) {
            Type entity = typeof(T);
            OracleParameterCollection sqlParameter = new OracleCommand().Parameters;
            Dictionary<string, bool> dictKeys = sqlQueryBuilder.GetPrimaryKeysAutoIncrem<T>();
            Dictionary<string, string> dictFields = sqlQueryBuilder.GetFields<T>();
            PropertyInfo[] prop = entity.GetProperties();
            foreach (var p in prop)
            {
                if (dictKeys.ContainsKey(dictFields[p.Name]))
                    sqlParameter.Add(":" + dictFields[p.Name], id);

            }

            return sqlParameter;
        }
    }
}
