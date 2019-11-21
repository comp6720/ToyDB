using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyDB
{
    public class SQLParser
    {
        
        public static void SqlRouteCommand(String sqlStatement)
        {
            DatabaseOperations db = new DatabaseOperations();
            Object returnedObject = null;
            //  XmlOperations xml_ops = new XmlOperations();
            String[] crud = sqlStatement.Split(' ');
            String route = crud[0];
            String createType = crud[1];
            String name = null;
            if ((crud.Length > 2))
            {
                name = crud[2];
            }

            switch (route.ToLower())
            {
                case "delete":
                    if (sqlStatement.Contains(" where "))
                    {
                        String whereString = sqlStatement.Split(new string[] { "where" }, StringSplitOptions.None)[1].Trim(); 
                        String statementWithoutWhereClause = sqlStatement.Split(new string[] { "where" }, StringSplitOptions.None)[1].Trim(); 
                        String tableNamea = statementWithoutWhereClause.Split(new string[] { "from" }, StringSplitOptions.None)[1].Trim();
                        returnedObject = (tableNamea,(whereString));
                    }
                    else
                    {
                        String tableName2 = sqlStatement.Split(new string[] { "from" }, StringSplitOptions.None)[1].Trim();
                        returnedObject = (tableName2, "better");
                    }

                    break;
                case "select":
                    String tableName = sqlStatement.Split(new string[] { "from" }, StringSplitOptions.None)[1].Trim();
                    String fieldsStringWithSelect = sqlStatement.Split(new string[] { "from" }, StringSplitOptions.None)[0].Trim();
                    String fieldsString = fieldsStringWithSelect.Replace("select", "");
                    if (sqlStatement.Contains("where"))
                    {
                        String whereString = sqlStatement.Split(new string[] {"where" }, StringSplitOptions.None)[1].Trim();
                        tableName = tableName.Split(' ')[0];
                        returnedObject = (tableName, fieldsString, (whereString));
                    }
                    else
                    {
                        returnedObject = (tableName, fieldsString, " ");
                    }

                    break;
                case "insert":
                    String valuesString = sqlStatement.Replace(")", "").Split(new string[] {"\\\\("}, StringSplitOptions.None)[1].Trim();
                    returnedObject = (name, valuesString);
                    break;
                case "update":
                    String tableName1 = sqlStatement.Split(' ')[1].Trim();
                    String takeSetString = sqlStatement.Split(new string[] {"set"}, StringSplitOptions.None)[1].Trim();
                    if (sqlStatement.Contains("where"))
                    {
                        String setString = takeSetString.Split(new string[] {"where"}, StringSplitOptions.None)[0].Trim();
                        String whereString1 = sqlStatement.Split(new string[] {"where"}, StringSplitOptions.None)[1].Trim();
                        returnedObject = (tableName1, setString, (whereString1));
                    }
                    else
                    {
                        returnedObject = (tableName1, takeSetString, " " );
                    }

                    break;
              
                case "create":
                    if (createType.ToLower().Equals("index"))
                    {
                        // CREATE INDEX ON indexName Employee(eid)
                        String stringWithIndexName = sqlStatement.Split(new string[] {"on"}, StringSplitOptions.None)[1].Trim();
                        String[] indexNameTableField = stringWithIndexName.Split(' ');
                        String indexName = indexNameTableField[0].Trim();
                        String[] nameTableField = indexNameTableField[0].Split(new string[] { "\\\\(" }, StringSplitOptions.None);
                        String indexTableName = nameTableField[0].Trim();
                        String indexField = nameTableField[1].Replace(")", "");
                        returnedObject = (indexTableName, indexName, indexField);
                        //  "createIndex".toString();
                    }
                    else if (createType.ToLower().Equals("table"))
                    {
                        String fields_string = sqlStatement.Split(new string[] {"("}, StringSplitOptions.None)[1].Trim();
                        returnedObject = db.CreateTable(name, fields_string);
                    }
                    else if (createType.ToLower().Equals("database"))
                    {
                        returnedObject = db.CreateDatabase(name);
                    }

                    break;
                case "use":
                         returnedObject = db.UseDatabase(createType);
                    break;

                case "drop":
                    if (createType.ToLower().Equals("database"))
                    {
                        String databaseName = sqlStatement.Split(' ')[2];                  

                        returnedObject = db.DropDatabase(databaseName);
                    }

                    break;

                default:
                    // "not amount the valid possibilies";
                    break;
                  
            }
            //return returnedObject;
        }
    }
}
