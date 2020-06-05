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
    public partial class frmAdminRoles : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                strNombreAplica = Assembly.GetExecutingAssembly().GetName().Name + ".xml";
                llenarDropDown(ddlRol);
            }
        }

        #region "Variables Globales"
        private static string strNombreAplica;
        #endregion

        #region "Metódos Privados"

        private bool validar(string strMetodoOrigen)
        {
            switch (strMetodoOrigen.ToLower())
            {
                case "agregarRol":
                    if (this.ddlRol.SelectedIndex == 0)
                    {
                        mostrarMsj("Debe seleccionar un Rol");
                        return false;
                    }
                    break;
                case "consultar":
                    if (this.txtIdentificacion.Text.Trim() == string.Empty)
                    {
                        mostrarMsj("Debe ingresar la Identificación");
                        return false;
                    }
                    break;
            }
            return true;
        }

        private void inhabilitarControles(string metodoOrigen)
        {
            switch (metodoOrigen)
            {
                case "consultar":
                    txtIdentificacion.Enabled = false;
                    btnConsultar.Enabled = false;
                    ddlRol.Enabled = true;
                    btnRegistrar.Enabled = true;
                    break;
                case "agregarRol":
                    txtIdentificacion.Text = string.Empty;
                    txtIdentificacion.Enabled = true;
                    btnConsultar.Enabled = true;
                    ddlRol.Enabled = false;
                    ddlRol.SelectedIndex = 0;
                    btnRegistrar.Enabled = false;
                    break;
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
        #endregion

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            consultar();
        }

        private bool consultar()
        {
            try
            {
                if (!validar("consultar"))
                {
                    return false;
                }

                clsRolesOPE objMatri = new clsRolesOPE(strNombreAplica);

                objMatri.Documento = this.txtIdentificacion.Text.Trim();
                objMatri.GvGen = this.gvMatricula;

                if (!objMatri.consultarOpe())
                {
                    mostrarMsj(objMatri.Error);
                    objMatri = null;
                    return false;
                }

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

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            agregarRol();
        }

        private void agregarRol()
        {
            try
            {
                if (!validar("agregarRol"))
                {
                    return;
                }

                clsRolesOPE objMatri = new clsRolesOPE(strNombreAplica);

                objMatri.Documento = this.txtIdentificacion.Text.Trim();
                objMatri.IdRol = Convert.ToInt32(this.ddlRol.SelectedValue);
                objMatri.GvGen = this.gvMatricula;

                if (!objMatri.registrarOpe())
                {
                    mostrarMsj(objMatri.Error);
                    objMatri = null;
                    return;
                }
                mostrarMsj(objMatri.Mensaje);
                objMatri = null;

                inhabilitarControles("agregarRol");
            }
            catch (Exception ex)
            {
                mostrarMsj(ex.Message);
            }
        }
    }
}