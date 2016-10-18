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

        private DataSet GetData()
        {
            int id = 0;
            int.TryParse(IdTextBox.Text, out id);
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

            int id = 0;
            int.TryParse(IdTextBox.Text, out id);
            if(string.IsNullOrWhiteSpace(IdTextBox.Text))
            {
                Response.Write("<script>alert('Introdusca Id')</script>");
            }
            else
            {

                DataSet ds = GetData();

                Repeater1.DataSource = ds;
                Repeater1.DataBind();

                UsuarioGridView.DataSource = usu.Listado("*","IdUsuario="+IdTextBox.Text, " ");
            UsuarioGridView.DataBind();
            }
       
           

        }

        protected void ImprimirButton_Click(object sender, EventArgs e)
        {
            Usuario usu = new Usuario();
  
            UsuarioReportViewer.Reset();


            UsuarioReportViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
            UsuarioReportViewer.LocalReport.ReportPath = @"Reportes\ReportUsuario1.rdlc";

            UsuarioReportViewer.LocalReport.DataSources.Clear();
            UsuarioReportViewer.LocalReport.DataSources.Add(

                new ReportDataSource("Usuario",
               usu.listar)
                );
            UsuarioReportViewer.LocalReport.Refresh();

        }
    }
}