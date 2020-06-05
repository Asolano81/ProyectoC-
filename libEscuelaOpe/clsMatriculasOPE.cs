using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using libEscuelaRN;
using libLlenarGrids;
namespace libEscuelaOpe
{
    public class clsMatriculasOPE
    {
        #region "Atributos"
        private string strNombreApp;
        private string strIdentEstu;
        private string strFechDeMatr;
        private int intIdDeporte;
        private int intIdProfesor;
        private int intIdGrupo;
        private int intIdGrupoViejo;
        private string strError;
        private DataSet dsDatos;
        private string strMensaje;
        DropDownList ddlGen;
        GridView gvGen;
        #endregion

        #region "Constructor"
        public clsMatriculasOPE(string nombreApp)
        {
            this.strNombreApp = nombreApp;
            strError = string.Empty;
            strIdentEstu = string.Empty;
            strFechDeMatr = string.Empty;
            intIdDeporte = -1;
            intIdProfesor = -1;
            intIdGrupo = -1;
            intIdGrupoViejo = -1;
            strMensaje = string.Empty;
        }
        #endregion

        #region "Propiedades"
        public string Error { get => strError; }
        public DataSet DatosRpta { get => dsDatos; }
        public string Mensaje { get => strMensaje; }
        public DropDownList DdlGen { set => ddlGen = value; }
        public GridView GvGen { set => gvGen = value; }
        public string IdentEstu { get => strIdentEstu; set => strIdentEstu = value; }
        public string FechDeMatr { get => strFechDeMatr; set => strFechDeMatr = value; }
        public int IdDeporte { get => intIdDeporte; set => intIdDeporte = value; }
        public int IdProfesor { get => intIdProfesor; set => intIdProfesor = value; }
        public int IdGrupo { get => intIdGrupo; set => intIdGrupo = value; }
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
                case "consultarMatriculaOpe":
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
                case "modificarOpe":
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
                case "eliminarOpe":
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

        private bool llenarGrid(DataTable dtDatos)
        {
            try
            {
                if (!validar("llenarGrid"))
                {
                    return false;
                }
                clsLlenarGrids objGrids = new clsLlenarGrids();

                if (!objGrids.llenarGridWeb(gvGen, dtDatos))
                {
                    strError = objGrids.Error;
                    objGrids = null;
                    return false;
                }
                objGrids = null;
                return true;

            }
            catch (Exception ex)
            {

                strError = ex.Message;
                return false;
            }
        }
        #endregion

        #region "Métodos públicos"

