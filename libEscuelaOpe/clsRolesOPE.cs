using libEscuelaRN;
using libLlenarGrids;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace libEscuelaOpe
{
    public class clsRolesOPE
    {

        #region "Atributos"
        private int intIdRol;
        private string strDescripcion;
        private string strNombreApp;
        private string strDocumento;
        private string strError;
        private DataSet dsDatos;
        private string strMensaje;
        DropDownList ddlGen;
        GridView gvGen;
        #endregion

        #region "Constructor"
        public clsRolesOPE(string nombreApp)
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
        public DataSet DatosRpta { get => dsDatos; }
        public DropDownList DdlGen { get => ddlGen; set => ddlGen = value; }
        public GridView GvGen { get => gvGen; set => gvGen = value; }
        public string Error { get => strError; }
        public string Documento { get => strDocumento; set => strDocumento = value; }
        public string Mensaje { get => strMensaje; set => strMensaje = value; }

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
                    if (intIdRol <= 0)
                    {
                        strError = "Debe seleccionar un rol";
                        return false;
                    }
                    break;
                case "consultarOpe":
                    if (strDocumento == string.Empty)
                    {
                        strError = "El documento no puede estar vacio";
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

        #region "Metodos publicos"
        public bool llenarDrop()
        {
            try
            {
                clsRolesRN objRn = new clsRolesRN(strNombreApp);
                switch (DdlGen.ID.ToLower())
                {
                    case "ddlRol":
                        objRn.IdRol = intIdRol;
                        objRn.Descripcion = strDescripcion;
                        break;
                }

                if (!objRn.llenarDropDowns(DdlGen))
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

                throw ex;
            }
        }

        public bool consultarOpe()
        {
            try
            {
                if (!validar("consultarOpe"))
                {
                    return false;
                }
                clsRolesRN objRn = new clsRolesRN(this.strNombreApp);

                objRn.Documento = strDocumento;

                if (!objRn.consultar())
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

        public bool registrarOpe()
        {
            try
            {
                if (!validar("registrarOpe"))
                {
                    return false;
                }
                clsRolesRN objRn = new clsRolesRN(this.strNombreApp);

                objRn.Documento = strDocumento;
                objRn.IdRol = intIdRol;

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
                    if (!llenarGrid(objRn.DatosRptaBd.Tables[2]))
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
        #endregion


    }
}
