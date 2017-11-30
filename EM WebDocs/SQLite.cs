using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using System.Text.RegularExpressions;

//Creates and manages database connection to SQLite file for LF Export
namespace EM_WebDocs
{
    class SQLite
    {
        private static string SQL_PATH = Directory.GetParent(Application.CommonAppDataPath).ToString() + "\\EMDocsDB.sqlite";
        private static string ID_PATH = Directory.GetParent(Application.CommonAppDataPath).ToString() + "\\EMDocsDBID.sqlite";

        public static List<Tuple<int, string, string, string, string, string, string>> GetProj(string table)
        {
            try
            {
                SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=" + SQL_PATH + ";Version=3;");
                m_dbConnection.Open();

                List<Tuple<int, string, string, string, string, string, string>> list = new List<Tuple<int, string, string, string, string, string, string>>();

                string sql = "select * from [" + table + "] order by ID";
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(Tuple.Create(Int32.Parse(reader["ID"].ToString()), reader["Keyword"].ToString(), reader["Name"].ToString(), reader["Aliases"].ToString(), reader["ProjType"].ToString(), reader["Developer"].ToString(), reader["Contact"].ToString()));
                }

                m_dbConnection.Close();
                return list;
            }
            catch (System.Data.SQLite.SQLiteException sE)
            {
                Log.AddMessage(sE.ToString(), "Error");
                MessageBox.Show(sE.ToString());
                return null;
            }
        }

