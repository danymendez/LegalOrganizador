using Common.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Data.SQLBuilders
{
    public class SqlQueryBuilder
    {
        public string InsertQuery<T>() {

            Dictionary<string, string> dictionaryField = GetFields<T>();
            Dictionary<string, string> dictionaryFieldComilla = new Dictionary<string, string>();
            Dictionary<string, bool> dictionaryPrimaryKeys = GetPrimaryKeysAutoIncrem<T>();
            Dictionary<string, string> dictFieldWithSignParameter = new Dictionary<string, string>();

            ArrayList fieldToRemove = new ArrayList();

            foreach (var dic in dictionaryField)
            {
                if (dictionaryPrimaryKeys.ContainsKey(dic.Value))
                {
                    if (dictionaryPrimaryKeys[dic.Value])
                    {
                        fieldToRemove.Add(dic.Key);
                    }

                }
            }

            foreach (var item in fieldToRemove) {
                dictionaryField.Remove(item.ToString());
            }
            
            foreach (var dic in dictionaryField) {
                dictFieldWithSignParameter.Add(dic.Key, ":" + dic.Value);
                dictionaryFieldComilla.Add(dic.Key, "\"" + dic.Value + "\"");
            }

            string query = String.Format("insert into \"{0}\" ({1}) values ({2}) RETURNING \"{3}\" into :my_id_param"
                                            , GetTableName<T>()
                                            , string.Join(", ", dictionaryFieldComilla.Values)
                                            ,string.Join(", ",dictFieldWithSignParameter.Values)
                                            ,string.Join("", dictionaryPrimaryKeys.Keys));

            return query;
        }

        public string UpdateQuery<TEntity>() {

            Dictionary<string, string> dictionaryField = GetFields<TEntity>();
            Dictionary<string, bool> dictionaryPrimaryKeys = GetPrimaryKeysAutoIncrem<TEntity>();
            Dictionary<string, string> dictFieldWithSignParameter = new Dictionary<string, string>();
            Dictionary<string, string> dictionaryKeysWithParameters = new Dictionary<string, string>();

            ArrayList fieldToRemove = new ArrayList();

            foreach (var dic in dictionaryField)
            {
                if (dictionaryPrimaryKeys.ContainsKey(dic.Value))
                {
                    if (dictionaryPrimaryKeys[dic.Value])
                    {
                        fieldToRemove.Add(dic.Key);
                      
                    }
                    dictionaryKeysWithParameters.Add(dic.Key, dic.Key + "=:" + dic.Key);
                }
            }

            foreach (var item in fieldToRemove)
            {
                dictionaryField.Remove(item.ToString());
            }

            foreach (var dic in dictionaryField)
            {
                dictFieldWithSignParameter.Add(dic.Key, dic.Value+"="+":" + dic.Value);
            }

            string query = String.Format("UPDATE {0} SET {1} where {2}"
                                            , GetTableName<TEntity>()
                                            , string.Join(", ", dictFieldWithSignParameter.Values)
                                            , string.Join(" and ", dictionaryKeysWithParameters.Values));

            return query;
        }

        public string SelectAllQuery<T>() {
            Dictionary<string, string> dictionaryField = new Dictionary<string, string>();
            foreach (var item in GetFields<T>()) {

                dictionaryField.Add(item.Key, "\"" + item.Value + "\"");
            }
            string query = String.Format("Select {0} from \"{1}\" ", string.Join(",", dictionaryField.Values), GetTableName<T>());

            return query;
        }

        public string SelectQuery<TEntity>() {
            Dictionary<string, string> dictionaryKeysWithParameters = new Dictionary<string, string>();

            foreach (var item in GetPrimaryKeysAutoIncrem<TEntity>()) {
                dictionaryKeysWithParameters.Add(item.Key, item.Key + "=:" + item.Key);
            }
            string query = String.Format("Select {0} from {1} where {2}"
                                        , string.Join(",", GetFields<TEntity>().Values)
                                        , GetTableName<TEntity>()
                                        ,string.Join(" and ",dictionaryKeysWithParameters.Values));

            return query;
        }

        public string DeleteQuery<TEntity>() {

            Dictionary<string, string> dictionaryKeysWithParameters = new Dictionary<string, string>();

            foreach (var item in GetPrimaryKeysAutoIncrem<TEntity>())
            {
                dictionaryKeysWithParameters.Add(item.Key, item.Key + "=:" + item.Key);
            }
            string query = String.Format("Update from {0} where {1}"
                                        , string.Join(",", GetFields<TEntity>().Values)
                                        , string.Join(" and ", dictionaryKeysWithParameters.Values));

            return query;
        }

        public string GetTableName<T>() {

            string nameTable = "";
            Type entity = typeof(T); 
            PropertyInfo[] prop = entity.GetProperties();

            var attribute = entity.GetCustomAttributes().Where(c => c.GetType().Name.Equals("Table")).FirstOrDefault();

            Table tb = attribute as Table;
            if (tb != null && tb.Name != null)
            {
                nameTable = tb.Name;
            }
            else {
                nameTable = entity.Name;
            }
                return nameTable;
        }

        public Dictionary<string,bool> GetPrimaryKeysAutoIncrem<T>() {
            Dictionary<string, bool> dictionaryPrimaryKeys = new Dictionary<string, bool>();
            Type entity = typeof(T);
            PropertyInfo[] prop = entity.GetProperties();
            foreach (var p in prop)
            {
                var attrPrimaryKey = p.GetCustomAttributes().Where(c => c.GetType().Name.Equals("PrimaryKey")).FirstOrDefault();
                PrimaryKey pk = attrPrimaryKey as PrimaryKey;
                if (pk != null)
                {
                    var attrField = p.GetCustomAttributes().Where(c => c.GetType().Name.Equals("Field")).FirstOrDefault();
                    Field field = attrField as Field;
                    if (field != null)
                    {
                        dictionaryPrimaryKeys.Add(field.Name, pk.AutoIncrement);
                    }
                    else {
                        dictionaryPrimaryKeys.Add(p.Name, pk.AutoIncrement);

                    }
                }
               

            }
                return dictionaryPrimaryKeys;
        }

        public Dictionary<string, string> GetFields<T>() {
            Dictionary<string, string> dictionaryFields = new Dictionary<string, string>();
            Type entity = typeof(T);
            PropertyInfo[] prop = entity.GetProperties();
            foreach (var p in prop)
            {
                var attrField = p.GetCustomAttributes().Where(c => c.GetType().Name.Equals("Field")).FirstOrDefault();
                Field field = attrField as Field;
                if (field != null)
                {
                    dictionaryFields.Add(p.Name, field.Name);
                }
                else {
                    dictionaryFields.Add(p.Name, p.Name);
                }

            }
            return dictionaryFields;
        }
    }
}
