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
            if (!IsPostBack)
            {
                strNombreAplica = Assembly.GetExecutingAssembly().GetName().Name + ".xml";
                llenarDropDown(ddlRol);
                llenarGiv();
            }
        }

        #region "Metódos Privados"
        private void limpiarControles()
        {
            this.txtUsuario.Text = string.Empty;
            this.txtEmail.Text = string.Empty;
            this.txtContraseña.Text = string.Empty;
            this.txtIdentificacion.Text = string.Empty;
            this.txtNombres.Text = string.Empty;
            this.txtApellidos.Text = string.Empty;
            this.txtFecNac.Text = string.Empty;
            this.txtTelefono.Text = string.Empty;
            this.ddlRol.SelectedIndex = 0;
        }

        private void llenarGiv()
        {
            try
            {
                clsUsuariosOPE objMatri = new clsUsuariosOPE(strNombreAplica);

                objMatri.GvGen = this.gvUsuario;

                if (!objMatri.cargarUsuariosOpe())
                {
                    mostrarMsj(objMatri.Error);
                    objMatri = null;
                    return;
                }
                objMatri = null;
            }
            catch (Exception ex)
            {
                mostrarMsj(ex.Message);
            }
        }

        public void inhabilitarControles(string metodo)
        {
            switch (metodo.ToLower())
            {
                case "cbxaccion_selectedindexchanged":
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
                    break;
                case "consultar":
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
                    btnConsultar2.Enabled = true;
                    btnEliminar.Enabled = false;
                    break;
                default:
                    goto case "cbxaccion_selectedindexchanged";
            }

        }

        private void mostrarMsj(string mensaje)
        {
            this.lblMensajeError.Text = mensaje;
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
        private bool validar(string strMetodoOrigen)
        {
            switch (strMetodoOrigen.ToLower())
            {
                case "registrar":
                    if (this.txtUsuario.Text.Trim() == string.Empty)
                    {
                        mostrarMsj("Debe ingresar un usuario");
                        return false;
                    }
                    if (this.txtEmail.Text.Trim() == string.Empty)
                    {
                        mostrarMsj("Debe ingresar un correo electronico");
                        return false;
                    }
                    if (this.txtContraseña.Text.Trim() == string.Empty)
                    {
                        mostrarMsj("Debe ingresar una contraseña para su usuario");
                        return false;
                    }
                    if (this.txtIdentificacion.Text.Trim() == string.Empty)
                    {
                        mostrarMsj("Debe ingresar el número de indentificación");
                        return false;
                    }
                    if (this.txtNombres.Text.Trim() == string.Empty)
                    {
                        mostrarMsj("Debe ingresar el(los) nombre(s) del usuario");
                        return false;
                    }
                    if (this.txtApellidos.Text.Trim() == string.Empty)
                    {
                        mostrarMsj("Debe ingresar el(los) apellido(s) del usuario");
                        return false;
                    }
                    if (this.txtFecNac.Text.Trim() == string.Empty)
                    {
                        mostrarMsj("Debe elegir la fecha de nacimiento");
                        return false;
                    }
                    if (this.txtTelefono.Text.Trim() == string.Empty)
                    {
                        mostrarMsj("Debe ingresar un número telefonico");
                        return false;
                    }
                    if (this.ddlRol.SelectedValue.Equals("0"))
                    {
                        mostrarMsj("Debe elegir un Rol");
                        return false;
                    }
                    break;
                case "modificar":
                    if (this.txtUsuario.Text.Trim() == string.Empty)
                    {
                        mostrarMsj("Debe ingresar un usuario para modificarlo");
                        return false;
                    }
                    if (this.txtEmail.Text.Trim() == string.Empty)
                    {
                        mostrarMsj("Debe ingresar un correo electronico para modificarlo");
                        return false;
                    }
                    if (this.txtNombres.Text.Trim() == string.Empty)
                    {
                        mostrarMsj("Debe ingresar el(los) nombre(s) del usuario para modificarlo");
                        return false;
                    }
                    if (this.txtApellidos.Text.Trim() == string.Empty)
                    {
                        mostrarMsj("Debe ingresar el(los) apellido(s) del usuario para modificarlo");
                        return false;
                    }
                    if (this.txtFecNac.Text.Trim() == string.Empty)
                    {
                        mostrarMsj("Debe elegir la fecha de nacimiento para modificarlo");
                        return false;
                    }
                    if (this.txtTelefono.Text.Trim() == string.Empty)
                    {
                        mostrarMsj("Debe ingresar un número telefonico para modificarlo");
                        return false;
                    }
                    break;
                case "consultar":
                    if (this.txtIdentificacion.Text.Trim() == string.Empty)
                    {
                        mostrarMsj("Debe ingresar el número de indentificación para consultar al usuario");
                        return false;
                    }
                    break;

            }
            return true;
        }

        private void consultar()
        {
            try
            {
                if (!validar("consultar"))
                {
                    return;
                }

                clsUsuariosOPE objUsuario = new clsUsuariosOPE(strNombreAplica, "frmGestUsuario");

                objUsuario.Identificacion = this.txtIdentificacion.Text.Trim();

                if (!objUsuario.consultarUsuarioOpe())
                {
                    mostrarMsj(objUsuario.Error);
                    objUsuario = null;
                    return;
                }
                txtUsuario.Text = objUsuario.NombreUsuario;
                txtEmail.Text = objUsuario.Email;
                txtContraseña.Text = objUsuario.Contrasena;
                txtIdentificacion.Text = objUsuario.Identificacion;
                txtNombres.Text = objUsuario.Nombre;
                txtApellidos.Text = objUsuario.Apellido;
                txtFecNac.Text = objUsuario.FechaNacimiento;
                txtTelefono.Text = objUsuario.Telefono;
                //ddlRol.Text = objUsuario.Rol.ToString();
                mostrarMsj(objUsuario.Mensaje);
                inhabilitarControles("consultar");
                objUsuario = null;

                return;
            }
            catch (Exception ex)
            {
                throw ex;
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

                clsUsuariosOPE objUsuario = new clsUsuariosOPE(strNombreAplica);

                objUsuario.NombreUsuario = this.txtUsuario.Text.Trim();
                objUsuario.Email = this.txtEmail.Text.Trim();
                objUsuario.Contrasena = this.txtContraseña.Text.Trim();
                objUsuario.Identificacion = this.txtIdentificacion.Text.Trim();
                objUsuario.Nombre = this.txtNombres.Text.Trim();
                objUsuario.Apellido = this.txtApellidos.Text.Trim();
                objUsuario.FechaNacimiento = this.txtFecNac.Text.Trim();
                objUsuario.Telefono = this.txtTelefono.Text.Trim();
                objUsuario.IdRol = Convert.ToInt32(this.ddlRol.SelectedValue);
                objUsuario.GvGen = this.gvUsuario;

                if (!objUsuario.registrarUsuarioOpe())
                {
                    mostrarMsj(objUsuario.Error);
                    objUsuario = null;
                    return;
                }
                mostrarMsj(objUsuario.Mensaje);
                objUsuario = null;
                limpiarControles();
                return;
            }
            catch (Exception ex)
            {

                throw ex;
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

                clsUsuariosOPE objUsuario = new clsUsuariosOPE(strNombreAplica, "frmGestUsuario");
                objUsuario.NombreUsuario = this.txtUsuario.Text.Trim();
                objUsuario.Email = this.txtEmail.Text.Trim();
                objUsuario.Identificacion = this.txtIdentificacion.Text.Trim();
                objUsuario.Contrasena = this.txtContraseña.Text.Trim();
                objUsuario.Nombre = this.txtNombres.Text.Trim();
                objUsuario.Apellido = this.txtApellidos.Text.Trim();
                objUsuario.FechaNacimiento = this.txtFecNac.Text.Trim();
                objUsuario.Telefono = this.txtTelefono.Text.Trim();
                objUsuario.GvGen = this.gvUsuario;

                if (!objUsuario.modificarUsuarioOpe())
                {
                    mostrarMsj(objUsuario.Error);
                    objUsuario = null;
                    return;
                }
                mostrarMsj(objUsuario.Mensaje);
                objUsuario = null;
                limpiarControles();
                return;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void eliminar()
        {
            try
            {
                if (!validar("eliminar"))
                {
                    return;
                }

                clsUsuariosOPE objUsuario = new clsUsuariosOPE(strNombreAplica, "frmGestUsuario");
                objUsuario.Identificacion = this.txtIdentificacion.Text.Trim();
                objUsuario.GvGen = this.gvUsuario;

                if (!objUsuario.eliminarOpe())
                {
                    mostrarMsj(objUsuario.Error);
                    objUsuario = null;
                    return;
                }
                mostrarMsj(objUsuario.Mensaje);
                limpiarControles();
                objUsuario = null;
                return;

            }
            catch (Exception ex)
            {
                throw ex;
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
                    ddlRol.Enabled = true;
                    pnlRegistrar.Visible = true;
                    limpiarControles();
                    break;
                case 2:
                    txtIdentificacion.Enabled = true;
                    ddlRol.Enabled = true;
                    pnlModificar.Visible = true;
                    limpiarControles();
                    break;
                case 3:
                    txtIdentificacion.Enabled = true;
                    ddlRol.Enabled = true;
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

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            modificar();
        }


        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            eliminar();
        }
        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            consultar();
        }
    }
}