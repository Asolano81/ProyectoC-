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
    public partial class frmGestHijo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                strNombreAplica = Assembly.GetExecutingAssembly().GetName().Name + ".xml";
                llenarGiv();
            }
        }

        #region "Variables Globales"
        private static string strNombreAplica;
        #endregion 

        #region "Metódos Privados"
        private void inhabilitarControles(string op)
        {
            switch (op)
            {
                case "cbxAccion_SelectedIndexChanged":
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
                    break;
                case "consultarEstudiante":
                    txtIdentificacion.Enabled = false;
                    txtUsuario.Enabled = true;
                    txtEmail.Enabled = true;
                    txtContraseña.Enabled = true;
                    txtNombres.Enabled = true;
                    txtApellidos.Enabled = true;
                    txtFecNac.Enabled = true;
                    txtTelefono.Enabled = true;
                    btnConsultar1.Enabled = false;
                    btnModificar.Enabled = true;
                    btnConsultar2.Enabled = false;
                    btnEliminar.Enabled = true;
                    consultarEncargado();
                    break;
                case "modificar":
                    /*txtDescripcionGrupo.Enabled = true;
                    ddlDeporte.Enabled = false;
                    ddlDias.Enabled = false;
                    ddlProfesor.Enabled = false;
                    ddlHoraInicio.Enabled = false;*/
                    btnConsultar1.Enabled = true;
                    btnModificar.Enabled = false;
                    break;
                case "eliminar":
                    /*txtDescripcionGrupo.Enabled = true;
                    ddlDeporte.Enabled = false;
                    ddlDias.Enabled = false;
                    ddlProfesor.Enabled = false;
                    ddlHoraInicio.Enabled = false;*/
                    btnConsultar2.Enabled = true;
                    btnEliminar.Enabled = false;
                    break;
                default:
                    goto case "cbxAccion_SelectedIndexChanged";
            }         
        }

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

        private void consultarEncargado()
        {
            try
            {
                string strNombreAplica;
                strNombreAplica = Assembly.GetExecutingAssembly().GetName().Name + ".xml";
                clsUsuariosOPE objUsuario = new clsUsuariosOPE(strNombreAplica);
                if (consultarConexion(objUsuario))
                {
                    if (!objUsuario.Contrasena.Equals(string.Empty))
                    {
                        if (objUsuario.Rol.Equals("Director"))
                        {
                            txtIdPadre.Enabled = true;
                        }
                        else
                        {
                            txtIdPadre.Text = objUsuario.Identificacion;
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
                clsUsuariosOPE objMatri = new clsUsuariosOPE(strNombreAplica);

                objMatri.GvGen = this.gvMatricula;

                if (!objMatri.cargarEstudiantesOpe())
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
                clsUsuariosOPE objMatri = new clsUsuariosOPE(strNombreAplica);

                objMatri.NombreUsuario = this.txtUsuario.Text.Trim();
                objMatri.Email = this.txtEmail.Text.Trim(); ;
                objMatri.Contrasena = this.txtContraseña.Text.Trim(); ;
                objMatri.Identificacion = this.txtIdentificacion.Text.Trim(); ;
                objMatri.Nombre = this.txtNombres.Text.Trim(); ;
                objMatri.Apellido = this.txtApellidos.Text.Trim(); ;
                objMatri.FechaNacimiento = this.txtFecNac.Text.Trim(); ;
                objMatri.Telefono = this.txtTelefono.Text.Trim(); ;
                objMatri.IdentPadre = this.txtIdPadre.Text.Trim(); ;
                objMatri.GvGen = this.gvMatricula;

                if (!objMatri.registrarEstudiante())
                {
                    mostrarMsj(objMatri.Error);
                    objMatri = null;
                    return;
                }
                mostrarMsj(objMatri.Mensaje);
                objMatri = null;
                //limpiarControles();
            }
            catch (Exception ex)
            {
                mostrarMsj(ex.Message);
            }
        }

        private void consultarEstudiante()
        {
            try
            {
                clsUsuariosOPE objMatri = new clsUsuariosOPE(strNombreAplica, "frmGestHijo");

                objMatri.Identificacion = this.txtIdentificacion.Text.Trim();

                if (!objMatri.consultarUsuarioOpe())
                {
                    mostrarMsj(objMatri.Error);
                    objMatri = null;
                    return;
                }
                txtUsuario.Text = objMatri.NombreUsuario;
                txtEmail.Text = objMatri.Email;
                txtContraseña.Text = objMatri.Contrasena;
                txtNombres.Text = objMatri.Nombre;
                txtApellidos.Text = objMatri.Apellido;
                txtFecNac.Text = Convert.ToDateTime(objMatri.FechaNacimiento).ToString("yyyy-MM-dd");
                txtTelefono.Text = objMatri.Telefono;
                txtIdPadre.Text = objMatri.IdentPadre;
                mostrarMsj(objMatri.Mensaje);
                objMatri = null;
                inhabilitarControles("consultarEstudiante");
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
                    txtUsuario.Enabled = true;
                    txtEmail.Enabled = true;
                    txtContraseña.Enabled = true;
                    txtIdentificacion.Enabled = true;
                    txtNombres.Enabled = true;
                    txtApellidos.Enabled = true;
                    txtFecNac.Enabled = true;
                    txtTelefono.Enabled = true;
                    pnlRegistrar.Visible = true;
                    consultarEncargado();
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

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            registrar();
        }

        protected void btnConsultar1_Click(object sender, EventArgs e)
        {
            consultarEstudiante();
        }
    }
}