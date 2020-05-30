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
        private SqlParameter[] objDatosUsuario;
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
            intIdRol = -1;
        }
        #endregion

        #region "Propiedades"        
            public int IdRol { get => intIdRol; set => intIdRol = value; }
            public string Descripcion { get => strDescripcion; set => strDescripcion = value; }
            public string Error { get => strError; set => strError = value; }
            public string NombreApp { get => strNombreApp; set => strNombreApp = value; }
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

        #endregion
    }
}
