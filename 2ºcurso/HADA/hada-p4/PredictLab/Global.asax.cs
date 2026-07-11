using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;

namespace PredictLab
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            Application["UserCount"] = 0;
            Application["ImgPermitidas"] = new string[] { ".jpg", ".png", ".gif", "jpeg" };
        ScriptManager.ScriptResourceMapping.AddDefinition(
            "jquery",
            new ScriptResourceDefinition
            {
                Path = "https://code.jquery.com/jquery-3.7.1.min.js",
                DebugPath = "https://code.jquery.com/jquery-3.7.1.min.js"
            });
        }

        protected void Session_Start(object sender, EventArgs e)
        {   
            Application.Lock();
                Application["UserCount"] = (int)Application["UserCount"] + 1;
            Application.UnLock();
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {
            Application.Lock();
            if (Application["UserCount"] != null)
            {
                Application["UserCount"] = (int)Application["UserCount"] - 1;
            }
            Application.UnLock();
        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}