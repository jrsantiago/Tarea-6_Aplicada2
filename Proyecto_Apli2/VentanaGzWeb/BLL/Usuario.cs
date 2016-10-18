using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DAL;

namespace BLL
{
    public class Usuario : ClaseMaestra
    {
        public int IdUsuario { get; set; }
        public string Contrasena { get; set; }
        public string UserName { get; set; }
        public string  Nombre { get; set; }
        public bool Restriccion { get; set; } //Esta pasando datos solamente true
        //Falta ponerle la Imagen en la bace de Datos...
        public List<Usuario>listar { get; set; }

        public Usuario()
        {
            this.IdUsuario = 0;
            this.Contrasena = "";
            this.UserName = "";
            this.Restriccion = false;
            this.listar = new List<Usuario>();
        }

        public override bool Insertar()
        {
            DbVentana cone = new DbVentana();
            bool retornar = false;
            try
            {
                retornar = cone.Ejecutar(String.Format("Insert into Usuario(Contrasena,UserName,Restriccion,Nombre) Values('{0}','{1}', 2,'{3}')", this.Contrasena, this.UserName, this.Restriccion, this.Nombre));

            }catch(Exception ex)
            {
                throw ex;
            }
            return retornar;
        }

        public override bool Editar()
        {
            bool retornar = false;
            DbVentana cone = new DbVentana();
            try
            {
                retornar = cone.Ejecutar(String.Format("Update Usuario set Contrasena='{0}',UserName='{1}',Nombre='{2}',Restriccion = 3 WHERE IdUsuario = {4}", this.Contrasena, this.UserName, this.Nombre, this.Restriccion, this.IdUsuario));

            }catch(Exception ex)
            {
                throw ex;
            }
            return retornar;
        }

        public override bool Eliminar()
        {
            bool Retornar = false;
            DbVentana cone = new DbVentana();
            try
            {
                Retornar = cone.Ejecutar(String.Format("Delete From Usuario where IdUsuario ={0}", this.IdUsuario));

            }catch(Exception ex)
            {
                throw ex;
            }
            return Retornar;
        }

        public override bool Buscar(int IdBuscado)
        {
            DbVentana cone = new DbVentana();
            DataTable dt = new DataTable();
            bool Retornar = true;
            try
            {
                dt = cone.ObtenerDatos(String.Format("Select * from Usuario where IdUsuario ={0}", IdBuscado));
                if(dt.Rows.Count >0)
                {
                    this.IdUsuario = (int)dt.Rows[0]["IdUsuario"];
                    this.Contrasena = dt.Rows[0]["Contrasena"].ToString();
                    this.UserName = dt.Rows[0]["UserName"].ToString();
                    this.Nombre = dt.Rows[0]["Nombre"].ToString();
                    this.Restriccion = (bool)dt.Rows[0]["Restriccion"];
                }

            }catch(Exception ex)
            {
                throw ex;
            }
            return Retornar;
        }

        public override DataTable Listado(string Campos, string Condicion, string Orden)
        {
            DbVentana cone = new DbVentana();
            string OrdenFinal = "";
            if (!Orden.Equals(""))
                OrdenFinal = "Orden by " + Orden;
            return cone.ObtenerDatos("Select " + Campos + " From Usuario where " + Condicion +" --");
        }
    }
}
