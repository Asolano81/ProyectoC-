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
    public class clsGruposOPE
    {
        #region "Atributos"
        private string strNombreApp;
        private string strError;
        private DataSet dsDatos;
        private string strMensaje;
        DropDownList ddlGen;
        GridView gvGen;
        #endregion

        #region "Constructor"
        public clsGruposOPE(string nombreApp)
        {
            this.strNombreApp = nombreApp;
            strError = string.Empty;
        }
        #endregion

        #region "Propiedades"
        public string Error { get => strError; }
        public DataSet DatosRpta { get => dsDatos; }
        public string Mensaje { get => strMensaje; }
        public DropDownList DdlGen { set => ddlGen = value; }
        public GridView GvGen { set => gvGen = value; }
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

                clsGruposRN objRn = new clsGruposRN(strNombreApp);
                
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

                throw ex;
            }
        }
        #endregion
    }
}
