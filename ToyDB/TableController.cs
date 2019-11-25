using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ToyDB
{
    class TableController
    {
        public void AddTableName(String dbPath,String database, String tableName)
        {
            SysTables st = new SysTables();
            IFormatter formatter = new BinaryFormatter();
            String path = dbPath+database + "/Sysdb/systable.obj";

            Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            try
            {
                if (File.Exists(path))
                {
                    st = (SysTables)(formatter.Deserialize(stream));
                    
                    Console.WriteLine(st);
                    stream.Close();
                }
                st.sys_table.Add(tableName);

                // remove file with new file of the same name if file exists
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                //IFormatter formatter = new BinaryFormatter();
                stream = new FileStream(path, FileMode.Create, FileAccess.Write);
                formatter.Serialize(stream, st);
                stream.Close();
            }
            catch (IOException i)
            {

            }
        }
        public void CreateColumns(String dbPath, String database, String tableName, String fields)
        {
            IFormatter formatter = new BinaryFormatter();
            SysColumns sc = new SysColumns();
            String[] fieldslist = fields.Replace(")", "").Split(',');
            String path = dbPath + database + "/sysdb/syscolumns.obj";
            Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            try
            {
                if (File.Exists(path))
                {
                    sc = (SysColumns)(formatter.Deserialize(stream));
                    Console.WriteLine(sc);
                    stream.Close();
                }
                foreach (String field in fieldslist)
                {
                    ArrayList col = new ArrayList();
                    String[] field_def = field.Trim().Split(' ');
                    String field_name = field_def[0];
                    String field_type = field_def[1];
                    col.Add(tableName);
                    col.Add(field_name);
                    col.Add(field_type);
                    col.Add(null);
                    sc.sys_column.Add(col);

                }
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                stream = new FileStream(path, FileMode.Create, FileAccess.Write);
                formatter.Serialize(stream, sc);
                stream.Close();

            }
            catch (IOException i)
            {

            }
            catch (Exception e)
            {
                // TODO Auto-generated catch block
            }
        }

        // reserve area to store i.e. sector to store blocks containing 5 records
        public void ReserveSectorForTable(String dbPath, String databaseName, String sectorName)
        {
            TableStructure ts = new TableStructure();
            String path = dbPath + databaseName + "/" + sectorName;          
            Directory.CreateDirectory(path);
            IFormatter formatter = new BinaryFormatter();
            try
            {
              Stream stream = new FileStream(path +"/"+ 0 + ".obj", FileMode.Create, FileAccess.Write);
              formatter.Serialize(stream, ts);
              stream.Close();
            }
            catch (IOException i)
            {

            }

        }

        public bool ValidTableName(String dbPath,String database, String tableName)
        {
            IFormatter formatter = new BinaryFormatter();
            String path = dbPath + database + "/sysdb/systable.obj";
            Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            SysTables st = null;
            bool validTable = false;
            try
            {
                st = (SysTables)(formatter.Deserialize(stream));
                Console.WriteLine(st);
                validTable = st.sys_table.Contains(tableName);               
                stream.Close();
            }
            catch (IOException)
            {

             return validTable;

            }
            return validTable;

        }

        public ArrayList GetBlock(String dbPath,String databaseName, String tableName)
        {

            String path = dbPath + databaseName + "/" + tableName;
            ArrayList blockIDs = new ArrayList();
           // string[] filePaths = Directory.GetFiles(@"c:\MyDir\");
           // File[] files = new File(databaseName + "/" + tableName).listFiles();
          
            foreach (string FileFound in Directory.GetFiles(path))
            {
                string blockName = (Path.GetFileName(FileFound)).Split(new string[] { "\\." }, StringSplitOptions.None)[0].Trim();             
                int blockID = Int32.Parse(blockName.Split('.')[0].Trim());
                blockIDs.Add(blockID);                
            }
            blockIDs.Sort(null);
            return blockIDs;
        }
         
	public ArrayList findValuePosition(String dbPath,String database, String tableName, String[] columnNames)
        {
            SysColumns sc = null;
            // Integer position = 0;
            IFormatter formatter = new BinaryFormatter();
            ArrayList columnPositions = new ArrayList();
            if (ValidTableName(dbPath,database, tableName))
            {
                try
                {
                    Stream stream = new FileStream(dbPath + database + "/sysdb/syscolumns.obj",FileMode.Open,FileAccess.Read);
                    //ObjectInputStream ois = new ObjectInputStream(fis);
                    sc = (SysColumns)(formatter.Deserialize(stream));
                
                    for (int i = 0; i < columnNames.Length; i++)
                    {
                        String columnName = columnNames[i].Trim();
                        int j = 0;
                        foreach (ArrayList columns in sc.sys_column)
                        {

                            if (columns.Contains(tableName))
                            {

                                if (columns.Contains(columnName))
                                {
                                    columnPositions.Add(j);
                                }
                                j = j + 1;

                            }
                        }
                    }                   
                    stream.Close();
                }
                catch (IOException i) {
                   
                }
                }

                return columnPositions;

            }
        }
}
