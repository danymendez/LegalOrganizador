using Common.Entity.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Common.Data.SQLBuilders
{
    public class SqlQueryBuilder
    {
        public TablesAttributes tablesAttributesWithEncloseSign { get; set; }
        public TablesAttributes tablesAttributesWithParamSign { get; set; }
        public string InsertQuery<T>() {

            TableAttributeBindName<T>("\"", ":");
            string primaryKeyName = tablesAttributesWithEncloseSign.PrimaryKeyAutoIncrementDict
                                    .Where(c=>c.Value==true)
                                    .Select(c => c.Key).FirstOrDefault();
            string query = String.Format("insert into {0} ({1}) values ({2}) RETURNING {3} into :my_id_param"
                                            , GetTableName<T>()
                                            , string.Join(", ", 
                                                tablesAttributesWithEncloseSign.FieldsDict
                                                    .Where(c => !c.Key.Equals(primaryKeyName))
                                                    .Select(c=>c.Value))
                                            , string.Join(", ", 
                                                tablesAttributesWithParamSign.FieldsDict
                                                    .Where(c => !c.Key.Equals(primaryKeyName))
                                                    .Select(c => c.Value))
                                            , string.Join("", 
                                                tablesAttributesWithEncloseSign.FieldsDict
                                                    .Where(c => c.Key.Equals(tablesAttributesWithEncloseSign.PrimaryKeyNameDict.Keys.FirstOrDefault()))
                                                    .Select(c=>c.Value)));

            return query;
        }

        public string UpdateQuery<TEntity>() {
            TableAttributeBindName<TEntity>("", ":");
            string primaryKeyName = tablesAttributesWithEncloseSign.PrimaryKeyAutoIncrementDict
                                    .Where(c => c.Value == true)
                                    .Select(c => c.Key).FirstOrDefault();
            string query = String.Format("UPDATE {0} SET {1} WHERE {2}"
                                            , "\""+tablesAttributesWithEncloseSign.TableName+"\""
                                            , string.Join(", ", tablesAttributesWithEncloseSign.FieldsDict
                                                    .Where(c => !c.Key.Equals(primaryKeyName)).Select(c => "\""+c.Value+"\"=:"+c.Value))
                                            , string.Join(" and ", tablesAttributesWithEncloseSign.FieldsDict
                                                    .Where(c => c.Key.Equals(tablesAttributesWithEncloseSign.PrimaryKeyNameDict.Keys.FirstOrDefault()))
                                                    .Select(c => "\""+c.Value+"\""+"=:"+c.Value)));

            return query;
        }

        public string SelectAllQuery<T>() {
            TableAttributeBindName<T>("\"", ":");
            string query = String.Format("Select {0} from {1}"
                                            , string.Join(",", tablesAttributesWithEncloseSign.FieldsDict.Values)
                                            , tablesAttributesWithEncloseSign.TableName);
            return query;
        }

        public string SelectQuery<TEntity>() {
            TableAttributeBindName<TEntity>("", ":");
            string query = String.Format("Select {0} from {1} where {2}"
                                        , string.Join(",",tablesAttributesWithEncloseSign.FieldsDict.Select(c => "\"" + c.Value + "\""))
                                        , tablesAttributesWithEncloseSign.TableName
                                        , string.Join(" and ", tablesAttributesWithEncloseSign.FieldsDict
                                                    .Where(c => c.Key.Equals(tablesAttributesWithEncloseSign.PrimaryKeyNameDict.Keys.FirstOrDefault()))
                                                    .Select(c => "\"" + c.Value + "\""+"=:"+c.Value)));

            return query;
        }

        public string DeleteQuery<TEntity>() {

            TableAttributeBindName<TEntity>("\"", ":");
            string query = String.Format("Delete from {0} where {1}"
                                        , tablesAttributesWithEncloseSign.TableName
                                        , string.Join(" and ", tablesAttributesWithEncloseSign.FieldsDict
                                                    .Where(c => c.Key.Equals(tablesAttributesWithEncloseSign.PrimaryKeyNameDict.Keys.FirstOrDefault()))
                                                    .Select(c => c.Value + "=:" + c.Value)));

            return query;
        }

        public string GetTableName<T>() {

            string nameTable = "";
            Type entity = typeof(T);
            PropertyInfo[] prop = entity.GetProperties();

            var attribute = entity.GetCustomAttributes().Where(c => c.GetType().Name.Equals("Table")).FirstOrDefault();

            if (attribute is Table tb && tb.Name != null)
            {
                nameTable = tb.Name;
            }
            else
            {
                nameTable = entity.Name;
            }
            return nameTable;
        }

        public Dictionary<string, bool> GetPrimaryKeysAutoIncrem<T>() {
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


        public void TableAttributeBindName<T>(string enclose, string signParam)
        {
            tablesAttributesWithEncloseSign = new TablesAttributes();
            tablesAttributesWithEncloseSign.FieldsDict = new Dictionary<string, string>();
            tablesAttributesWithEncloseSign.PrimaryKeyNameDict = new Dictionary<string, string>();
            tablesAttributesWithEncloseSign.PrimaryKeyAutoIncrementDict = new Dictionary<string, bool>();

            tablesAttributesWithParamSign = new TablesAttributes();

            tablesAttributesWithParamSign.FieldsDict = new Dictionary<string, string>();
            tablesAttributesWithParamSign.PrimaryKeyNameDict = new Dictionary<string, string>();
            tablesAttributesWithParamSign.PrimaryKeyAutoIncrementDict = new Dictionary<string, bool>();

            Type entity = typeof(T);

            var attribute = entity.GetCustomAttributes().Where(c => c.GetType().Name.Equals("Table")).FirstOrDefault();

            if (attribute is Table tb && tb.Name != null)
            {
                tablesAttributesWithEncloseSign.TableName = enclose + tb.Name + enclose;
            }
            else
            {
                tablesAttributesWithEncloseSign.TableName = enclose + entity.Name + enclose;
            }
            
            PropertyInfo[] prop = entity.GetProperties();

            foreach (var p in prop)
            {
                var attrField = p.GetCustomAttributes();

                int contadorAttrValidos = 0;
                foreach (var item in attrField) {
                    string name = item.GetType().Name;
                    switch (name) {
                        case "Field":
                            Field field = item as Field;
                                tablesAttributesWithEncloseSign.FieldsDict[p.Name]= enclose + field.Name + enclose;
                                tablesAttributesWithParamSign.FieldsDict[p.Name]= signParam + field.Name;
                                contadorAttrValidos++;
                            
                            break;
                        case "PrimaryKey":
                            PrimaryKey pk = item as PrimaryKey;
                            tablesAttributesWithEncloseSign.PrimaryKeyNameDict.Add(p.Name, enclose + p.Name + enclose);
                            tablesAttributesWithParamSign.PrimaryKeyNameDict.Add(p.Name, signParam + p.Name);
                            tablesAttributesWithEncloseSign.PrimaryKeyAutoIncrementDict.Add(p.Name, pk.AutoIncrement);
                            break;
                        default:
                          
                          
                            break;
                    }

                }
                if (contadorAttrValidos == 0)
                {
                    tablesAttributesWithEncloseSign.FieldsDict.Add(p.Name, enclose + p.Name + enclose);
                    tablesAttributesWithParamSign.FieldsDict.Add(p.Name, signParam + p.Name);
                }
            }
        }
   }


    public class TablesAttributes{

        public string TableName { get; set; }

        public Dictionary<string, string> FieldsDict { get; set; }

        public Dictionary<string, string> PrimaryKeyNameDict { get; set; }

        public Dictionary<string, bool> PrimaryKeyAutoIncrementDict { get; set; }

    }
}
