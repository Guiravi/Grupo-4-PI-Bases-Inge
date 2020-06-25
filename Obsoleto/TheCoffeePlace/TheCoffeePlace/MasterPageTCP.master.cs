using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPageTCP : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
		errorMsg.InnerHtml = "";
		if (Session["ErrorMsg"] != null)
		{
			SetErrorMsg(Session["ErrorMsg"].ToString());
			Session.Remove("ErrorMsg");
		}
    }

	protected void SetErrorMsg(string errorMsg)
	{
		string errorDiv = @"<div style=""width:300px; heigth:100px; background-color:indianred; border:solid black 1px; border-radius: 10px 10px 10px 10px;
            position: static;"">
                                <p >" + errorMsg + "</p></div>";
		((System.Web.UI.HtmlControls.HtmlGenericControl)this.FindControl("errorMsg")).InnerHtml += errorDiv;
	}
}
