﻿using libEscuelaOpe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace prjProyectoFinal
{
    public partial class Site1 : System.Web.UI.MasterPage
<<<<<<< HEAD
    {   
        
        protected void Page_Load(object sender, EventArgs e)
        {
           

=======
    {
        #region "Variables Globales"
        private static string strNombreAplica;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                strNombreAplica = Assembly.GetExecutingAssembly().GetName().Name + ".xml";
                llenarDropDown(this.ddlRol);
                clsUsuariosOPE objConsEst = new clsUsuariosOPE(strNombreAplica);
                if (consultarConexion(objConsEst))
                {
                    if (!objConsEst.Identificacion.Equals(string.Empty))
                    {
                        lblUserConex.Text = "Conectado: " + objConsEst.Nombre;
                        mnuPpal.Items.RemoveAt(0);
                        pnlLogin.Visible = false;
                        if (Request.RawUrl.ToString().Equals("/frmLogin.aspx"))
                        {
                            pnlInicio.Visible = true;
                        } 
                        llenarMenu(objConsEst.Rol);
                        mnuPpal.Orientation = Orientation.Horizontal;
                    }
                }
            }
        }

        #region "Métodos Privados"
        private void mostrarMsj(string mensaje)
        {
            this.lblUsuario.Text = mensaje;
            if (mensaje == string.Empty)
            {
                this.lblUsuario.Visible = false;
                return;
            }
            this.lblUsuario.Visible = true;
        }

        private void llenarDropDown(DropDownList ddlDrop)
        {
            try
            {
                clsRolesOPE objOpe = new clsRolesOPE(strNombreAplica);

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

        private bool consultarConexion(clsUsuariosOPE objConsEst)
        {
            try
            {
                    if (!objConsEst.consConex())
                    {
                        lblUserConex.Text = objConsEst.Error;
                        objConsEst = null;
                        return false;
                    }
                    return true;
            }
            catch (Exception ex)
            {

                lblUserConex.Text = ex.Message;
                return false;
            }
        }

        private void llenarMenu(string rol)
        {
            MenuItem mnuPadre = new MenuItem("Padres", "opcPadres");
            MenuItem AproRecMatri = new MenuItem("Aprobar/Rechazar-Matricula-(EN CONSTRUCCIÓN)", "opcAproRechMtr");
            MenuItem GestionarHijos = new MenuItem("Gestionar Hijos", "opcGestHijos");
            MenuItem mnuProfesor = new MenuItem("Profesores", "opcProfesor");
            MenuItem ConsDep = new MenuItem("Consultar mis deportes-(EN CONSTRUCCIÓN)", "opcConsDep");
            MenuItem ConsGrup = new MenuItem("Consultar mis grupos-(EN CONSTRUCCIÓN)", "opcConsGrup");
            MenuItem mnuEstudiante = new MenuItem("Estudiantes", "opcEstudiante");
            MenuItem MatrDep = new MenuItem("Matricular Deportes", "opcMatrDep");
            MenuItem ConsHor = new MenuItem("Consultar mis horarios-(EN CONSTRUCCIÓN)", "opcConsHor");
            MenuItem mnuDirector = new MenuItem("Directores", "opcDirectores");
            MenuItem Gestionar = new MenuItem("Gestionar", "opcGestionar");
            MenuItem Usuarios = new MenuItem("Usuarios", "opcGestionar");
            MenuItem Grupos = new MenuItem("Grupos", "opcGestionar");
            MenuItem mnuInformes = new MenuItem("Informes", "opcInformes");
            mnuPadre.ChildItems.Add(GestionarHijos);
            mnuPadre.ChildItems.Add(AproRecMatri);
            mnuProfesor.ChildItems.Add(ConsDep);
            mnuProfesor.ChildItems.Add(ConsGrup);
            mnuEstudiante.ChildItems.Add(MatrDep);
            mnuEstudiante.ChildItems.Add(ConsHor);
            Gestionar.ChildItems.Add(Usuarios);
            Gestionar.ChildItems.Add(Grupos);
            mnuDirector.ChildItems.Add(Gestionar);
            switch (rol)
            {
                case "Padre":
                    mnuPpal.Items.AddAt(0, mnuPadre);
                    break;
                case "Profesor":   
                    mnuPpal.Items.AddAt(0, mnuProfesor);
                    break;
                case "Estudiante":          
                    mnuPpal.Items.AddAt(0, mnuEstudiante);                    
                    break;
                case "Director":
                    mnuPpal.Items.AddAt(0, mnuDirector);
                    mnuPpal.Items.AddAt(1, mnuPadre);
                    mnuPpal.Items.AddAt(2, mnuProfesor);
                    mnuPpal.Items.AddAt(3, mnuEstudiante);
                    mnuPpal.Items.AddAt(4, mnuInformes); 
                    break;
                default:
                    break;
            } 
        }

        private void realizarConexion()
        {
            try
            {
                clsUsuariosOPE objMatri = new clsUsuariosOPE(strNombreAplica);

                objMatri.NombreUsuario = this.txtUsuario.Text.Trim();
                objMatri.Identificacion = this.txtContraseña.Text.Trim();
                objMatri.Rol = ddlRol.SelectedItem.Text;

                if (!objMatri.realizarConex())
                {
                    mostrarMsj(objMatri.Error);
                    objMatri = null;
                    return;
                }
                objMatri = null;

                Response.Redirect(Request.Url.ToString());
            }
            catch (Exception ex)
            {
                lblUserConex.Text = ex.Message;
            }
        }

        #endregion

        protected void btnIniciar_Click(object sender, EventArgs e)
        {
            realizarConexion();
        }

        protected void mnuPpal_MenuItemClick(object sender, MenuEventArgs e)
        {
            string op = e.Item.Text;

            switch (op)
            {
                case "Gestionar Hijos":
                    Response.Redirect("~/frmGestHijo.aspx"); 
                    break;
                case "Matricular Deportes":
                    Response.Redirect("~/frmMatricularDep.aspx");
                    break;
                case "Usuarios":
                    Response.Redirect("~/frmGestUsuario.aspx");
                    break;
                case "Grupos":
                    Response.Redirect("~/frmGestGrupo.aspx");
                    break;
                default:
                    break;
            }
            
>>>>>>> 19a9ad756a9a13ead81c9792a04f9a874fdc773a
        }
    }
}