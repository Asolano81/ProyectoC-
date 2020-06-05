using System;
using System.Data;
using System.Data.SqlClient;
using libConexionBd;
using libLlenarGrids;
using libLlenarCombos;
using System.Web.UI.WebControls;

namespace libEscuelaRN
{
    public class clsRolesRN
    {

        #region "Atributos"
        private int intIdRol;
        private string strDescripcion;       
        private string strError;
        private string strNombreApp;
        private string strDocumento;
        private SqlParameter[] objDatosRol;
        private clsConexionBd objConex;
        private clsLlenarGrids objLlenaGrids;
        private DataSet dsDatos;      


        #endregion

        #region "Constructor"
        public clsRolesRN(String nombreApp)
        {
            this.strNombreApp = nombreApp;
            strError = string.Empty;
            strDescripcion = string.Empty;
            strDocumento = string.Empty;
            intIdRol = -1;
        }
        #endregion

        #region "Propiedades"        
            public int IdRol { get => intIdRol; set => intIdRol = value; }
            public string Descripcion { get => strDescripcion; set => strDescripcion = value; }
            public string Error { get => strError; set => strError = value; }
            public string Documento {set => strDocumento = value; }
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
            switch (metodoOrigen)
            {
                case "consultar":
                    if (strDocumento == string.Empty)
                    {
                        strError = "El documento no puede estar vacio";
                        return false;
                    }
                    break;
                case "registrar":
                    if (intIdRol <=0)
                    {
                        strError = "Debe seleccionar un rol";
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
                    case "consultar":
                        objDatosRol = new SqlParameter[1];
                        objDatosRol[0] = new SqlParameter("@Documento", strDocumento);
                        break;
                    case "registrar":
                        objDatosRol = new SqlParameter[2];
                        objDatosRol[0] = new SqlParameter("@Documento", strDocumento);
                        objDatosRol[1] = new SqlParameter("@IdRol", intIdRol);
                        break;
                }
                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
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
                objLlenar.ParametrosSQL = objDatosRol;

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

        public bool consultar()
        {
            try
            {
                if (!agregarParam("consultar"))
                {
                    return false;
                }
                clsConexionBd objCnx = new clsConexionBd(strNombreApp);

                objCnx.SQL = "[SP_ConsultarRolDeUs" +
                    "]";
                objCnx.ParametrosSQL = objDatosRol;

                if (!objCnx.llenarDataSet(true, true))
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
                strError = ex.Message;
                return false;
            }
        }

        public bool registrar()
        {
            try
            {
                if (!agregarParam("registrar"))
                {
                    return false;
                }

                objConex = new clsConexionBd(strNombreApp);
                objConex.SQL = "[SP_AñadirRolUsuario]";
                objConex.ParametrosSQL = objDatosRol;

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
                strError = ex.Message;
                return false;
            }
        }

        #endregion
    }
}
