using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using libEscuelaOpe;

namespace prjProyectoFinal
{
    public partial class frmLogin : System.Web.UI.Page
    {
        #region "Variables Globales"
        private static string strNombreAplica;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                strNombreAplica = Assembly.GetExecutingAssembly().GetName().Name + ".xml";
                llenarDropDown(this.ddlRol);
            }


        }

        #region "Métodos Privados"
        private void mostrarMsj(string mensaje)
        {
            this.lblUsuario.Text = mensaje;
            if (mensaje == string.Empty)
            {
                this.lblUsuario.Visible = false;
                return;
            }
            this.lblUsuario.Visible = true;
        }

        #endregion
        private void llenarDropDown(DropDownList ddlDrop)
        {
            try
            {
                clsRolesOPE objOpe = new clsRolesOPE(strNombreAplica);
              
                //limpiar(ddlDrop.ID);
                objOpe.DdlGen = ddlDrop;

                if (!objOpe.llenarDrop())
                {
                    mostrarMsj(objOpe.Error);
                    return;
                }
            }
            catch (Exception ex)
            {

                mostrarMsj(ex.Message);
            }
        }
    }
}