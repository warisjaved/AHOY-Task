using System; 

using System.Diagnostics;
using System.Web;
using System.Xml;
using Microsoft.AspNetCore.Mvc;

using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace AHOY_Backend_Task.Repository
{
    public class DataAccess : IDataAccess
    {
        private IConfiguration _config;

        public DataAccess(IConfiguration config)
        {
            _config = config;
        }

        public void Method()
        {
            var connectionString = _config.GetValue<string>("App:Connection:Value"); //notice the structure of this string
                                                                                     //do whatever with connection string
        }
        public SqlCommand GetSqlCommand(string commandText, SqlConnection sqlConnection, System.Data.CommandType commandType)
        {
            var sqlCommand = new SqlCommand(commandText, sqlConnection)
            {
                CommandType = commandType
            };
            return sqlCommand;
        }


    }

    public interface IDataAccess {
        public void Method();
    }
    //public class DatabaseRepository: IDatabaseRepository
    //{
    //    private IConfiguration _config;

    //    public DatabaseRepository(IConfiguration config)
    //    {
    //        _config = config;
    //    }


    //    var connectionString = _config.GetValue<string>("App:Connection:Value");

    //    public SqlConnection GetSqlConnection()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    //public SqlConnection GetSqlConnection()
    //    //{
    //    //    var connectionString = GetConnectionString("DefaultConnection");
    //    //    var sqlConnection = new SqlConnection(connectionString);
    //    //    //var sqlConnection = new SqlConnection(connString);
    //    //    return sqlConnection;
    //    //}
    //    //public SqlCommand GetSqlCommand(string commandText, SqlConnection sqlConnection, System.Data.CommandType commandType)
    //    //{
    //    //    var sqlCommand = new SqlCommand(commandText, sqlConnection)
    //    //    {
    //    //        CommandType = commandType
    //    //    };
    //    //    return sqlCommand;
    //    //}

    //    //public static string GetConnectionString(string connName)//connName is connection string name same as //in config file
    //    //{
    //    //    try
    //    //    {
    //    //        string connString = string.Empty;
    //    //        string configPath = string.Empty;
    //    //        string p = Process.GetCurrentProcess().MainModule.FileName; 
    //    //        XmlDocument doc = new XmlDocument();
    //    //        //if (p.Contains("AmsUtilites.exe"))
    //    //        //{
    //    //        //    p = Process.GetCurrentProcess().MainModule.FileName.Replace("AmsUtilites\\bin\\Debug\\AmsUtilites.exe", "AMS\\web.config");
    //    //        //    doc.Load(p);
    //    //        //}
    //    //        //else
    //    //        //{
    //    //            doc.Load(HttpContext.Current.Server.MapPath("~/web.config"));
    //    //        //}
    //    //        //doc.Load(file);
    //    //        XmlNode node = null;

    //    //        node = doc.SelectSingleNode("configuration/connectionStrings/add[@name = \"" +
    //    //        connName + "\"]");////it will select connection string section.
    //    //        if (node != null)
    //    //        {
    //    //            XmlAttribute attr = node.Attributes["connectionString"];
    //    //            if (attr != null)
    //    //            {
    //    //                SqlConnectionStringBuilder csb = new SqlConnectionStringBuilder
    //    //                (attr.Value);////sql string builder class passing connection string parameter.
    //    //                if (IsIntegratedSecurity(csb.ToString()))////check for IntegratedSecurity 
    //    //                                                         ////if true then there is no password in config file.
    //    //                {
    //    //                    string clearPass = Helpers.EncryptionHelper.Decrypt(csb.Password);/////if password is not encrypted then function will return null.
    //    //                    if (string.IsNullOrEmpty(clearPass))////if password is not encrypted
    //    //                    {
    //    //                        csb.Password = Helpers.EncryptionHelper.Encrypt(csb.Password);////call encrypt function to encrypt password and return encrypted text.
    //    //                        connString = csb.ToString();////assign Encrypted password to connection string.
    //    //                        attr.Value = csb.ToString();
    //    //                        doc.Save(configPath);/////save config file with changed Encrypted password.
    //    //                    }
    //    //                    //else//// if password was already encrypted then assign decrypted password to connection string.
    //    //                    //{
    //    //                    //    csb.Password = clearPass;////assign original password to return connection string.
    //    //                    //    connString = csb.ToString();
    //    //                    //    attr.Value = csb.ToString();
    //    //                    //}
    //    //                }
    //    //                else
    //    //                {
    //    //                    connString = ConfigurationManager.ConnectionStrings
    //    //                        ["DefaultConnection"].ConnectionString;
    //    //                }
    //    //            }
    //    //        }
    //    //        return connString;/////return connection string.
    //    //    }
    //    //    catch (Exception ex)
    //    //    {
    //    //        return null;
    //    //    }
    //    //}

    //    //private static bool IsIntegratedSecurity(string attr)
    //    //{
    //    //    return attr.ToUpper().Contains("PASSWORD");////if not contains password 
    //    //                                               ////then it is interated security true, there is no password to encrypt.
    //    //}


    //}
}