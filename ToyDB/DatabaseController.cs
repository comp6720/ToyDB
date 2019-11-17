using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyDB
{
    class DatabaseController
    {
        String databasePath;
        String databaseName;

        public String getDatabasePath()
        {
            return databasePath;
        }

        public void setDatabasePath(String databasePath)
        {
            this.databasePath = databasePath;
        }
        public String getDatabaseName()
        {
            String currentDB = "currentdb.txt";
            String line = "";
            try
            {
                string[] lines = File.ReadAllLines(@"C:\Users\Public\TestFolder\WriteLines2.txt");
                //line = br.readLine();

                //br.close();


            }
            catch (FileNotFoundException e)
            {
                // TODO Auto-generated catch block
               // e.printStackTrace();
            }
            catch (IOException e)
            {
                // TODO Auto-generated catch block
               // e.printStackTrace();
            }
            finally
            {
                // return line;

            }
            return line;

        }
        public String  createTable(String tableName, String fields)
        {
            String returnString = "";
            //String useDB = getDatabaseName();
            ////TableOperations to = new TableOperations();
            //to.addTableNameToSysTable(useDB, tableName);
            //to.createColumns(useDB, tableName, fields);
            //to.reserveSectorForTable(useDB, tableName);
            //returnString = "table " + tableName + " was successfully created";
            return returnString;
        }

    }
}
