using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.SQLite;

namespace Cyrez.SqliteUtil
{
    class DataLoader
    {
        public void ImportFromCsv(SQLiteConnection connection, string path, bool firstRowAsColumnName)
        {
            ImportFromCsv(connection, path, firstRowAsColumnName, SqliteUtil.GetValidIdentifier(Path.GetFileNameWithoutExtension(path)));
        }

        public int ImportFromCsv(SQLiteConnection connection, string path, bool firstRowAsColumnName, string tableName)
        {
            var cnt = 0;
                        
            using (var reader = new CsvReader(path))
            using(var insertCmd = GetInsertCommand(connection, reader))
            {
                CreateTable(reader);
                
                while (reader.Read())
                {
                    cnt++;
                    
                    for (var i = 0; i <= reader.FieldCount; i++)
                        insertCmd.Parameters[0].Value = reader[i];
                    
                    if (cnt % 1000 == 0)
                        Console.WriteLine(cnt);
                }
            }
            return cnt;
        }

        public SQLiteCommand GetInsertCommand(SQLiteConnection connection, CsvReader reader)
        {
            var insertCmd = connection.CreateCommand();
            
            insertCmd.CommandText = "INSERT INTO {0} (";

            for (var i = 0; i <= reader.FieldCount; i++)
                insertCmd.CommandText += (i > 0 ? "," : "") + reader.GetName(i);

            insertCmd.CommandText = ") VALUES (";
            
            for (var i = 0; i <= reader.FieldCount; i++)
            {
                insertCmd.CommandText += i > 0 ? ", :" + i : ":" + i;
                insertCmd.Parameters.Add(i.ToString(), System.Data.DbType.String);
            }

            return insertCmd;
        }

        private void CreateTable(CsvReader reader)
        {
            throw new NotImplementedException();
        }

    }
}
