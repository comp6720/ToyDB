using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToyDB
{
    class DatabaseOperations
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

        public string @Location { get; set; }

        String dLocation = @"C:\Users\ot5848\source\repos\ToyDB\ToyDB\ToyDBServer\Databases";
        //The name of the database
        public string DatabaseName { get; set; }

        //Checks whether or not the input database location and name already exists
        public bool databaseAlreadyExists = false;

        /**
         * Create a new database folder
         *
         * @param string databaseName - The name of the database.
         *
         * @return void
        **/
        public String CreateDatabase(string databaseName)
        {
            //Set the DatabaseName property to the value passed in the parameter
            DatabaseName = databaseName;

            //The location where the database will be stored
            string path = dLocation + "\\" + DatabaseName;

            //Create the database folder if it does not exist
            if (!Directory.Exists(path))
            {
                Path.GetFullPath(path);
                Directory.CreateDirectory(path);

                MessageBox.Show("Database successfully created.");
            }

            //Database folder already exists
            else
            {
                databaseAlreadyExists = true;
                MessageBox.Show("Error! That folder already exists.\n");
            }
            return "test";
        }

        /**
            * Navigates to the specified database folder
        * @param string databaseName - The name of the database.
        *
        * @return void
           *
        **/
        public String UseDatabase(string databaseName)
        {
            //The location of the database to use
            string path = dLocation + "\\" + databaseName;

            try
            {
                Directory.SetCurrentDirectory(path);
                Console.WriteLine("Using database '{0}'", databaseName);
            }

            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine("The specified database does not exist. {0}", e);
            }
            return "test";
        }

        public Object DropDatabase(String database)
        {
           String path = dLocation + "\\" + database;
            
            //File file = new File(database);
            String returnString = "";
            if (path.Contains(database))
            {
                if ((path != null))
                {
                    foreach (string FileFound in Directory.GetFiles(path))
                        File.Delete(FileFound);
                }
                //call delete to delete files and empty directory
                var dir = new DirectoryInfo(path);
                dir.Delete(true);

            }
            returnString = "database " + database + " dropped";

            return returnString;
        }
        public String CreateTable(String tableName, String fields)
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

