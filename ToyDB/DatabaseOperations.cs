using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
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

        public string @databaseLocation { get; set; }

        String dbPath = @"C:\Users\ot5848\source\repos\ToyDB\ToyDB\ToyDBServer\Databases\";
        //The name of the database
        public string DatabaseName { get; set; }

        //Checks whether or not the input database location and name already exists
        public bool databaseAlreadyExists = false;

       
        public String CreateDatabase(string databaseName)
        {
            //Set the DatabaseName property to the value passed in the parameter
            DatabaseName = databaseName;

            //The location where the database will be stored
            @databaseLocation = dbPath + "\\" + DatabaseName;

            //Create the database folder if it does not exist
            if (!Directory.Exists(@databaseLocation))
            {
                Path.GetFullPath(@databaseLocation);
                Directory.CreateDirectory(@databaseLocation);
                Directory.CreateDirectory(@databaseLocation + "/indexes");
                Directory.CreateDirectory(@databaseLocation + "/sysdb");
                CreateSystable();
                CreateColumns();
                MessageBox.Show("Database successfully created.");
            }

            //Database folder already exists
            else
            {
                databaseAlreadyExists = true;
                MessageBox.Show("Error! database already exists.\n");
            }
            return "test";
        }

       
        public String UseDatabase(string databaseName)
        {
            //The location of the database to use       
            String useDBPath = dbPath + "useDB.txt";
            @databaseLocation = databaseName;
           
            if (File.Exists(@useDBPath))
            {
                File.Delete(@useDBPath);
            }

            try
            {
                using (FileStream fs = File.Create(useDBPath))
                {                   
                     byte[] info = new UTF8Encoding(true).GetBytes(databaseName);
                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }

                Directory.SetCurrentDirectory(dbPath);
                Console.WriteLine("Using database '{0}'", databaseName);
            }

            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine("The specified database does not exist. {0}", e);
            }
            return "test";
        }

        public String getDatabaseName()
        {
            String useDBPath = dbPath+"useDB.txt";
            String result = "";
            try
            {
                // File.OpenRead("textfile.txt");
                string[] lines = File.ReadAllLines(useDBPath);
                foreach (string line in lines)
                {
                    result = line;
                }
            }
            catch (FileNotFoundException e)
            {
                // TODO Auto-generated catch block
            }
            catch (IOException e)
            {
                // TODO Auto-generated catch block
            }
            finally
            {                

            }
            return result;
        }
        public Object DropDatabase(String database)
        {
            String path = dbPath + "\\" + database;           
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
                //drop database
                dir.Delete(true);

            }
            returnString = "database " + database + " dropped";

            return returnString;
        }    

        public String CreateTable(String tableName, String fields) 
        {
		    String returnString = "";
		    String useDB = getDatabaseName();
		    TableController tc = new TableController();
		    tc.AddTableName(dbPath,useDB, tableName);
		    tc.CreateColumns(dbPath,useDB, tableName, fields);
		    tc.ReserveSectorForTable(dbPath,useDB, tableName);
		    returnString = "table " + tableName + " was successfully created";
		    return returnString;
	    }

        public void CreateSystable()
        {
            SysTables st = new SysTables();
            IFormatter formatter = new BinaryFormatter();
            String path = @databaseLocation + "/Sysdb/systable.obj";

            IFormatter ibin = new BinaryFormatter();// create the binary formatter

            Stream strobj = new FileStream(@path, //file location

            FileMode.Create, // create folder

            FileAccess.Write, // write the file

            FileShare.None);

            ibin.Serialize(strobj, st); //written to the file

            strobj.Close();
        }

        public void CreateColumns()
        {
            SysColumns st = new SysColumns();
            IFormatter formatter = new BinaryFormatter();
            String path = @databaseLocation + "/Sysdb/syscolumns.obj";

            IFormatter ibin = new BinaryFormatter();// create the binary formatter

            Stream strobj = new FileStream(@path, //file location

            FileMode.Create, // create folder

            FileAccess.Write, // write the file

            FileShare.None);

            ibin.Serialize(strobj, st); //written to the file

            strobj.Close();
        }

        public String InsertIntoTable(String tableName, String valuesString)
        {
            String returnString = "";
            String useDB = getDatabaseName();
            TableController to = new TableController();
            IFormatter formatter = new BinaryFormatter();
            int currentBlockSize = (to.GetBlock(dbPath, useDB, tableName).Count);
            int lastBlockID = 0;
            if (currentBlockSize < 1)
            {
                lastBlockID = (to.GetBlock(dbPath,useDB, tableName).Count);
            }
            else
            {
                lastBlockID = (to.GetBlock(dbPath, useDB, tableName).Count) - 1;
            }
            String filePath =dbPath+ useDB + "/" + tableName + "/" + lastBlockID + ".obj";
            TableStructure ts = new TableStructure();
            String[] valuesList = valuesString.Split(',');
            ArrayList valuesArray = new ArrayList();
            
            try
            {
                Stream stream = new FileStream(filePath,FileMode.Open,FileAccess.Read);

                if (stream.Length != 0)
                {
                    if (File.Exists(filePath))
                    {

                        ts = (TableStructure)(formatter.Deserialize(stream));

                        Console.WriteLine(ts);
                        //stream.Close();
                    }
                }
                stream.Close();
                foreach (String value in valuesList)
                {
                    valuesArray.Add(value.Trim().Replace("'", "").Trim());
                }

                ts.table.Add(valuesArray);       
                /**
                 * check if block contains maximum number of elements if it does
                 * create a data block by incrementing lastDatablock id by one
                 * otherwise write to the current data block
                 */
                if ((ts.table.Count-1) == 5)
                {
                    ts.table.Clear();
                    ts.table.Add(valuesArray);
                    int nextBlockID = lastBlockID + 1;
                    filePath = dbPath + useDB + "/" + tableName + "/" + nextBlockID + ".obj";
                }
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                stream = new FileStream(filePath, FileMode.Create,FileAccess.Write);
                formatter.Serialize(stream, ts);
                stream.Close();

                returnString = "1 record was inserted";

            }
            catch (Exception e) {
                // TODO Auto-generated catch block
                e.Message.ToString();
            }

            return returnString;

            }


        }
    }

