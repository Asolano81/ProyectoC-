using libEscuelaOpe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace prjProyectoFinal
{
    public partial class frmMatricularDep : System.Web.UI.Page
    {
        #region "Variables Globales"
        private static string strNombreAplica;
        private static string strIdGrupoViejo;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                strNombreAplica = Assembly.GetExecutingAssembly().GetName().Name + ".xml";
                llenarDropDown(ddlDeporte);
                llenarGiv();
            }
            
        }

        #region "Metódos Privados"

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

        private void llenarDropDown(DropDownList ddlDrop)
        {
            try

            {
                clsMatriculasOPE objOpe = new clsMatriculasOPE(strNombreAplica);

                objOpe.DdlGen = ddlDrop;
                objOpe.IdDeporte = ddlDeporte.SelectedIndex;

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

        private void inhabilitarControles(string op)
        {
            switch (op)
            {
                case "cbxAccion_SelectedIndexChanged":
                    txtIdentEstu.Enabled = false;
                    txtFechaDeMatr.Text = string.Empty;
                    ddlDeporte.Enabled = false;
                    ddlGrupo.Enabled = false;
                    pnlRegistrar.Visible = false;
                    pnlModificar.Visible = false;
                    pnlEliminar.Visible = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnConsultar1.Enabled = true;
                    btnConsultar2.Enabled = true;
                    pnlMensaje.Visible = false;
                    break;
                case "consultar":
                    txtIdentEstu.Enabled = false;
                    ddlDeporte.Enabled = false;
                    ddlGrupo.Enabled = true;
                    btnConsultar1.Enabled = false;
                    btnModificar.Enabled = true;
                    btnConsultar2.Enabled = false;
                    btnEliminar.Enabled = true;
                    break;
                case "modificar":
                    txtIdentEstu.Text = string.Empty;
                    txtFechaDeMatr.Text = string.Empty;
                    consultarEncargado();
                    ddlDeporte.Enabled = true;
                    ddlGrupo.Enabled = false;
                    btnConsultar1.Enabled = true;
                    btnModificar.Enabled = false;
                    break;
                case "eliminar":
                    txtIdentEstu.Text = string.Empty;
                    txtFechaDeMatr.Text = string.Empty;
                    consultarEncargado();
                    ddlDeporte.Enabled = true;
                    ddlGrupo.Enabled = false;
                    btnConsultar2.Enabled = true;
                    btnEliminar.Enabled = false;
                    break;
                case "btnConsultar2_Click":
                    txtIdentEstu.Enabled = false;
                    ddlDeporte.Enabled = false;
                    ddlGrupo.Enabled = false;
                    btnConsultar2.Enabled = false;
                    btnEliminar.Enabled = true;
                    break;
                default:
                    goto case "cbxAccion_SelectedIndexChanged";
            }
        }

        private void llenarInfoGrupo()
        {
            clsGruposOPE objGrupo = new clsGruposOPE(strNombreAplica);
            clsUsuariosOPE objUsuario = new clsUsuariosOPE(strNombreAplica);

            if (consultarGrupo(objGrupo))
            {

                txtProfesor.Text = objGrupo.NomProf;
                txtDias.Text = objGrupo.Dia;
                txtHoraInicio.Text = objGrupo.HoraIn;
                txtHoraFin.Text = objGrupo.HoraFin;
            }
        }

        private bool validar(string strMetodoOrigen)
        {
            switch (strMetodoOrigen.ToLower())
            {
                case "registrar":
                    if (this.txtIdentEstu.Text.Trim() == string.Empty)
                    {
                        mostrarMsj("Debe ingresar la Identificación del estudiante");
                        return false;
                    }
                    if (this.ddlDeporte.SelectedIndex == 0)
                    {
                        mostrarMsj("Debe seleccionar un Deporte");
                        return false;
                    }
                    if (this.ddlGrupo.SelectedIndex == 0)
                    {
                        mostrarMsj("Debe seleccionar un Grupo");
                        return false;
                    }
                    break;
                case "consultar":
                    if (this.txtIdentEstu.Text.Trim() == string.Empty)
                    {
                        mostrarMsj("Debe ingresar la Identificación del estudiante");
                        return false;
                    }
                    if (this.ddlDeporte.SelectedIndex == 0)
                    {
                        mostrarMsj("Debe seleccionar un Deporte");
                        return false;
                    }
                    break;
                case "modificar":
                    goto case "registrar";

            }
            return true;
        }

        private void limpiarControles(string op)
        {
            if (op.Equals("ddlGrupo_SelectedIndexChanged") || op.Equals("ddlDeporte_SelectedIndexChanged"))
            {
                txtProfesor.Text = string.Empty;
                txtDias.Text = string.Empty;
                txtHoraInicio.Text = string.Empty;
                txtHoraFin.Text = string.Empty;
                return;
            }

            txtIdentEstu.Text = string.Empty;
            ddlDeporte.SelectedIndex = 0;
            ddlGrupo.Items.Clear();
            txtProfesor.Text = string.Empty;
            txtDias.Text = string.Empty;
            txtHoraInicio.Text = string.Empty;
            txtHoraFin.Text = string.Empty;

            if (op.Equals("eliminar") || op.Equals("modificar"))
            {
                txtFechaDeMatr.Text = string.Empty;
            }
        }

        private bool consultarGrupo(clsGruposOPE objGrupo)
        {
            try
            {
                objGrupo.Descripcion = ddlGrupo.SelectedItem.ToString();

                if (!objGrupo.consultarGrupoConParOpe())
                {
                    mostrarMsj(objGrupo.Error);
                    objGrupo = null;
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                mostrarMsj(ex.Message);
                return false;
            }
        }

        private void consultarEncargado()
        {
            try
            {
                clsUsuariosOPE objUsuario = new clsUsuariosOPE(strNombreAplica);
                if (consultarConexion(objUsuario))
                {
                    if (!objUsuario.Contrasena.Equals(string.Empty))
                    {
                        if (objUsuario.Rol.Equals("Director"))
                        {
                            txtIdentEstu.Enabled = true;
                        }
                        else
                        {
                            txtIdentEstu.Text = objUsuario.Identificacion;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                mostrarMsj(ex.Message);
            }

        }

        private bool consultarConexion(clsUsuariosOPE objConsEst)
        {
            try
            {
                if (!objConsEst.consConex())
                {
                    mostrarMsj(objConsEst.Error);
                    objConsEst = null;
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                mostrarMsj(ex.Message);
                return false;
            }
        }

        private void llenarGiv()
        {
            try
            {
                clsMatriculasOPE objMatri = new clsMatriculasOPE(strNombreAplica);

                objMatri.GvGen = this.gvMatricula;

                if (!objMatri.cargarMatriculasOpe())
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

        private void registrar()
        {
            try
            {
                if (!validar("registrar"))
                {
                    return;
                }

                clsMatriculasOPE objMatri = new clsMatriculasOPE(strNombreAplica);

                objMatri.IdentEstu = this.txtIdentEstu.Text.Trim();
                objMatri.FechDeMatr = this.txtFechaDeMatr.Text.Trim();
                objMatri.IdGrupo = Convert.ToInt32( this.ddlGrupo.SelectedValue);
                objMatri.IdDeporte = Convert.ToInt32(this.ddlDeporte.SelectedValue);
                objMatri.GvGen = this.gvMatricula;

                if (!objMatri.registrarOpe())
                {
                    mostrarMsj(objMatri.Error);
                    objMatri = null;
                    return;
                }
                mostrarMsj(objMatri.Mensaje);
                objMatri = null;
                limpiarControles("");
                consultarEncargado();
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

                clsMatriculasOPE objMatri = new clsMatriculasOPE(strNombreAplica);

                objMatri.IdentEstu = this.txtIdentEstu.Text.Trim();
                objMatri.IdGrupo = Convert.ToInt32(this.ddlGrupo.SelectedValue);
                objMatri.IdGrupoViejo = Convert.ToInt32(strIdGrupoViejo);
                objMatri.GvGen = this.gvMatricula;

                if (!objMatri.modificarOpe())
                {
                    mostrarMsj(objMatri.Error);
                    objMatri = null;
                    return;
                }
                mostrarMsj(objMatri.Mensaje);
                objMatri = null;
                limpiarControles("");
                inhabilitarControles("modificar");
            }
            catch (Exception ex)
            {
                mostrarMsj(ex.Message);
            }
        }

        private bool consultar()
        {
            try
            {
                if (!validar("consultar"))
                {
                    return false;
                }

                clsMatriculasOPE objMatri = new clsMatriculasOPE(strNombreAplica);

                objMatri.IdentEstu = this.txtIdentEstu.Text.Trim();
                objMatri.IdDeporte = Convert.ToInt32(this.ddlDeporte.SelectedValue);

                if (!objMatri.consultarMatriculaOpe())
                {
                    mostrarMsj(objMatri.Error);
                    objMatri = null;
                    return false;
                }

                strIdGrupoViejo = objMatri.IdGrupo.ToString();
                ddlGrupo.SelectedValue = strIdGrupoViejo;
                txtFechaDeMatr.Text = Convert.ToDateTime(objMatri.FechDeMatr).ToString("yyyy-MM-dd");
                llenarInfoGrupo();

                mostrarMsj(objMatri.Mensaje);
                objMatri = null;

                inhabilitarControles("consultar");
                return true;
            }
            catch (Exception ex)
            {
                mostrarMsj(ex.Message);
                return false;
            }
        }

        private void eliminar()
        {
            try
            {
                if (!validar("consultarEstudiante"))
                {
                    return;
                }

                clsMatriculasOPE objMatri = new clsMatriculasOPE(strNombreAplica);

                objMatri.IdentEstu = this.txtIdentEstu.Text.Trim();
                objMatri.IdGrupo = Convert.ToInt32(ddlGrupo.SelectedValue);
                objMatri.GvGen = this.gvMatricula;

                if (!objMatri.eliminarOpe())
                {
                    mostrarMsj(objMatri.Error);
                    objMatri = null;
                    return;
                }
                mostrarMsj(objMatri.Mensaje);
                objMatri = null;
                limpiarControles("eliminar");
                inhabilitarControles("eliminar");
            }
            catch (Exception ex)
            {
                mostrarMsj(ex.Message);
            }
        }

        #endregion

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
                    ddlDeporte.Enabled = true;
                    ddlGrupo.Enabled = true;
                    pnlRegistrar.Visible = true;
                    txtFechaDeMatr.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    consultarEncargado();
                    break;
                case 2:
                    ddlDeporte.Enabled = true;
                    pnlModificar.Visible = true;
                    consultarEncargado();
                    break;
                case 3:
                    ddlDeporte.Enabled = true;
                    pnlEliminar.Visible = true;
                    consultarEncargado();
                    break;
                default:
                    goto case 0;
            }
        }

        protected void ddlDeporte_SelectedIndexChanged(object sender, EventArgs e)
        {
            limpiarControles("ddlDeporte_SelectedIndexChanged");
            llenarDropDown(ddlGrupo);
        }

        protected void ddlGrupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlGrupo.SelectedIndex != 0)
            { 
                llenarInfoGrupo();
            }
            else
            {
                limpiarControles("ddlGrupo_SelectedIndexChanged");
            }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            registrar();
        }

        protected void btnConsultar1_Click(object sender, EventArgs e)
        {
            consultar();
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            modificar();
        }

        protected void btnConsultar2_Click(object sender, EventArgs e)
        {
            if (consultar())
            {
                inhabilitarControles("btnConsultar2_Click");
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            eliminar();
        }
    }
}