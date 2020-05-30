using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace prjProyectoFinal
{
    public partial class frmGestHijo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region "Metódos Privados"
        public void inhabilitarControles()
        {
            txtUsuario.Enabled = false;
            txtEmail.Enabled = false;
            txtContraseña.Enabled = false;
            txtIdentificacion.Enabled = false;
            txtNombres.Enabled = false;
            txtApellidos.Enabled = false;
            txtFecNac.Enabled = false;
            txtTelefono.Enabled = false;
            txtIdPadre.Enabled = false;
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
                    txtUsuario.Enabled = true;
                    txtEmail.Enabled = true;
                    txtContraseña.Enabled = true;
                    txtIdentificacion.Enabled = true;
                    txtNombres.Enabled = true;
                    txtApellidos.Enabled = true;
                    txtFecNac.Enabled = true;
                    txtTelefono.Enabled = true;
                    txtIdPadre.Enabled = true;
                    pnlRegistrar.Visible = true;
                    break;
                case 2:
                    txtIdentificacion.Enabled = true;
                    pnlModificar.Visible = true;
                    break;
                case 3:
                    txtIdentificacion.Enabled = true;
                    pnlEliminar.Visible = true;
                    break;
                default:
                    goto case 0;
            }
        }
    }
}