        public bool llenarDrop()
        {
            try
            {
                if (!validar("llenarDrop"))
                {
                    return false;
                }

                clsMatriculasRN objRn = new clsMatriculasRN(strNombreApp);

                switch (ddlGen.ID.ToLower())
                {
                    case "ddlgrupo":
                        objRn.IdDeporte = intIdDeporte;
                        break;
                }

                if (!objRn.llenarDropDowns(ddlGen))
                {
                    strError = objRn.Error;
                    objRn = null;
                    return false;
                }
                objRn = null;
                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }

        public bool registrarOpe()
        {
            try
            {
                if (!validar("registrarOpe"))
                {
                    return false;
                }
                clsMatriculasRN objRn = new clsMatriculasRN(this.strNombreApp);

                objRn.IdentEstu = strIdentEstu;
                objRn.FechDeMatr = strFechDeMatr;
                objRn.IdGrupo = intIdGrupo;
                objRn.IdDeporte = intIdDeporte;

                if (!objRn.registrar())
                {
                    strError = objRn.Error;
                    objRn = null;
                    return false;
                }
                if (objRn.DatosRptaBd.Tables[0].Rows[0]["CodRpta"].ToString() == "1")
                {
                    strError = objRn.DatosRptaBd.Tables[0].Rows[0]["Mensaje"].ToString();
                    objRn = null;
                    return false;
                }
                if (objRn.DatosRptaBd.Tables.Count > 1)
                {
                    if (!llenarGrid(objRn.DatosRptaBd.Tables[1]))
                    {
                        objRn = null;
                        return false;
                    }
                }
                strMensaje = objRn.DatosRptaBd.Tables[0].Rows[0]["Mensaje"].ToString();
                objRn = null;
                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }

        public bool modificarOpe()
        {
            try
            {
                if (!validar("modificarOpe"))
                {
                    return false;
                }
                clsMatriculasRN objRn = new clsMatriculasRN(this.strNombreApp);

                objRn.IdentEstu = strIdentEstu;
                objRn.IdGrupo = intIdGrupo;
                objRn.IdGrupoViejo = intIdGrupoViejo;

                if (!objRn.modificarMatricula())
                {
                    strError = objRn.Error;
                    objRn = null;
                    return false;
                }
                if (objRn.DatosRptaBd.Tables[0].Rows[0]["CodRpta"].ToString() == "1")
                {
                    strError = objRn.DatosRptaBd.Tables[0].Rows[0]["Mensaje"].ToString();
                    objRn = null;
                    return false;
                }
                if (objRn.DatosRptaBd.Tables.Count > 1)
                {
                    if (!llenarGrid(objRn.DatosRptaBd.Tables[1]))
                    {
                        objRn = null;
                        return false;
                    }
                }
                strMensaje = objRn.DatosRptaBd.Tables[0].Rows[0]["Mensaje"].ToString();
                objRn = null;
                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }

        public bool eliminarOpe()
        {
            try
            {
                if (!validar("eliminarOpe"))
                {
                    return false;
                }
                clsMatriculasRN objRn = new clsMatriculasRN(this.strNombreApp);

                objRn.IdentEstu = strIdentEstu;
                objRn.IdGrupo = intIdGrupo;

                if (!objRn.eliminarDeporteMatri())
                {
                    strError = objRn.Error;
                    objRn = null;
                    return false;
                }
                if (objRn.DatosRptaBd.Tables[0].Rows[0]["CodRpta"].ToString() == "1")
                {
                    strError = objRn.DatosRptaBd.Tables[0].Rows[0]["Mensaje"].ToString();
                    objRn = null;
                    return false;
                }
                if (objRn.DatosRptaBd.Tables.Count > 1)
                {
                    if (!llenarGrid(objRn.DatosRptaBd.Tables[1]))
                    {
                        objRn = null;
                        return false;
                    }
                }
                strMensaje = objRn.DatosRptaBd.Tables[0].Rows[0]["Mensaje"].ToString();
                objRn = null;
                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }

        public bool consultarMatriculaOpe()
        {
            try
            {
                if (!validar("consultarMatriculaOpe"))
                {
                    return false;
                }
                clsMatriculasRN objRn = new clsMatriculasRN(this.strNombreApp);

                objRn.IdentEstu = strIdentEstu;
                objRn.IdDeporte = intIdDeporte;

                if (!objRn.consultarMatricula())
                {
                    strError = objRn.Error;
                    objRn = null;
                    return false;
                }

                if (objRn.DatosRptaBd.Tables[0].Rows[0]["CodRpta"].ToString() == "1")
                {
                    strError = objRn.DatosRptaBd.Tables[0].Rows[0]["Mensaje"].ToString();
                    objRn = null;
                    return false;
                }

                intIdGrupo = Convert.ToInt32(objRn.DatosRptaBd.Tables[1].Rows[0]["id_grupo"].ToString());
                strFechDeMatr = objRn.DatosRptaBd.Tables[1].Rows[0]["fecha_matricula"].ToString();
                strMensaje = objRn.DatosRptaBd.Tables[0].Rows[0]["Mensaje"].ToString();

                objRn = null;
                return true;
            }
            catch (Exception ex)
            {
                strMensaje =  ex.Message.ToString();
                return false;
            }
        }

        public bool cargarMatriculasOpe()
        {
            try
            {
                if (!validar("cargarMatriculasOpe"))
                {
                    return false;
                }
                clsMatriculasRN objRn = new clsMatriculasRN(this.strNombreApp);

                if (!objRn.cargarMatriculas())
                {
                    strError = objRn.Error;
                    objRn = null;
                    return false;
                }

                if (!llenarGrid(objRn.DatosRptaBd.Tables[0]))
                {
                    objRn = null;
                    return false;
                }
                objRn = null;
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
