using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using DAL;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace VentanaGzWeb.Registros
{
    public partial class ReUsuario : System.Web.UI.Page
    {
        
        DbVentana cone = new DbVentana();
        Usuario usu = new Usuario();
        public int id;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                HyperLink1.Visible = false;
                LblMesanje.Visible = false;
            }
        }
        public void ObtenereDatos()
        {
            if (usu.Restriccion == 1)
            {
                RestriccionDropDownList.Text = "Administrador";
            }
            else
            {
                RestriccionDropDownList.Text = "Usuario";
            }
            UserNameTextBox.Text = usu.UserName;
            ContrasenaTextBox.Text = usu.Contrasena;
            NombreTextBox.Text = usu.Nombre;

        }
        public void ObtenerValor()
        {
            if (RestriccionDropDownList.Text == "Administrador")
            {
                usu.Restriccion = 1;
            }
            else
            {
                usu.Restriccion = 0;
            }
            string str = FileUpload1.FileName;
            FileUpload1.PostedFile.SaveAs(Server.MapPath("//Imagenes//") + str);
            string path = "~//Imagenes//" + str.ToString();

            usu.Imagenes = path;
            usu.UserName = UserNameTextBox.Text;
            usu.Contrasena = ContrasenaTextBox.Text;
            usu.Nombre = NombreTextBox.Text;
        }
        public void ConvertirId()
        {
            int.TryParse(IdTextBox.Text, out this.id);
        }
        public void Limpiar()
        {
            IdTextBox.Text = "";
            NombreTextBox.Text = "";
            ContrasenaTextBox.Text = "";
            UserNameTextBox.Text = "";
        }
        public void subirFoto()
        {
            //if (FileUpload1.HasFile)
            //{
            //    string str = FileUpload1.FileName;
            //    FileUpload1.PostedFile.SaveAs(Server.MapPath("//Imagenes//") + str);
            //    string path = "~//Imagenes//" + str.ToString();
            //    cone.Ejecutar(String.Format("Insert into Usuario(imagen) Values('{0}')", path));

            //    LblMesanje.Text = "Imagen Subida";


            //}
            //else
            //{
            //    LblMesanje.Text = "Porfavor suba una imagen";
            //}
        }

        protected void BuscarButton_Click(object sender, EventArgs e)
        {

            ConvertirId();

            if (usu.Buscar(this.id))
            {
                ObtenereDatos();
            }
            else
            {
                Response.Write("<script>alert('Debe insertar un Id')</script>");
            }
        }

        protected void GuardarButton_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(NombreTextBox.Text) || string.IsNullOrWhiteSpace(ContrasenaTextBox.Text) || string.IsNullOrWhiteSpace(NombreTextBox.Text))
            {
                Response.Write("<script>alert('LLene Todos los Campos')</script>");
            }
            else
            {

                if (IdTextBox.Text == "")
                {

                    ObtenerValor();
                    if (usu.Insertar())
                    {
                        Response.Write("<script>alert('Guardado')</script>");
                    }
                }
                else
                {
                    ConvertirId();
                    usu.IdUsuario = this.id;
                    ObtenerValor();
                    if (usu.Editar())
                    {
                        Response.Write("<script>alert('Actualizado')</script>");
                    }
                }

            }
        }

        protected void EliminarButton_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(IdTextBox.Text))
            {
                Response.Write("<script>alert('Introdusca un Id')</script>");
            }else
            {
                ConvertirId();
                usu.IdUsuario = this.id;
                usu.Eliminar();
                 Response.Write("<script>alert('Eliminado')</script>");
                Limpiar();
            }
        }

        protected void LimpiarButton_Click(object sender, EventArgs e)
        {
            Limpiar();

        }

        protected void UploadButton_Click(object sender, EventArgs e)
        {
            
            
        }
    }
}