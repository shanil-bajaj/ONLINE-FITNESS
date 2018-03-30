using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NDOnlineGym_2017
{
    public partial class CourtBooking : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ImageButton8_Click(object sender, ImageClickEventArgs e)
        {
            string strPopup = "<script language='javascript' ID='script1'>"
           + "window.open('CourtBookingForm.aspx" 
           + "','_blank', 'toolbar=yes,scrollbars=no,resizable=yes,top=5,left=200,width=1010,height=650')"
           + "</script>";
            ScriptManager.RegisterClientScriptBlock((Page)HttpContext.Current.Handler, typeof(Page), "Script1", strPopup, false);
        }
    }
}