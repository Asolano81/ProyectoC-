using System;
using System.Data;
using System.Data.SqlClient;
using libConexionBd;
using libLlenarGrids;
using libLlenarCombos;
using System.Web.UI.WebControls;

namespace libEscuelaRN
{
    public class clsUsuariosRN
    {

        #region "Atributos"
        private int intIdUsuario;
        private string strNombreUsuario;
        private string strEmail;
        private string strContrasena;
        private string strIdentificacion;
        private string strNombre;
        private string strApellido;
        private string strFechaNacimiento;
        private string strTelefono;
        private string strNombreApp;
        private int intPadreId;
        private string strError;        
        private SqlParameter[] objDatosUsuario;
        private clsConexionBd objConex;
        private clsLlenarGrids objLlenaGrids;
        private DataSet dsDatos;

       
        #endregion

        #region "Constructor"
        public clsUsuariosRN(String nombreApp)
        {
            this.strNombreApp = nombreApp;
            strError = string.Empty;
        }
        #endregion

        #region "Propiedades"        
        public string NombreUsuario { set => strNombreUsuario = value; }
        public string Contrasena { set => strContrasena = value; }
        public int IntIdUsuario { get => intIdUsuario; set => intIdUsuario = value; }
        public DataSet DatosRptaBd { get => dsDatos; }
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
                        strError = "LÑa contraseña no puede estar vacia";
                        return false;
                    }
                    break;
            }
            return true;
            
        }

        private bool loginUsuario()
        {
            /*try
            {
                if (!validar("loginUsuario"))
                {
                    return false;
                }                
            catch (Exception ex)
            {

                throw ex;
            }*/

            return true;
        }    



        #endregion

        #region "Métodos Públicos"
        public bool llenarDropDowns(DropDownList ddlGen)
        {
            try
            {
                String strCampoTexto = "";
                String strCampoId = "";
                switch (ddlGen.ID)
                {
                    case "ddlRol":
                        strCampoId = "id";
                        strCampoTexto = "descripcion";
                        break;
                    default:
                        strError = "Control desconocido";
                        return false;
                }
                
                clsLlenarCombos objLlenar = new clsLlenarCombos(strNombreApp);

                objLlenar.SQL = "SP_ConsultarRoles";
                objLlenar.CampoID = strCampoId;
                objLlenar.CampoTexto = strCampoTexto;
                objLlenar.ParametrosSQL = objDatosUsuario;

                if (!objLlenar.llenarComboWeb(ddlGen))
                {
                    strError = objLlenar.Error;
                    objLlenar = null;
                    return false;
                }
                objLlenar = null;
                return true;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool consConex()
        {
            try
            {
                clsConexionBd objCnx = new clsConexionBd(strNombreApp);

                objCnx.SQL = "SP_ConsultarConexion";
                objCnx.ParametrosSQL = objDatosUsuario;

                if (!objCnx.llenarDataSet(false, true))
                {
                    strError = objCnx.Error;
                    objCnx.cerrarCnx();
                    objCnx = null;
                    return false;
                }
                dsDatos = objCnx.DataSetLleno;
                objCnx.cerrarCnx();
                objCnx = null;
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
