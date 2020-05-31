using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using libLlenarGrids;
using libEscuelaRN;
using System.Web.UI.WebControls;

namespace libEscuelaOpe
{
    public class clsUsuariosOPE
    {
        #region "Atributos"
        private int intIdUsuario;
        private string strNombreUsuario;
        private string strEmail;
        private string strContrasena;
        private string strIdentificacion;
        private string strNombre;
        private string strRol;
        private string strApellido;
        private string strFechaNacimiento;
        private string strTelefono;
        private string strNombreApp;
        private int intPadreId;
        private string strError;
        private DataSet dsDatos;
        private string strMensaje;
        DropDownList ddlGen;
        GridView gvGen;
        #endregion

        #region "Constructor"
        public clsUsuariosOPE(string nombreApp)
        {
            this.strNombreApp = nombreApp;    
        }
        #endregion

        #region "Propiedades"
        public string Nombre { get => strNombre; set => strNombre = value; }
        public string NombreUsuario { get => strNombreUsuario; set => strNombreUsuario = value; }
        public string Rol { get => strRol; set => strRol = value; }
        public string Identificacion { get => strIdentificacion; set => strIdentificacion = value; }
        public string Error { get => strError; }
        public string Contrasena { get => strContrasena; set => strContrasena = value; }

        #endregion

        #region "Métodos Privados"       
        private bool validar(string metodoOrigen)
        {
            if (strNombreApp == string.Empty)
            {
                strError = "Olvido enviar el nombre de la aplicación";
                return false;
            }
            switch (metodoOrigen.ToLower())
            {
                case "loginusuario":
                    if (strNombreUsuario == string.Empty)
                    {
                        strError = "El nombre de usuario no puede estar vacio";
                        return false;
                    }
                    if (strContrasena == string.Empty)
                    {
                        strError = "La contraseña no puede estar vacia";
                        return false;
                    }
                    break;
            }
            return true;

        }

        #endregion

        #region "Métodos públicos"
    
        public bool consConex()
        {
            try
            {
                clsUsuariosRN objConsRn = new clsUsuariosRN(strNombreApp);

                if (!objConsRn.consConex())
                {
                    objConsRn = null;
                    return false;
                }
                if (objConsRn.DatosRptaBd.Tables[0].Rows.Count == 0)
                {
                    strError = "No hay conexion";
                    return false;
                }
                strNombre = objConsRn.DatosRptaBd.Tables[0].Rows[0]["nombre"].ToString();
                strContrasena = objConsRn.DatosRptaBd.Tables[0].Rows[0]["contrasena"].ToString();
                strRol = objConsRn.DatosRptaBd.Tables[0].Rows[0]["rol"].ToString();
                objConsRn = null;

                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool realizarConex()
        {
            try
            {
                if (!validar("loginusuario"))
                {
                    return false;
                }
                clsUsuariosRN objRn = new clsUsuariosRN(this.strNombreApp);

                objRn.NombreUsuario = strNombreUsuario;
                objRn.Contrasena = strContrasena;
                objRn.Rol = strRol;               

                if (!objRn.realizarConex())
                {
                    strError = objRn.Error;
                    objRn = null;
                    return false;
                }

                if (objRn.DatosRptaBd.Tables[0].Rows[0]["Respuesta"].ToString() == "1")
                {
                    strError = objRn.DatosRptaBd.Tables[0].Rows[0]["Mensaje"].ToString();
                    objRn = null;
                    return false;
                }

                strMensaje = objRn.DatosRptaBd.Tables[0].Rows[0]["Mensaje"].ToString();

                objRn = null;
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


    }
}
