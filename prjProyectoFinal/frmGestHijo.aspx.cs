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
                    pnlMensaje.Visible = false;
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
                    txtIdentificacion.Enabled = true;
                    txtUsuario.Enabled = false;
                    txtEmail.Enabled = false;
                    txtContraseña.Enabled = false;
                    txtNombres.Enabled = false;
                    txtApellidos.Enabled = false;
                    txtFecNac.Enabled = false;
                    txtTelefono.Enabled = false;
                    txtIdPadre.Enabled = false;
                    btnConsultar1.Enabled = true;
                    btnModificar.Enabled = false;
                    break;
                case "eliminar":
                    txtIdentificacion.Enabled = true;
                    txtUsuario.Enabled = false;
                    txtEmail.Enabled = false;
                    txtContraseña.Enabled = false;
                    txtNombres.Enabled = false;
                    txtApellidos.Enabled = false;
                    txtFecNac.Enabled = false;
                    txtTelefono.Enabled = false;
                    txtIdPadre.Enabled = false;
                    btnConsultar2.Enabled = true;
                    btnEliminar.Enabled = false;
                    break;
                case "btnConsultar2_Click":
                    txtIdentificacion.Enabled = false;
                    txtUsuario.Enabled = false;
                    txtEmail.Enabled = false;
                    txtContraseña.Enabled = false;
                    txtNombres.Enabled = false;
                    txtApellidos.Enabled = false;
                    txtFecNac.Enabled = false;
                    txtTelefono.Enabled = false;
                    txtIdPadre.Enabled = false;
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

        private void limpiarControles()
        {
            this.txtUsuario.Text = string.Empty;
            this.txtEmail.Text = string.Empty;
            this.txtContraseña.Text = string.Empty;
            this.txtIdentificacion.Text = string.Empty;
            this.txtNombres.Text= string.Empty;
            this.txtApellidos.Text = string.Empty;
            this.txtFecNac.Text = string.Empty;
            this.txtTelefono.Text = string.Empty;
            this.txtIdPadre.Text = string.Empty;
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

        private bool validar(string strMetodoOrigen)
        {
            switch (strMetodoOrigen.ToLower())
            {
                case "registrar":
                    if (this.txtUsuario.Text.Trim() == string.Empty)
                    {
                        mostrarMsj("Debe ingresar un Usuario");
                        return false;
                    }
                    if (this.txtEmail.Text.Trim() == string.Empty)
                    {
                        mostrarMsj("Debe ingresar un Email");
                        return false;
                    }
                    if (this.txtContraseña.Text.Trim() == string.Empty)
                    {
                        mostrarMsj("Debe ingresar una Contraseña");
                        return false;
                    }
                    if (this.txtIdentificacion.Text.Trim() == string.Empty)
                    {
                        mostrarMsj("Debe ingresar la Identificación del estudiante");
                        return false;
                    }
                    if (this.txtNombres.Text.Trim() == string.Empty)
                    {
                        mostrarMsj("Debe ingresar los Nombres del estudiante");
                        return false;
                    }
                    if (this.txtApellidos.Text.Trim() == string.Empty)
                    {
                        mostrarMsj("Debe ingresar los Apellidos del estudiante");
                        return false;
                    }
                    if (this.txtFecNac.Text.Trim() == string.Empty)
                    {
                        mostrarMsj("Debe ingresar una Fecha de nacimiento");
                        return false;
                    }
                    if (this.txtTelefono.Text.Trim() == string.Empty)
                    {
                        mostrarMsj("Debe ingresar un número de Telefono");
                        return false;
                    }
                    if (this.txtIdPadre.Text.Trim() == string.Empty)
                    {
                        mostrarMsj("Debe ingresar la identifiación del padre");
                        return false;
                    }
                    break;
                case "consultarestudiante":
                    if (this.txtIdentificacion.Text.Trim() == string.Empty)
                    {
                        mostrarMsj("Debe ingresar la identificación para buscar el estudiante");
                        return false;
                    }
                    break;
                case "modificar":
                    goto case "registrar";

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

                clsUsuariosOPE objMatri = new clsUsuariosOPE(strNombreAplica);

                objMatri.NombreUsuario = this.txtUsuario.Text.Trim();
                objMatri.Email = this.txtEmail.Text.Trim(); ;
                objMatri.Contrasena = this.txtContraseña.Text.Trim(); ;
                objMatri.Identificacion = this.txtIdentificacion.Text.Trim(); 
                objMatri.Nombre = this.txtNombres.Text.Trim(); ;
                objMatri.Apellido = this.txtApellidos.Text.Trim(); ;
                objMatri.FechaNacimiento = this.txtFecNac.Text.Trim(); ;
                objMatri.Telefono = this.txtTelefono.Text.Trim(); ;
                objMatri.IdentPadre = this.txtIdPadre.Text.Trim(); 
                objMatri.GvGen = this.gvMatricula;

                if (!objMatri.registrarEstudiante())
                {
                    mostrarMsj(objMatri.Error);
                    objMatri = null;
                    return;
                }
                mostrarMsj(objMatri.Mensaje);
                objMatri = null;
                limpiarControles();
                consultarEncargado();
            }
            catch (Exception ex)
            {
                mostrarMsj(ex.Message);
            }
        }

        private void modificar()
        {
            if (!validar("modificar"))
            {
                return;
            }

            clsUsuariosOPE objMatri = new clsUsuariosOPE(strNombreAplica, "frmGestHijo");

            objMatri.NombreUsuario = this.txtUsuario.Text.Trim();
            objMatri.Email = this.txtEmail.Text.Trim(); ;
            objMatri.Contrasena = this.txtContraseña.Text.Trim(); ;
            objMatri.Identificacion = this.txtIdentificacion.Text.Trim();
            objMatri.Nombre = this.txtNombres.Text.Trim(); ;
            objMatri.Apellido = this.txtApellidos.Text.Trim(); ;
            objMatri.FechaNacimiento = this.txtFecNac.Text.Trim(); ;
            objMatri.Telefono = this.txtTelefono.Text.Trim(); ;
            objMatri.IdentPadre = this.txtIdPadre.Text.Trim();
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

        private void eliminar()
        {
            try
            {
                clsUsuariosOPE objMatri = new clsUsuariosOPE(strNombreAplica, "frmGestHijo");

                objMatri.Identificacion = this.txtIdentificacion.Text.Trim();
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

        private bool consultarEstudiante()
        {
            try
            {
                if (!validar("consultarEstudiante"))
                {
                    return false;
                }

                clsUsuariosOPE objMatri = new clsUsuariosOPE(strNombreAplica, "frmGestHijo");

                objMatri.Identificacion = this.txtIdentificacion.Text.Trim();

                if (!objMatri.consultarUsuarioOpe())
                {
                    mostrarMsj(objMatri.Error);
                    objMatri = null;
                    return false;
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
                return true;
            }
            catch (Exception ex)
            {
                mostrarMsj(ex.Message);
                return false;
            }
        }

        #endregion

        protected void cbxAccion_SelectedIndexChanged(object sender, EventArgs e)
        {
            inhabilitarControles("cbxAccion_SelectedIndexChanged");
            int op = cbxAccion.SelectedIndex;
            btnConsultar1.Enabled = true;
            btnConsultar2.Enabled = true;
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
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
                    limpiarControles();
                    consultarEncargado();
                    break;
                case 2:
                    txtIdentificacion.Enabled = true;
                    pnlModificar.Visible = true;
                    limpiarControles();
                    break;
                case 3:
                    txtIdentificacion.Enabled = true;
                    pnlEliminar.Visible = true;
                    limpiarControles();
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

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            modificar();
        }

        protected void btnConsultar2_Click(object sender, EventArgs e)
        {
            if (consultarEstudiante())
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