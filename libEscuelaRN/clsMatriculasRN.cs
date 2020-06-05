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
    public class clsMatriculasRN
    {
        #region "Atributos"
        private string strTipoCons;
        private string strNombreApp;
        private string strIdentEstu;
        private string strFechDeMatr;
        private int intIdDeporte;
        private int intIdProfesor;
        private int intIdGrupo;
        private int intIdGrupoViejo;
        private string strError;
        private SqlParameter[] objDatosMatricula;
        private clsConexionBd objConex;
        private clsLlenarGrids objLlenaGrids;
        private DataSet dsDatos;
        #endregion

        #region "Constructor"
        public clsMatriculasRN(string nombreApp)
        {
            this.strNombreApp = nombreApp;
            strError = string.Empty;
        }
        #endregion

        #region "Propiedades"        
        public string Error { get => strError; }
        public DataSet DatosRptaBd { get => dsDatos; }
        public string IdentEstu { set => strIdentEstu = value; }
        public string FechDeMatr { set => strFechDeMatr = value; }
        public int IdDeporte { set => intIdDeporte = value; }
        public int IdProfesor { set => intIdProfesor = value; }
        public int IdGrupo {set => intIdGrupo = value; }
        public int IdGrupoViejo { set => intIdGrupoViejo = value; }
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
                case "registrarOpe":
                    if (strIdentEstu == string.Empty)
                    {
                        strError = "La identificación del estudiante no puede estar vacia";
                        return false;
                    }
                    if (strFechDeMatr == string.Empty)
                    {
                        strError = "La fecha no puede estar vacia";
                        return false;
                    }
                    if (intIdDeporte <= 0)
                    {
                        strError = "Debe seleccionar un deporte para la matricula para la matrícula";
                        return false;
                    }
                    if (intIdGrupo <= 0)
                    {
                        strError = "Debe seleccionar un deporte para la matricula para la matrícula";
                        return false;
                    }
                    break;
                case "consultarMatricula":
                    if (strIdentEstu == string.Empty)
                    {
                        strError = "La identificación del estudiante no puede estar vacia";
                        return false;
                    }
                    if (intIdDeporte <= 0)
                    {
                        strError = "Debe seleccionar un deporte para la matricula para la matrícula";
                        return false;
                    }
                    break;
                case "modificarMatricula":
                    if (strIdentEstu == string.Empty)
                    {
                        strError = "La identificación del estudiante no puede estar vacia";
                        return false;
                    }
                    if (intIdGrupo <= 0)
                    {
                        strError = "Debe seleccionar un deporte para la matricula para la matrícula";
                        return false;
                    }
                    if (intIdGrupoViejo == intIdGrupo)
                    {
                        strError = "Esta seleccionando el mismo grupo, si dedesa modificarlo elija uno diferente";
                        return false;
                    }
                    break;
                case "eliminarDeporteMatri":
                    if (strIdentEstu == string.Empty)
                    {
                        strError = "La identificación del estudiante no puede estar vacia";
                        return false;
                    }
                    if (intIdGrupo <= 0)
                    {
                        strError = "Debe seleccionar un deporte para la matricula para la matrícula";
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
                    case "registrarope":
                        objDatosMatricula = new SqlParameter[4];
                        objDatosMatricula[0] = new SqlParameter("@DocEstudiante", strIdentEstu);
                        objDatosMatricula[1] = new SqlParameter("@Fecha", strFechDeMatr);
                        objDatosMatricula[2] = new SqlParameter("@IdGrupo", intIdGrupo);
                        objDatosMatricula[3] = new SqlParameter("@IdDeporte", intIdDeporte);
                        break;
                    case "llenardropdowns":
                        objDatosMatricula = new SqlParameter[2];
                        objDatosMatricula[0] = new SqlParameter("@TipoConsulta", strTipoCons);
                        objDatosMatricula[1] = new SqlParameter("@IdDeporte", intIdDeporte);
                        break;
                    case "consultarmatricula":
                        objDatosMatricula = new SqlParameter[2];
                        objDatosMatricula[0] = new SqlParameter("@DocEstudiante", strIdentEstu);
                        objDatosMatricula[1] = new SqlParameter("@IdDeporte", intIdDeporte);
                        break;
                    case "modificarmatricula":
                        objDatosMatricula = new SqlParameter[3];
                        objDatosMatricula[0] = new SqlParameter("@DocEstudiante", strIdentEstu);
                        objDatosMatricula[1] = new SqlParameter("@IdGrupoViejo", intIdGrupoViejo);
                        objDatosMatricula[2] = new SqlParameter("@IdGrupoNuevo", intIdGrupo);
                        break; 
                    case "eliminardeportematri":
                        objDatosMatricula = new SqlParameter[2];
                        objDatosMatricula[0] = new SqlParameter("@DocEstudiante", strIdentEstu);
                        objDatosMatricula[1] = new SqlParameter("@IdGrupo", intIdGrupo);
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
                string strCampoTexto = "";
                string strCampoId = "";
                switch (ddlGen.ID)
                {
                    case "ddlDeporte":
                        strTipoCons = "DEPORTES";
                        strCampoId = "id";
                        strCampoTexto = "descripcion";
                        break;
                    case "ddlGrupo":
                        strTipoCons = "GRUPOS";
                        strCampoId = "id";
                        strCampoTexto = "descripcion_grupo";
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

                objLlenar.SQL = "[SP_ConsultarComboMatriculas]";
                objLlenar.CampoID = strCampoId;
                objLlenar.CampoTexto = strCampoTexto;
                objLlenar.ParametrosSQL = objDatosMatricula;

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
                strError = ex.Message;
                return false;
            }
        }

        public bool registrar()
        {
            try
            {
                if (!agregarParam("registrarope"))
                {
                    return false;
                }

                objConex = new clsConexionBd(strNombreApp);
                objConex.SQL = "[SP_RegistrarMatricula]";
                objConex.ParametrosSQL = objDatosMatricula;

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

        public bool modificarMatricula()
        {
            try
            {
                if (!agregarParam("modificarMatricula"))
                {
                    return false;
                }
                clsConexionBd objCnx = new clsConexionBd(strNombreApp);

                objCnx.SQL = "[SP_ModificarMatricula]";
                objCnx.ParametrosSQL = objDatosMatricula;

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

        public bool eliminarDeporteMatri()
        {
            try
            {
                if (!agregarParam("eliminarDeporteMatri"))
                {
                    return false;
                }
                clsConexionBd objCnx = new clsConexionBd(strNombreApp);

                objCnx.SQL = "[SP_EliminarDeporteMatri]";
                objCnx.ParametrosSQL = objDatosMatricula;

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

        public bool cargarMatriculas()
        {
            try
            {
                if (!agregarParam("cargarMatriculas"))
                {
                    return false;
                }

                objConex = new clsConexionBd(strNombreApp);
                objConex.SQL = "[SP_CargarMatriculas]";
                objConex.ParametrosSQL = objDatosMatricula;

                if (!objConex.llenarDataSet(false, true))
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

        public bool consultarMatricula()
        {
            try
            {
                if (!agregarParam("consultarMatricula"))
                {
                    return false;
                }
                clsConexionBd objCnx = new clsConexionBd(strNombreApp);

                objCnx.SQL = "[SP_ConsultarMatricula]";
                objCnx.ParametrosSQL = objDatosMatricula;

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

        #endregion
    }
}
