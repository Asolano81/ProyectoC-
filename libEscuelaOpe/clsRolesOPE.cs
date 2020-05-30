using System;
using System.Data;
using libEscuelaRN;
using System.Web.UI.WebControls;

namespace libEscuelaOpe
{
    public class clsRolesOPE
    {

        #region "Atributos"
        private int intIdRol;
        private string strDescripcion;
        private string strNombreApp;
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


        }
        #endregion
        #region "Propiedades"
        public int IdRol { get => intIdRol; set => intIdRol = value; }
        public string Descripcion { get => strDescripcion; set => strDescripcion = value; }
        public DropDownList DdlGen { get => ddlGen; set => ddlGen = value; }
        public GridView GvGen { get => gvGen; set => gvGen = value; }
        public string Error { get => strError; }

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
                        objRn.IdRol =intIdRol;
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
        #endregion


    }
}
