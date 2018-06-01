using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UserRights
/// </summary>
public abstract class UserRights
{
    public static bool VCancel
    {
        get { return Convert.ToBoolean(HttpContext.Current.Session["VCancel"]); }
        set { HttpContext.Current.Session["VCancel"] = value; }
    }

    public static bool VUpdation
    {
        get { return Convert.ToBoolean(HttpContext.Current.Session["VUpdation"]); }
        set { HttpContext.Current.Session["VUpdation"] = value; }
    }

    public static bool VWrite
    {
        get { return Convert.ToBoolean(HttpContext.Current.Session["VWrite"]); }
        set { HttpContext.Current.Session["VWrite"] = value; }
    }

    public static bool VRead
    {
        get { return Convert.ToBoolean(HttpContext.Current.Session["VRead"]); }
        set { HttpContext.Current.Session["VRead"] = value; }
    }
}