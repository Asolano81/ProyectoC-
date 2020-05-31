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
    public partial class frmGestGrupo : System.Web.UI.Page
    {
        #region "Variables Globales"
        private static string strNombreAplica;
        #endregion 

        #region "Métodos Privados"
        private void mostrarMsj(string mensaje)
        {
            this.lblAccion.Text = mensaje;
            if (mensaje == string.Empty)
            {
                this.lblAccion.Visible = false;
                return;
            }
            this.lblAccion.Visible = true;
        }

        private void llenarDropDown(DropDownList ddlDrop)
        {
            try
            {
                clsGruposOPE objOpe = new clsGruposOPE(strNombreAplica);

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
        #endregion

        #region "Metódos Privados"
        public void inhabilitarControles()
        {
            txtDescripcionGrupo.Enabled = false;
            ddlDeporte.Enabled = false;
            ddlDias.Enabled = false;
            cbxProfesor.Enabled = false;
            cbxHoraInicio.Enabled = false;
            pnlRegistrar.Visible = false;
            pnlModificar.Visible = false;
            pnlEliminar.Visible = false;
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                strNombreAplica = Assembly.GetExecutingAssembly().GetName().Name + ".xml";
                llenarDropDown(this.ddlDeporte);
            }
        }

        protected void cbxHoraInicio_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxHoraInicio.SelectedIndex !=0)
            {
                if (cbxHoraInicio.SelectedIndex != 14 && cbxHoraInicio.SelectedIndex != 15)
                {
                    cbxHoraInicio.SelectedIndex = cbxHoraInicio.SelectedIndex + 2;
                    txtHoraFin.Text = cbxHoraInicio.SelectedValue;
                    cbxHoraInicio.SelectedIndex = cbxHoraInicio.SelectedIndex + -2;
                }
                else
                {
                    if (cbxHoraInicio.SelectedIndex == 14)
                    {
                        txtHoraFin.Text = "21:00";
                    }
                    else
                    {
                        txtHoraFin.Text = "22:00";
                    }
                }
            }
            else
            {
                txtHoraFin.Text = string.Empty;
            }
        }

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
                    txtDescripcionGrupo.Enabled = true;
                    ddlDeporte.Enabled = true;
                    ddlDias.Enabled = true;
                    cbxProfesor.Enabled = true;
                    cbxHoraInicio.Enabled = true;
                    pnlRegistrar.Visible = true;
                    break;
                case 2:
                    txtDescripcionGrupo.Enabled = true;
                    pnlModificar.Visible = true;
                    break;
                case 3:
                    txtDescripcionGrupo.Enabled = true;
                    pnlEliminar.Visible = true;
                    break;
                default:
                    goto case 0;
            }
        }
    }
}