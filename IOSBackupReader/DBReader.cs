using Claunia.PropertyList;
using NSKeyedUnarchiver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

namespace IOSBackupReader
{
    public static class DBReader
    {
        const string SelectSql = "SELECT fileID,domain,relativePath,file FROM Files";
        const string WhereClause = " WHERE domain=?";

        public static List<MBFile> GetFiles(string backupDir, string inDomain = null)
        {
            var files = new List<MBFile>();
            using (var conn = Connect(Path.Combine(backupDir, "Manifest.db")))
            using (var cmd = GetSqlCommand(conn, backupDir, inDomain))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var fileID = reader.GetString(0);
                    var domain = reader.GetString(1);
                    var relativePath = reader.GetString(2);
                    MBFile file;
                    using (var fileBlob = reader.GetStream(3))
                    {
                        file = ReadFileBlob(fileBlob);
                    }
                    file.FileID = fileID;
                    file.Domain = domain;
                    file.RelativePath = relativePath;
                    files.Add(file);
                }
            }
            return files;
        }

        private static SQLiteConnection Connect(string path)
        {
            var builder = new SQLiteConnectionStringBuilder
            {
                DataSource = path,
                Version = 3,
            };
            var conn = new SQLiteConnection(builder.ConnectionString);
            conn.Open();
            return conn;
        }

        private static SQLiteCommand GetSqlCommand(SQLiteConnection conn, string backupDir, string domain = null)
        {
            var cmd = new SQLiteCommand(SelectSql, conn);
            if (domain != null)
            {
                cmd.CommandText += WhereClause;
                cmd.Parameters.AddWithValue(null, domain);
            }
            return cmd;
        }

        private static MBFile ReadFileBlob(Stream blob)
        {
            var dict = Unarchiver.DeepParse(PropertyListParser.Parse(blob));
            var mbFile = new MBFile();
            DictUtil.TryGet(dict, "RelativePath", out mbFile.RelativePath);
            DictUtil.TryGet(dict, "Target", out mbFile.Target);
            DictUtil.TryGet(dict, "Mode", out mbFile.Mode);
            DictUtil.TryGet(dict, "InodeNumber", out mbFile.InodeNumber);
            DictUtil.TryGet(dict, "UserID", out mbFile.UserID);
            DictUtil.TryGet(dict, "GroupID", out mbFile.GroupID);
            DictUtil.TryGet(dict, "LastModified", out mbFile.LastModified);
            DictUtil.TryGet(dict, "LastStatusChange", out mbFile.LastStatusChange);
            DictUtil.TryGet(dict, "Birth", out mbFile.Birth);
            DictUtil.TryGet(dict, "Size", out mbFile.Size);
            DictUtil.TryGet(dict, "ProtectionClass", out mbFile.ProtectionClass);
            return mbFile;
        }
    }
}
