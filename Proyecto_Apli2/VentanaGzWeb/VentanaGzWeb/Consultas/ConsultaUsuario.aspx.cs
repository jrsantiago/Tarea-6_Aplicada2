using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using BLL;
using Microsoft.Reporting.WebForms;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace VentanaGzWeb.Consultas
{
    public partial class ConsultaUsuario : System.Web.UI.Page
    {
        Usuario usu = new Usuario();

      

        protected void Page_Load(object sender, EventArgs e)
        { /*DbVentana cone = new DbVentana();*/
        //    DataSet ds = GetData();
           
        //    Repeater1.DataSource = ds;
        //    Repeater1.DataBind();

        }
        public int convertirId()
        {
            int id = 0;
            int.TryParse(IdTextBox.Text, out id);
            return id;
        }

        private DataSet GetData()
        {
            int id = convertirId();
            string Cs = ConfigurationManager.ConnectionStrings["VentanaDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(Cs))
            {
                SqlDataAdapter da = new SqlDataAdapter("Select * from Usuario where IdUsuario="+id,con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
        }

        protected void BuscarButton_Click(object sender, EventArgs e)
        {

            DbVentana cone = new DbVentana();

            int id = convertirId();
            if(string.IsNullOrWhiteSpace(IdTextBox.Text))
            {
                Response.Write("<script>alert('Introdusca Id')</script>");
            }
            else
            {

                DataSet ds = GetData();

                Repeater1.DataSource = ds;
                Repeater1.DataBind();

            }
       
           

        }

        protected void ImprimirButton_Click(object sender, EventArgs e)
        {
            int id = convertirId();
            Usuario usu = new Usuario();

            UsuarioReportViewer.LocalReport.DataSources.Clear();
            UsuarioReportViewer.ProcessingMode = ProcessingMode.Local;

            UsuarioReportViewer.LocalReport.ReportPath = @"Reportes\ReportUsuario1.rdlc";

            ReportDataSource source = new ReportDataSource("Usuario", usu.Listado("*","IdUsuario="+IdTextBox.Text, " "));

            UsuarioReportViewer.LocalReport.DataSources.Add(source);
            UsuarioReportViewer.LocalReport.Refresh();
  
            //UsuarioReportViewer.Reset();


            //UsuarioReportViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
            //UsuarioReportViewer.LocalReport.ReportPath = @"Reportes\ReportUsuario1.rdlc";

            //UsuarioReportViewer.LocalReport.DataSources.Clear();
            //UsuarioReportViewer.LocalReport.DataSources.Add(

            //    new ReportDataSource("Usuario",
            //   usu.listar)
            //    );
            //UsuarioReportViewer.LocalReport.Refresh();

        }
    }
}