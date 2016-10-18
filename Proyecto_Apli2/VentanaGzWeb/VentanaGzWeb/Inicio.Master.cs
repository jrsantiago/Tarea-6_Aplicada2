using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VentanaGzWeb
{
    public partial class Inicio : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void RegistroUsuarioButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ReUsuario.aspx");
        }

        protected void ConsultarButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ConsultaUsuario.aspx");
        }
    }
}