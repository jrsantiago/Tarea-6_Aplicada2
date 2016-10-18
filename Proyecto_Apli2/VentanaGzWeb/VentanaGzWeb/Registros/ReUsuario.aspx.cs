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
            if (usu.Restriccion == true)
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
                usu.Restriccion = true;
            }
            else
            {
                usu.Restriccion = false;
            }

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
            HttpPostedFile postFi = FileUpload1.PostedFile;
            string filename = Path.GetFileName(postFi.FileName);
            string fileExte = Path.GetExtension(filename);
            int filesize = postFi.ContentLength;

            if(fileExte.ToLower()==".jpg" || fileExte.ToLower()==".bmp" ||
                    fileExte.ToLower() == ".gif" || fileExte.ToLower() == ".png")
            {
                Stream stre = postFi.InputStream;
                BinaryReader binaRe = new BinaryReader(stre);

                byte[] bytes = binaRe.ReadBytes((int)stre.Length);

                string cs = ConfigurationManager.ConnectionStrings["VentanaDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(cs))
                {
                    SqlCommand cmd = new SqlCommand("spUploadImage", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter paramName = new SqlParameter()
                    {
                        ParameterName ="Name",
                        Value = filename
                    };
                    cmd.Parameters.Add(paramName);
                    SqlParameter paramSize = new SqlParameter()
                    {
                        ParameterName = "Size",
                        Value = filesize
                    };
                    cmd.Parameters.Add(paramSize);
                    SqlParameter paramData = new SqlParameter()
                    {
                        ParameterName = "ImagenData",
                        Value = bytes
                    };
                    cmd.Parameters.Add(paramData);
                }

            }else
            {
                LblMesanje.Visible = true;
                LblMesanje.Text = "Solo Imagenes (.jpg, .bmp, .gif, png) Pueden ser Cargadas";
                LblMesanje.ForeColor = System.Drawing.Color.Red;
                HyperLink1.Visible = false;
            }
        }
    }
}