        public static List<Tuple<int, string, string>> GetAgency(string table)
        {
            try
            {
                SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=" + SQL_PATH + ";Version=3;");
                m_dbConnection.Open();

                List<Tuple<int, string, string>> list = new List<Tuple<int, string, string>>();

                string sql = "select * from [" + table + "] order by ID";
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(Tuple.Create(Int32.Parse(reader["ID"].ToString()), reader["Keyword"].ToString(), reader["Agency"].ToString()));
                }

                m_dbConnection.Close();
                return list;
            }
            catch (System.Data.SQLite.SQLiteException sE)
            {
                Log.AddMessage(sE.ToString(), "Error");
                MessageBox.Show(sE.ToString());
                return null;
            }
        }

        public static List<Tuple<string, string, string>> GetOfficials(string table)
        {
            try
            {
                SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=" + SQL_PATH + ";Version=3;");
                m_dbConnection.Open();

                List<Tuple<string, string, string>> list = new List<Tuple<string, string, string>>();

                string sql = "select distinct [dbo.key_officials_ocrtext].offid,[dbo.key_officials_ocrtext].keyword, [dbo.list_officials].fullname FROM [dbo.key_officials_ocrtext] inner join[dbo.list_officials] on[dbo.list_officials].offid = [dbo.key_officials_ocrtext].offid order by[dbo.key_officials_ocrtext].offid asc";
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(Tuple.Create(reader["OffID"].ToString(), reader["Keyword"].ToString(), reader["FullName"].ToString()));
                }

                m_dbConnection.Close();
                return list;
            }
            catch (System.Data.SQLite.SQLiteException sE)
            {
                Log.AddMessage(sE.ToString(), "Error");
                MessageBox.Show(sE.ToString());
                return null;
            }
        }

        public static List<Tuple<int, string, string>> GetDocType(string table)
        {
            try
            {
                SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=" + SQL_PATH + ";Version=3;");
                m_dbConnection.Open();

                List<Tuple<int, string, string>> list = new List<Tuple<int, string, string>>();

                string sql = "select * from [" + table + "] order by ID";
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(Tuple.Create(Int32.Parse(reader["ID"].ToString()), reader["Keyword"].ToString(), reader["DocType"].ToString()));
                }

                m_dbConnection.Close();
                return list;
            }
            catch (System.Data.SQLite.SQLiteException sE)
            {
                Log.AddMessage(sE.ToString(), "Error");
                MessageBox.Show(sE.ToString());
                return null;
            }
        }

        public static List<string> GetTypeDet(string table, string agency, string doctype)
        {
            try
            {
                SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=" + SQL_PATH + ";Version=3;");
                m_dbConnection.Open();

                List<string> list = new List<string>();

                string sql = "select * from [" + table + "] where Agency = \"" + agency + "\" AND DocType = \"" + doctype + "\""; //"select * from [" + table + "] order by ID";
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(reader["Keyword"].ToString());
                }

                m_dbConnection.Close();
                return list;
            }
            catch (System.Data.SQLite.SQLiteException sE)
            {
                Log.AddMessage(sE.ToString(), "Error");
                MessageBox.Show(sE.ToString());
                return null;
            }
        }

        public static List<string> GetVotes(string table, string agency, string doctype)
        {
            try
            {
                SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=" + SQL_PATH + ";Version=3;");
                m_dbConnection.Open();

                List<string> list = new List<string>();

                string sql = "select * from [" + table + "] where Agency = \"" + agency + "\" AND DocType = \"" + doctype + "\""; //"select * from [" + table + "] order by ID";
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(reader["Keyword"].ToString());
                }

                m_dbConnection.Close();
                return list;
            }
            catch (System.Data.SQLite.SQLiteException sE)
            {
                Log.AddMessage(sE.ToString(), "Error");
                MessageBox.Show(sE.ToString());
                return null;
            }
        }

        public static string InsertID(string table, string prefix, string filename)
        {
            try
            {
                string id = "";

                SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=" + ID_PATH + ";Version=3;");
                m_dbConnection.Open();

                //Console.WriteLine(filename);

                filename = filename.Replace("'", "");

                string sql = "insert into [" + table + "] values ('" + filename + "')";
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();

                sql = "select last_insert_rowid()";
                command = new SQLiteCommand(sql, m_dbConnection);

                long lID = (long)command.ExecuteScalar();
                id = lID.ToString();

                m_dbConnection.Close();
                return prefix + id;
            }
            catch (System.Data.SQLite.SQLiteException sE)
            {
                Log.AddMessage(sE.ToString(), "Error");
                MessageBox.Show(sE.ToString());
                return "";
            }
        }

        //public List<int> SearchDB(string table)
        //{
        //    try
        //    {
        //        SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=" + SQL_PATH + ";Version=3;");
        //        m_dbConnection.Open();
        //        List<int> list = new List<int>();

        //        string sql = "select * from " + table + " order by docid";
        //        SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
        //        SQLiteDataReader reader = command.ExecuteReader();

        //        while (reader.Read())
        //        {
        //            list.Add(Int32.Parse(reader["docid"].ToString()));
        //        }

        //        m_dbConnection.Close();
        //        return list;
        //    }
        //    catch (System.Data.SQLite.SQLiteException sE)
        //    {
        //        MessageBox.Show(sE.Message + "\nTry deleting the CULaser.sqlite database in this softwares AppData folder for your user.");
        //        return null;
        //    }
        //}

        //public void CreateNewDataBase()
        //{
        //    SQLiteConnection.CreateFile(SQL_PATH);
        //    SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=" + SQL_PATH + ";Version=3;");
        //    m_dbConnection.Open();

        //    string sql = "CREATE TABLE queued (docid INTEGER, lfpath TEXT)";
        //    SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
        //    command.ExecuteNonQuery();

        //    sql = "CREATE TABLE complete (docid INTEGER, lfpath TEXT)";
        //    command = new SQLiteCommand(sql, m_dbConnection);
        //    command.ExecuteNonQuery();

        //    sql = "CREATE TABLE problem (docid INTEGER, lfpath TEXT)";
        //    command = new SQLiteCommand(sql, m_dbConnection);
        //    command.ExecuteNonQuery();

        //    m_dbConnection.Close();
        //}

        //public void AddQueued(int docid, string lfpath) 
        //{
        //    SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=" + SQL_PATH + ";Version=3;");
        //    m_dbConnection.Open();

        //    try
        //    {
        //        string pattern = "'";
        //        string replacement = "";
        //        Regex rgx = new Regex(pattern);
        //        string result = rgx.Replace(lfpath, replacement);

        //        string sql = "insert into queued (docid, lfpath) values (" + docid + ", '" + result + "')";
        //        //MessageBox.Show(sql);
        //        SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
        //        command.ExecuteNonQuery();

        //        m_dbConnection.Close();
        //    }
        //    catch (Exception e)
        //    {
        //        MessageBox.Show(e.Message);
        //    }
        //}

        //public void AddMultipleQueued(List<Tuple<int, string>> list)
        //{
        //    SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=" + SQL_PATH + ";Version=3;");
        //    //m_dbConnection.Open();

        //    try
        //    {
        //        string sql = "INSERT INTO queued (docid, lfpath) VALUES (@0, @1)";

        //        using (var cn = new SQLiteConnection(m_dbConnection))
        //        {
        //            // Open the connection, and also atransaction:
        //            cn.Open();
        //            using (var transaction = cn.BeginTransaction())
        //            {
        //                foreach (var l in list)
        //                {
        //                    using (var cmd = cn.CreateCommand())
        //                    {
        //                        cmd.CommandText = sql;
        //                        cmd.Parameters.AddWithValue("@0", l.Item1);
        //                        cmd.Parameters.AddWithValue("@1", l.Item2);
        //                        cmd.ExecuteNonQuery();
        //                    }
        //                }
        //                transaction.Commit();
        //            }
        //            cn.Close();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        MessageBox.Show(e.ToString());
        //    }
        //}

        //public void AddMultipleProb(List<Tuple<int, string>> list)
        //{
        //    SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=" + SQL_PATH + ";Version=3;");
        //    //m_dbConnection.Open();

        //    try
        //    {
        //        string sql = "INSERT INTO problem (docid, lfpath) VALUES (@0, @1)";

        //        using (var cn = new SQLiteConnection(m_dbConnection))
        //        {
        //            // Open the connection, and also atransaction:
        //            cn.Open();
        //            using (var transaction = cn.BeginTransaction())
        //            {
        //                foreach (var l in list)
        //                {
        //                    using (var cmd = cn.CreateCommand())
        //                    {
        //                        cmd.CommandText = sql;
        //                        cmd.Parameters.AddWithValue("@0", l.Item1);
        //                        cmd.Parameters.AddWithValue("@1", l.Item2);
        //                        cmd.ExecuteNonQuery();
        //                    }
        //                }
        //                transaction.Commit();
        //            }
        //            cn.Close();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        MessageBox.Show(e.ToString());
        //    }
        //}

        //public void MoveToComplete(int docid, string lfpath)
        //{ 
        //    //ADD TO COMPLETE
        //    SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=" + SQL_PATH + ";Version=3;");
        //    m_dbConnection.Open();

        //    string pattern = "'";
        //    string replacement = "";
        //    Regex rgx = new Regex(pattern);
        //    string result = rgx.Replace(lfpath, replacement);

        //    string sql = "insert into complete (docid, lfpath) values (" + docid + ", '" + result + "')";
        //    SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
        //    command.ExecuteNonQuery();

        //    //REMOVE FROM QUEUED
        //    sql = "delete from queued where docid = " + docid;
        //    command = new SQLiteCommand(sql, m_dbConnection);
        //    command.ExecuteNonQuery();
        //    m_dbConnection.Close();
        //}

        //public void MoveToProblem(int docid, string lfpath)
        //{
        //    //ADD TO PROBLEM
        //    SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=" + SQL_PATH + ";Version=3;");
        //    m_dbConnection.Open();

        //    string pattern = "'";
        //    string replacement = "";
        //    Regex rgx = new Regex(pattern);
        //    string result = rgx.Replace(lfpath, replacement);

        //    string sql = "insert into problem (docid, lfpath) values (" + docid + ", '" + result + "')";
        //    SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
        //    command.ExecuteNonQuery();

        //    //REMOVE FROM QUEUED
        //    sql = "delete from queued where docid = " + docid;
        //    command = new SQLiteCommand(sql, m_dbConnection);
        //    command.ExecuteNonQuery();
        //    m_dbConnection.Close();
        //}

        //public void AddProblem(int docid, string lfpath)
        //{
        //    //ADD TO PROBLEM
        //    SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=" + SQL_PATH + ";Version=3;");
        //    m_dbConnection.Open();

        //    string pattern = "'";
        //    string replacement = "";
        //    Regex rgx = new Regex(pattern);
        //    string result = rgx.Replace(lfpath, replacement);

        //    string sql = "insert into problem (docid, lfpath) values (" + docid + ", '" + result + "')";
        //    SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
        //    command.ExecuteNonQuery();

        //    m_dbConnection.Close();
        //}
    }
}
