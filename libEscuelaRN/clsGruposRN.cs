using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using libConexionBd;
using libLlenarGrids;
using libLlenarCombos;

namespace libEscuelaRN
{
    public class clsGruposRN
    {
        #region "Atributos"
        private string strTipoCons;
        private string strNombreApp;
        private string strError;
        private SqlParameter[] objDatosGrupo;
        private clsConexionBd objConex;
        private clsLlenarGrids objLlenaGrids;
        private DataSet dsDatos;
        #endregion

        #region "Constructor"
        public clsGruposRN(String nombreApp)
        {
            this.strNombreApp = nombreApp;
            strError = string.Empty;
        }
        #endregion

        #region "Propiedades"        
        public string Error { get => strError; }
        public DataSet DatosRptaBd { get => dsDatos; }
        #endregion

        #region "Métodos Privados"
        private bool validar(string metodoOrigen)
        {
            if (strNombreApp == string.Empty)
            {
                strError = "Olvido enviar el nombre de la palicación";
                return false;
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
                    case "matricular":

                        break;
                    case "llenardropdowns":
                        objDatosGrupo = new SqlParameter[1];
                        objDatosGrupo[0] = new SqlParameter("@TipoConsulta", strTipoCons);
                        break;
                    case "consestudiante":

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
                    case "ddlDeporte":
                        strTipoCons = "DEPORTES";
                        strCampoId = "id";
                        strCampoTexto = "descripcion";
                        break;
                    case "ddlAsignatura":
                        strTipoCons = "CURSOS";
                        strCampoId = "Id";
                        strCampoTexto = "Curso";
                        break;
                    case "ddlDocente":
                        strTipoCons = "DOCENTES";
                        strCampoId = "Id";
                        strCampoTexto = "Docente";
                        break;
                    case "ddlAula":
                        strTipoCons = "AULAS";
                        strCampoId = "Id";
                        strCampoTexto = "Aula";
                        break;
                    default:
                        strError = "Control desconocido";
                        return false;
                }
                if (!agregarParam("llenarDropDowns"))
                {
                    return false;
                }
                clsLlenarCombos objLlenar = new clsLlenarCombos(strNombreApp);

                objLlenar.SQL = "SP_ConsultarComboGrupos";
                objLlenar.CampoID = strCampoId;
                objLlenar.CampoTexto = strCampoTexto;
                objLlenar.ParametrosSQL = objDatosGrupo;

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
