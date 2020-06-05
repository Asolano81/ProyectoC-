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

        private void inhabilitarControles(string op)
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
                    pnlMensaje.Visible = false;
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
                case "btnConsultar2_Click":
                    txtDescripcionGrupo.Enabled = false;
                    ddlDeporte.Enabled = false;
                    ddlDias.Enabled = false;
                    ddlProfesor.Enabled = false;
                    ddlHoraInicio.Enabled = false;
                    btnConsultar2.Enabled = false;
                    btnEliminar.Enabled = true;
                    break;
                default:
                    goto case "cbxAccion_SelectedIndexChanged";
            }
        }

        private void limpiarControles()
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

        private bool validar(string strMetodoOrigen)
        {
            switch (strMetodoOrigen.ToLower())
            {
                case "registrar":
                    if (this.txtDescripcionGrupo.Text.Trim() == string.Empty)
                    {
                        mostrarMsj("Debe ingresar una Descripcion(Nombre o Codigo de grupo)");
                        return false;
                    }
                    if (this.ddlDeporte.SelectedIndex == 0)
                    {
                        mostrarMsj("Debe seleccionar un Deporte");
                        return false;
                    }
                    if (this.ddlProfesor.SelectedIndex == 0)
                    {
                        mostrarMsj("Debe seleccionar un profesor");
                        return false;
                    }
                    if (this.ddlDias.SelectedIndex == 0)
                    {
                        mostrarMsj("Debe seleccionar un Día");
                        return false;
                    }
                    if (this.ddlHoraInicio.SelectedIndex == 0)
                    {
                        mostrarMsj("Debe seleccionar una hora de inicio");
                        return false;
                    }     
                    break;
                case "modificar":
                    goto case "registrar";
                case "consultargrupo":
                    if (this.txtDescripcionGrupo.Text.Trim() == string.Empty)
                    {
                        mostrarMsj("Debe ingresar la Descripcion(Nombre o Codigo de grupo) para poder consultarlo");
                        return false;
                    }
                    break;
            }
            return true;
        }

        private void registrar()
        {
            try
            {
                if (!validar("registrar"))
                {
                    return;
                }
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
                if (!validar("modificar"))
                {
                    return;
                }
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

        private bool consultarGrupo()
        {
            if (!validar("consultarGrupo"))
            {
                return false;
            }
            try
            {
                clsGruposOPE objMatri = new clsGruposOPE(strNombreAplica);

                objMatri.Descripcion = this.txtDescripcionGrupo.Text.Trim();

                if (!objMatri.consultarGrupoConParOpe())
                {
                    mostrarMsj(objMatri.Error);
                    objMatri = null;
                    return false;
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
                return true;
            }
            catch (Exception ex)
            {
                mostrarMsj(ex.Message);
                return false;
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
            if (consultarGrupo())
            {
                inhabilitarControles("btnConsultar2_Click");
            }
            
        }
    }
}