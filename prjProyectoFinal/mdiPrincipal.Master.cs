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
    public partial class Site1 : System.Web.UI.MasterPage
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
                        pnlGeneral.Visible = false;
                        llenarMenu(objConsEst.Rol);
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
                if (mnuPpal.Items.Count <= 6)
                {
                    if (!objConsEst.consConex())
                    {
                        lblUserConex.Text = objConsEst.Error;
                        objConsEst = null;
                        return false;
                    }
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {

                lblUserConex.Text = ex.Message;
                return false;
            }
        }

        private void llenarMenu(string rol)
        {
            MenuItem mnuInicio = new MenuItem("Inicio", "opcInicio", "", "~/frmPrincipal.aspx");
            mnuPpal.Items.AddAt(0, mnuInicio);
            MenuItem mnuPadre = new MenuItem("Padres", "opcPadres");
            MenuItem AproRecMatri = new MenuItem("Aprobar/Rechazar-Matricula-(EN CONSTRUCCIÓN)", "opcAproRechMtr");
            MenuItem GestionarHijos = new MenuItem("Gestionar Hijos", "opcGestHijos", "", "~/frmGestHijo.aspx");
            MenuItem mnuProfesor = new MenuItem("Profesores", "opcProfesor");
            MenuItem ConsDep = new MenuItem("Consultar mis deportes-(EN CONSTRUCCIÓN)", "opcConsDep");
            MenuItem ConsGrup = new MenuItem("Consultar mis grupos-(EN CONSTRUCCIÓN)", "opcConsGrup");
            MenuItem mnuEstudiante = new MenuItem("Estudiantes", "opcEstudiante");
            MenuItem MatrDep = new MenuItem("Matricular Deportes", "opcMatrDep", "", "~/frmMatricularDep.aspx");
            MenuItem ConsHor = new MenuItem("Consultar mis horarios-(EN CONSTRUCCIÓN)", "opcConsHor");
            MenuItem mnuDirector = new MenuItem("Directores", "opcDirectores");
            MenuItem Gestionar = new MenuItem("Gestionar", "opcGestionar");
            MenuItem Usuarios = new MenuItem("Usuarios", "opcGestionar", "", "~/frmGestUsuario.aspx");
            MenuItem Grupos = new MenuItem("Grupos", "opcGestionar", "", "~/frmGestGrupo.aspx");
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
                    mnuPpal.Items.AddAt(1, mnuPadre);
                    break;
                case "Profesor":   
                    mnuPpal.Items.AddAt(1, mnuProfesor);
                    break;
                case "Estudiante":          
                    mnuPpal.Items.AddAt(1, mnuEstudiante);                    
                    break;
                case "Director":
                    mnuPpal.Items.AddAt(1, mnuDirector);
                    mnuPpal.Items.AddAt(2, mnuPadre);
                    mnuPpal.Items.AddAt(3, mnuProfesor);
                    mnuPpal.Items.AddAt(4, mnuEstudiante);
                    mnuPpal.Items.AddAt(5, mnuInformes); 
                    break;
                default:
                    break;
            } 
        }
        #endregion

        protected void btnIniciar_Click(object sender, EventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex)
            {

                lblUserConex.Text = ex.Message;
            }
        }
    }
}