using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Common.Data.SQLBuilders
{
    public class SqlParameterBuilder
    {
        SqlQueryBuilder sqlQueryBuilder = new SqlQueryBuilder();
        public OracleParameterCollection InsertParametersBuilder<T>(T entity)
        {

            OracleParameterCollection sqlParameter = new OracleCommand().Parameters;
            sqlQueryBuilder.TableAttributeBindName<T>("\"", ":");
            Dictionary<string, bool> dictKeys = sqlQueryBuilder.tablesAttributesWithEncloseSign.PrimaryKeyAutoIncrementDict;
            Dictionary<string, string> dictFields = sqlQueryBuilder.tablesAttributesWithParamSign.FieldsDict;
            PropertyInfo[] prop = entity.GetType().GetProperties();
            foreach (var p in prop)
            {
                if (dictKeys.ContainsKey(p.Name))
                {
                    if (!dictKeys[p.Name])
                        sqlParameter.Add(dictFields[p.Name], p.GetValue(entity));
                }
                else {
                   
                        sqlParameter.Add(dictFields[p.Name], p.GetValue(entity));
                }
            }

            return sqlParameter;
        }

        public OracleParameterCollection UpdateParametersBuilder<T>(long id,T entity) {
           
                OracleParameterCollection sqlParameter = new OracleCommand().Parameters;
            sqlQueryBuilder.TableAttributeBindName<T>("\"", ":");
            Dictionary<string, bool> dictKeys = sqlQueryBuilder.tablesAttributesWithEncloseSign.PrimaryKeyAutoIncrementDict;
            Dictionary<string, string> dictFields = sqlQueryBuilder.tablesAttributesWithParamSign.FieldsDict;
            PropertyInfo[] prop = entity.GetType().GetProperties();
                foreach (var p in prop)
                {
                    if (dictKeys.ContainsKey(p.Name))
                      sqlParameter.Add(dictFields[p.Name],id);
                    else
                      sqlParameter.Add(dictFields[p.Name], p.GetValue(entity));
                }

                return sqlParameter;            
        }

        public OracleParameterCollection SelectOneParametersBuilder<T>(long id)
        {
            Type entity = typeof(T);
            OracleParameterCollection sqlParameter = new OracleCommand().Parameters;
            sqlQueryBuilder.TableAttributeBindName<T>("\"", ":");
            Dictionary<string, bool> dictKeys = sqlQueryBuilder.tablesAttributesWithEncloseSign.PrimaryKeyAutoIncrementDict;
            Dictionary<string, string> dictFields = sqlQueryBuilder.tablesAttributesWithParamSign.FieldsDict;
            PropertyInfo[] prop = entity.GetProperties();
            foreach (var p in prop)
            {
                if (dictKeys.ContainsKey(p.Name))
                    sqlParameter.Add(dictFields[p.Name], id);

            }


            return sqlParameter;
        }

        public OracleParameterCollection DeleteParameterBuilder<T>(long id) {
            Type entity = typeof(T);
            OracleParameterCollection sqlParameter = new OracleCommand().Parameters;
            sqlQueryBuilder.TableAttributeBindName<T>("\"", ":");
            Dictionary<string, bool> dictKeys = sqlQueryBuilder.tablesAttributesWithEncloseSign.PrimaryKeyAutoIncrementDict;
            Dictionary<string, string> dictFields = sqlQueryBuilder.tablesAttributesWithParamSign.FieldsDict;
            PropertyInfo[] prop = entity.GetProperties();
            foreach (var p in prop)
            {
                if (dictKeys.ContainsKey(p.Name))
                    sqlParameter.Add(dictFields[p.Name], id);

            }

            return sqlParameter;
        }
    }
}
