using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ConnectionConfiguration
/// </summary>
public class ConnectionConfiguration
{
    public ConnectionConfiguration()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public FbConnection ConnectionString//for firebird connection
    {
        get
        {
            return new FbConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
        }
    }
}