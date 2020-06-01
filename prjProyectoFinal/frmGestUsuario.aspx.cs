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
    public partial class frmGestUsuario : System.Web.UI.Page
    {


        #region "Variables Globales"
        private static string strNombreAplica;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            strNombreAplica = Assembly.GetExecutingAssembly().GetName().Name + ".xml";
            llenarDropDown(ddlRol);
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
            ddlRol.SelectedIndex = 0;
            ddlRol.Enabled = false;
            pnlRegistrar.Visible = false;
            pnlModificar.Visible = false;
            pnlEliminar.Visible = false;
        }

        private void mostrarMsj(string mensaje)
        {
            this.lblMensajeErrorCargaLista.Text = mensaje;
            this.pnlMensaje.Visible = true;
        }

        private void llenarDropDown(DropDownList ddlDrop)
        {
            try
            {
                clsRolesOPE objOpe = new clsRolesOPE(strNombreAplica);


                objOpe.DdlGen = ddlDrop;

                if (!objOpe.llenarDrop())
                {
                    mostrarMsj(objOpe.Error);
                    return;
                }
                excluirEstudiante();
            }
            catch (Exception ex)
            {

                mostrarMsj(ex.Message);
            }
        }       

        private void excluirEstudiante()
        {
            ddlRol.Items.RemoveAt(4);
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
                    ddlRol.Enabled = true;
                    pnlRegistrar.Visible = true;
                    break;
                case 2:
                    txtIdentificacion.Enabled = true;
                    ddlRol.Enabled = true;
                    pnlModificar.Visible = true;
                    break;
                case 3:
                    txtIdentificacion.Enabled = true;
                    ddlRol.Enabled = true;
                    pnlEliminar.Visible = true;
                    break;
                default:
                    goto case 0;
            }
        }
    }
}