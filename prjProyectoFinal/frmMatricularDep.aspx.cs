using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace prjProyectoFinal
{
    public partial class frmMatricularDep : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region "Metódos Privados"
        public void inhabilitarControles()
        {
            txtIdMatricula.Enabled = false;
            cbxDeporte.Enabled = false;
            cbxGrupo.Enabled = false;
            pnlRegistrar.Visible = false;
            pnlModificar.Visible = false;
            pnlEliminar.Visible = false;
        }
        #endregion

        protected void cbxAccion_SelectedIndexChanged(object sender, EventArgs e)
        {
            inhabilitarControles();
            int op = cbxAccion.SelectedIndex;
            switch (op)
            {
                case 0:
                    inhabilitarControles();
                    break;
                case 1:
                    txtIdMatricula.Enabled = true;
                    cbxDeporte.Enabled = true;
                    cbxGrupo.Enabled = true;
                    pnlRegistrar.Visible = true;
                    break;
                case 2:
                    txtIdMatricula.Enabled = true;
                    cbxDeporte.Enabled = true;
                    pnlModificar.Visible = true;
                    break;
                case 3:
                    txtIdMatricula.Enabled = true;
                    cbxDeporte.Enabled = true;
                    pnlEliminar.Visible = true;
                    break;
                default:
                    goto case 0;
            }
        }
    }
}