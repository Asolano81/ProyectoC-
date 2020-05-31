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
        private string strRol;
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
        public string NombreUsuario { get => strNombreUsuario; set => strNombreUsuario = value; }
        public string Identificacion { get => strIdentificacion; set => strIdentificacion = value; }
        public string Rol { get => strRol; set => strRol = value; }
        public string Error { get => strError; }
        public DataSet DatosRptaBd { get => dsDatos; }
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
         
        private bool agregarParam(string metodoOrigen)
        {
            try
            {
                if (!validar(metodoOrigen))
                {
                    return false;
                }

                switch (metodoOrigen.ToLower())
                {
                    case "realizarconex":
                        objDatosUsuario = new SqlParameter[3];
                        objDatosUsuario[0] = new SqlParameter("@Nombre", strNombreUsuario);
                        objDatosUsuario[1] = new SqlParameter("@contrasena", strContrasena);                        
                        objDatosUsuario[2] = new SqlParameter("@Rol", strRol);
                        break;
                }
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
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

        public bool realizarConex()
        {
            try
            {
                if (!agregarParam("realizarConex"))
                {
                    return false;
                }

                objConex = new clsConexionBd(strNombreApp);
                objConex.SQL = "[SP_RealizarConexion]";
                objConex.ParametrosSQL = objDatosUsuario;

                if (!objConex.llenarDataSet(true, true))
                {
                    strError = objConex.Error;
                    objConex.cerrarCnx();
                    objConex = null;
                    return false;
                }
                dsDatos = objConex.DataSetLleno;
                objConex.cerrarCnx();
                objConex = null;
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
