<%@ Application Language="C#" %>

<script RunAt="server">

    void Application_Start(object sender, EventArgs e)
    {
        // Code that runs on application startup
    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown
    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs
        //exc ;
        //try
        //{
        //    Exception exc = Server.GetLastError();
        //    if (exc.GetType() == typeof(HttpException))
        //    {
        //        string Msg = exc.Message;
        //    }
        //}
        //catch (Exception ex)
        //{
        //    Response.Write(ex);
        //}


        Exception exc = Server.GetLastError();

        if (exc.GetType() == typeof(HttpException))
        {
            string Msg = exc.Message;
        }
        if (exc != null)
        {
            // Log
            if (HttpContext.Current.Server != null)
            {
                Response.Write(exc);
                //throw new HttpUnhandledException("Unhandle Exception", exc);
            }
        }





    }

    void Session_Start(object sender, EventArgs e)
    {
        if (System.Web.HttpContext.Current.Request.Url.AbsolutePath != "/DemoRegistration.aspx")
        {
            //if (Session.IsNewSession && Session["SessionExpire"] == null)
            {
                Session["SessionExpire"] = true;
                Response.Redirect("~/frmLogin.aspx");
            }
        }
    }

    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }
       
</script>
