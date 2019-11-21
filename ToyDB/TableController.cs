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
        public void AddTableName(String database, String tableName)
        {
            SysTables st = new SysTables();
            IFormatter formatter = new BinaryFormatter();
            String path = @"C:\Users\ot5848\source\repos\ToyDB\ToyDB\ToyDBServer\Databases\"+ database +"/ sysdb/systable.ser";           
            using (FileStream fs = File.Create(path))
            {
                byte[] info = new UTF8Encoding(true).GetBytes(database);
                // Add some information to the file.
                fs.Write(info, 0, info.Length);
            }
            Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            //String path = database+"/sysdb/systable.ser";

            try
            {
                if (File.Exists(path))
                {
                    st = (SysTables)(formatter.Deserialize(stream));

                    Console.WriteLine(st);
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
        public void CreateColumns(String database, String tableName, String fields)
        {
            IFormatter formatter = new BinaryFormatter();
            SysColumns sc = new SysColumns();
            String[] fieldslist = fields.Replace(")", "").Split(',');
            String path = database + "/sysdb/syscolumns.ser/";
            Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            try
            {
                if (File.Exists(path))
                {
                    sc = (SysColumns)(formatter.Deserialize(stream));
                    Console.WriteLine(sc);
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
        public void reserveSectorForTable(String databaseName, String sectorName)
        {
            TableStructure ts = new TableStructure();
            String path = databaseName + "/" + sectorName;
            IFormatter formatter = new BinaryFormatter();
            var dir = new DirectoryInfo(path);
            //drop database
            dir.Delete(true);
            Directory.CreateDirectory(path);
            try
            {
                if (File.Exists(path))
                {
                    Stream stream = new FileStream(path + 0 + ".ser", FileMode.Create, FileAccess.Write);
                    formatter.Serialize(stream, ts);
                    stream.Close();
                }
            }
            catch (IOException i)
            {

            }

        }

        public bool ValidTableName(String database, String tableName)
        {
            IFormatter formatter = new BinaryFormatter();
            String path = database + @"c:\ToyDB\ToyDB\ToyDBServer\Databases\sysdb\systable.ser";
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
    }
}
