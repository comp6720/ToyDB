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
            String useDBPath = dbPath + "useDB.txt";
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
            tc.AddTableName(dbPath, useDB, tableName);
            tc.CreateColumns(dbPath, useDB, tableName, fields);
            tc.ReserveSectorForTable(dbPath, useDB, tableName);
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
                lastBlockID = (to.GetBlock(dbPath, useDB, tableName).Count);
            }
            else
            {
                lastBlockID = (to.GetBlock(dbPath, useDB, tableName).Count) - 1;
            }
            String filePath = dbPath + useDB + "/" + tableName + "/" + lastBlockID + ".obj";
            TableStructure ts = new TableStructure();
            String[] valuesList = valuesString.Split(',');
            ArrayList valuesArray = new ArrayList();

            try
            {
                Stream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

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
                if ((ts.table.Count - 1) == 5)
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

                stream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
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
        // Select values from specified table

        public String DeleteRecord(string tableName, string whereString)
        {
            //Check if a database is selected
            if (dbPath != null)
            {
                string filePath = dbPath + "\\" + "mydb" + "\\" + tableName;
                TableStructure ts = new TableStructure();

                string[] operatorArray = { "<", ">", "=", "!=" };

                //Variables to store where conditions
                string column = "";
                string chosenOperator = "";
                string value = "";

                //Loop through the operators array and check if the operator in the whereString exists in the array
                foreach (string ioperator in operatorArray)
                {
                    if (whereString.Contains(ioperator))
                    {
                        chosenOperator = ioperator;
                    }
                }

                //Get the column to traverse
                column = whereString.Split(new string[] { chosenOperator }, StringSplitOptions.None)[0].Trim();

                //Get the value to delete based on
                value = whereString.Split(new string[] { chosenOperator }, StringSplitOptions.None)[1].Trim();


                try
                {
                    //Loop through all data in the table
                    foreach (string block in Directory.EnumerateFiles(filePath, "*.obj", SearchOption.TopDirectoryOnly))
                    {
                        IFormatter formatter = new BinaryFormatter();
                        Stream stream = new FileStream(block, FileMode.Open, FileAccess.Read);

                        //Deserialize the block
                        if (stream.Length != 0)
                        {
                            if (File.Exists(block))
                            {
                                ts = (TableStructure)(formatter.Deserialize(stream));
                            }
                        }
                        stream.Close();

                        using (StreamReader reader = new StreamReader(block))
                        {
                            while (reader.ReadLine() != null)
                            {
                                ts.table.Add(reader.ReadLine());

                            }
                        }

                        foreach (ArrayList item in ts.table)
                        {
                            var strings = item.Cast<string>().ToArray();
                            string test = string.Join(",", strings);
                            Console.WriteLine(test);
                        }
                    }
                }

                catch (IOException e)
                {
                    Console.WriteLine("IOException: {0}", e);
                }
            }

            //No database selected
            else
            {
                Console.WriteLine("Error! No database selected.");
            }
            return "true";
        }

        public ArrayList SelectTableValues(String tableName, String fieldsString, dynamic whereField)
        {
            String useDB = getDatabaseName();
            IFormatter formatter = new BinaryFormatter();
            TableController to = new TableController();          
          // IndexStructure ix = null;
            TableStructure ts = null;
            // TableOperations to = new TableOperations();
            List<String> whereItemField = new List<string>();
            ArrayList whereFieldPosition = null;
            String indexFieldName = "";
            List<String> whereItemFieldList = null;

            if (!(String.IsNullOrEmpty(whereField)))
            {
                whereItemFieldList = OperatorSplit(whereField);

                whereItemField.Add(whereItemFieldList[0]);
                String[] whereFieldsArray = new String[whereItemField.Count()];
                whereFieldsArray = whereItemField.ToArray();

                whereFieldPosition = to.findValuePosition(dbPath, useDB, tableName, whereFieldsArray);
               
            }

            ArrayList returnRecords = new ArrayList();
            ArrayList columnPositions = new ArrayList();
            String[] columnNames = fieldsString.Split(',');
            columnPositions = to.findValuePosition(dbPath, useDB, tableName, columnNames);
            int blockPointer = 0;
            int recordPointer = 0;

            ArrayList blocks = new ArrayList();

            if ((indexFieldName.Length == 0) && whereField != null)
            {
                blocks = to.GetBlock(dbPath, useDB, tableName);
            }
            else
            {
                blocks.Add(blockPointer);
            }

            foreach (int block in blocks)
            {

                String filePath = dbPath + useDB + "/" + tableName + "/" + block + ".obj";
                try
                {
                    Stream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

                    ts = (TableStructure)(formatter.Deserialize(stream));
                    stream.Close();
                }
                catch (IOException i)
                {
                    Console.Write(i.Message.ToString());
                }

                if ((indexFieldName.Length == 0) || (indexFieldName.Equals(null)))
                {
                    ts.table = ts.table;
                }
                else
                {
                    //ArrayList myRow = new ArrayList();
                    var myRow = ts.table[recordPointer];
                    ts.table = null;
                    ts.table.Add(myRow);
                }

                foreach (ArrayList row in ts.table)
                {
                    ArrayList returnRows = new ArrayList();
                    //foreach (int columnPosition in columnPositions)
                    //{
                    if (!String.IsNullOrEmpty(whereField))
                    {
                        string ind = whereFieldPosition[0].ToString();
                        string rows = row[int.Parse(ind)].ToString();
                        string field = whereItemFieldList[1].ToString().TrimEnd(';');
                        string newWhereField = whereField.ToString().TrimEnd(';');

                        if (Operators(newWhereField, rows, field))
                        {
                            returnRows.Add(row);
                            returnRecords.Add(returnRows);
                        }
                    }
                    else
                    {
                        returnRows.Add(row);
                        returnRecords.Add(returnRows);
                    }

                    //}
                    //returnRecords.Add(returnRows);

                }

            }
            returnRecords =SetColumnPosition(columnPositions, returnRecords);
            return returnRecords;
        }



		
	

    public List<String> OperatorSplit(String whereFields)
        {
            List<String> whereColumnValue = new List<string>();
            if (whereFields.Contains("!="))
            {
                whereColumnValue = whereFields.Split('=').ToList();
            }
            else if (whereFields.Contains("<"))
            {
                whereColumnValue = (whereFields.Split('<')).ToList();
            }
            else if (whereFields.Contains(">"))
            {
                whereColumnValue = (whereFields.Split('>')).ToList();
            }
            else if (whereFields.Contains("="))
            {
                whereColumnValue = whereFields.Split('=').ToList();
            }
            return whereColumnValue;
        }
        public Boolean Operators(String whereString, String fieldValue, String value)
        {
            Boolean isCompare = false;
            if (whereString.Contains("!="))
            {

                if (value.Contains("'"))
                {
                    isCompare = !(fieldValue.Trim().Equals(value.Replace("'", "").Trim()));
                }
                else
                {
                    isCompare = int.Parse(fieldValue.Trim()) != int.Parse(value.Trim());
                }

            }
            else if (whereString.Contains("<"))
            {
                if (!value.Contains("'"))
                {
                    isCompare = int.Parse(fieldValue.Trim()) < int.Parse(value.Trim());
                }

            }
            else if (whereString.Contains(">"))
            {
                if (!value.Contains("'"))
                {
                    isCompare = int.Parse(fieldValue.Trim()) > int.Parse(value.Trim());
                }
            }
            else if (whereString.Contains("="))
            {
                if (value.Contains("'"))
                {
                    isCompare = (fieldValue.Trim().Equals(value.Replace("'", "").Trim()));
                }
                else
                {
                    isCompare = int.Parse(fieldValue.Trim()) == int.Parse(value.Trim());
                }
            }
            return isCompare;
        }
        public ArrayList SetColumnPosition(ArrayList position, ArrayList records)
        {
            ArrayList arrayRecords = new ArrayList();
            foreach (ArrayList record in records)
            {
                ArrayList TempList = new ArrayList();
                foreach (ArrayList recordItem in record)
                {
                    foreach (int pos in position)
                    {
                        var items = recordItem[pos];
                        TempList.Add(items);

                    }
                }
                arrayRecords.Add(TempList);
            }
    
            return arrayRecords;
        }

    }
}
