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
            this.lblMensajeError.Text = mensaje;
            if (mensaje == string.Empty || mensaje == null)
            {
                this.pnlMensaje.Visible = false;
                return;
            }
            this.pnlMensaje.Visible = true;
        }

        public void inhabilitarControles(string op)
        {
            switch (op)
            {
                case "cbxAccion_SelectedIndexChanged":
                    txtDescripcionGrupo.Enabled = false;
                    ddlDeporte.Enabled = false;
                    ddlDias.Enabled = false;
                    ddlProfesor.Enabled = false;
                    ddlHoraInicio.Enabled = false;
                    pnlRegistrar.Visible = false;
                    pnlModificar.Visible = false;
                    pnlEliminar.Visible = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnConsultar1.Enabled = true;
                    btnConsultar2.Enabled = true;
                    break;
                case "consultarGrupo":
                    txtDescripcionGrupo.Enabled = false;
                    ddlDeporte.Enabled = true;
                    ddlDias.Enabled = true;
                    ddlProfesor.Enabled = true;
                    ddlHoraInicio.Enabled = true;
                    btnConsultar1.Enabled = false;
                    btnModificar.Enabled = true;
                    btnConsultar2.Enabled = false;
                    btnEliminar.Enabled = true;
                    break;
                case "modificar":
                    txtDescripcionGrupo.Enabled = true;
                    ddlDeporte.Enabled = false;
                    ddlDias.Enabled = false;
                    ddlProfesor.Enabled = false;
                    ddlHoraInicio.Enabled = false;
                    btnConsultar1.Enabled = true;
                    btnModificar.Enabled = false;
                    break;
                case "eliminar":
                    txtDescripcionGrupo.Enabled = true;
                    ddlDeporte.Enabled = false;
                    ddlDias.Enabled = false;
                    ddlProfesor.Enabled = false;
                    ddlHoraInicio.Enabled = false;
                    btnConsultar2.Enabled = true;
                    btnEliminar.Enabled = false;
                    break;
                default:
                    goto case "cbxAccion_SelectedIndexChanged";
            }
        }

        public void limpiarControles()
        {
            txtDescripcionGrupo.Text = string.Empty;
            txtHoraFin.Text = string.Empty;
            ddlDeporte.SelectedIndex = 0;
            ddlDias.SelectedIndex = 0;
            ddlProfesor.SelectedIndex = 0;
            ddlHoraInicio.SelectedIndex = 0;
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

        private void registrar()
        {
            try
            {
                clsGruposOPE objMatri = new clsGruposOPE(strNombreAplica);

                objMatri.Descripcion = this.txtDescripcionGrupo.Text.Trim();
                objMatri.HoraIn = this.ddlHoraInicio.SelectedItem.Text;
                objMatri.HoraFin = txtHoraFin.Text.Trim();
                objMatri.Dia = this.ddlDias.SelectedItem.Text;
                objMatri.IdDeporte = Convert.ToInt32(this.ddlDeporte.SelectedValue);
                objMatri.IdProfesor = Convert.ToInt32(this.ddlProfesor.SelectedValue);
                objMatri.GvGen = this.gvMatricula;

                if (!objMatri.registrarOpe())
                {
                    mostrarMsj(objMatri.Error);
                    objMatri = null;
                    return;
                }
                mostrarMsj(objMatri.Mensaje);
                objMatri = null;
                limpiarControles();
            }
            catch (Exception ex)
            {
                mostrarMsj(ex.Message);
            }
        }

        private void modificar()
        {
            try
            {
                clsGruposOPE objMatri = new clsGruposOPE(strNombreAplica);

                objMatri.Descripcion = this.txtDescripcionGrupo.Text.Trim();
                objMatri.HoraIn = this.ddlHoraInicio.SelectedItem.Text;
                objMatri.HoraFin = txtHoraFin.Text.Trim();
                objMatri.Dia = this.ddlDias.SelectedItem.Text;
                objMatri.IdDeporte = Convert.ToInt32(this.ddlDeporte.SelectedValue);
                objMatri.IdProfesor = Convert.ToInt32(this.ddlProfesor.SelectedValue);
                objMatri.GvGen = this.gvMatricula;

                if (!objMatri.modificarOpe())
                {
                    mostrarMsj(objMatri.Error);
                    objMatri = null;
                    return;
                }
                mostrarMsj(objMatri.Mensaje);
                objMatri = null;
                limpiarControles();
                inhabilitarControles("modificar");
            }
            catch (Exception ex)
            {
                mostrarMsj(ex.Message);
            }
        }

        private void eliminar()
        {
            try
            {
                clsGruposOPE objMatri = new clsGruposOPE(strNombreAplica);

                objMatri.Descripcion = this.txtDescripcionGrupo.Text.Trim();
                objMatri.GvGen = this.gvMatricula;

                if (!objMatri.eliminarOpe())
                {
                    mostrarMsj(objMatri.Error);
                    objMatri = null;
                    return;
                }
                mostrarMsj(objMatri.Mensaje);
                objMatri = null;
                limpiarControles();
                inhabilitarControles("eliminar");
            }
            catch (Exception ex)
            {
                mostrarMsj(ex.Message);
            }
        }

        private void llenarGiv()
        {
            try
            {
                clsGruposOPE objMatri = new clsGruposOPE(strNombreAplica);

                objMatri.GvGen = this.gvMatricula;

                if (!objMatri.cargarGrupoOpe())
                {
                    mostrarMsj(objMatri.Error);
                    objMatri = null;
                    return;
                }
                mostrarMsj(objMatri.Mensaje);
                objMatri = null;
            }
            catch (Exception ex)
            {
                mostrarMsj(ex.Message);
            }
        }

        private void consultarGrupo()
        {
            try
            {
                clsGruposOPE objMatri = new clsGruposOPE(strNombreAplica);

                objMatri.Descripcion = this.txtDescripcionGrupo.Text.Trim();

                if (!objMatri.consultarGrupoConParOpe())
                {
                    mostrarMsj(objMatri.Error);
                    objMatri = null;
                    return;
                }
                txtDescripcionGrupo.Text = objMatri.Descripcion;
                ddlDias.SelectedValue = objMatri.Dia;
                ddlHoraInicio.SelectedValue = objMatri.HoraIn;
                txtHoraFin.Text = objMatri.HoraFin;
                ddlDeporte.SelectedValue = objMatri.IdDeporte.ToString();
                ddlProfesor.SelectedValue = objMatri.IdProfesor.ToString();
                mostrarMsj(objMatri.Mensaje);
                objMatri = null;
                inhabilitarControles("consultarGrupo");
            }
            catch (Exception ex)
            {
                mostrarMsj(ex.Message);
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                strNombreAplica = Assembly.GetExecutingAssembly().GetName().Name + ".xml";
                llenarDropDown(this.ddlDeporte);
                llenarDropDown(this.ddlProfesor);
                llenarGiv();     
            }
        }

        protected void cbxHoraInicio_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlHoraInicio.SelectedIndex !=0)
            {
                if (ddlHoraInicio.SelectedIndex != 8)
                {
                    ddlHoraInicio.SelectedIndex = ddlHoraInicio.SelectedIndex + 1;
                    txtHoraFin.Text = ddlHoraInicio.SelectedValue;
                    ddlHoraInicio.SelectedIndex = ddlHoraInicio.SelectedIndex + -1;
                }
                else
                {
                    txtHoraFin.Text = "22:00";
                }
            }
            else
            {
                txtHoraFin.Text = string.Empty;
            }
        }

        protected void cbxAccion_SelectedIndexChanged(object sender, EventArgs e)
        {
            inhabilitarControles("cbxAccion_SelectedIndexChanged");
            int op = cbxAccion.SelectedIndex;
            switch (op)
            {
                case 0:
                    inhabilitarControles("cbxAccion_SelectedIndexChanged");
                    break;
                case 1:
                    txtDescripcionGrupo.Enabled = true;
                    ddlDeporte.Enabled = true;
                    ddlDias.Enabled = true;
                    ddlProfesor.Enabled = true;
                    ddlHoraInicio.Enabled = true;
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
            limpiarControles();
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            registrar();
        }

        protected void btnConsultar1_Click(object sender, EventArgs e)
        {
            consultarGrupo();           
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            modificar();
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            eliminar();
        }

        protected void btnConsultar2_Click(object sender, EventArgs e)
        {
            consultarGrupo();
        }
    }
}