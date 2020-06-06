using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using libLlenarGrids;
using libEscuelaRN;
using System.Web.UI.WebControls;

namespace libEscuelaOpe
{
    public class clsUsuariosOPE
    {
        #region "Atributos"
        private string strNombreUsuario;
        private string strContrasena;
        private string strIdentificacion;
        private int intIdRol;
        private string strRol;
        private string strNombre;
        private string strEmail;
        private string strApellido;
        private string strFechaNacimiento;
        private string strTelefono;
        private string formulario;
        private string strNombreApp;
        private string strIdentPadre;
        private string strError;
        private DataSet dsDatos;
        private string strMensaje;
        DropDownList ddlGen;
        GridView gvGen;
        #endregion

        #region "Constructor"
        public clsUsuariosOPE(string nombreApp)
        {
            this.strNombreApp = nombreApp;
            strContrasena = string.Empty;
            strIdentificacion = string.Empty; 
            strRol = string.Empty; 
            strNombre = string.Empty; 
            strEmail = string.Empty; 
            strApellido = string.Empty; 
            strFechaNacimiento = string.Empty; 
            strTelefono = string.Empty; 
            formulario = string.Empty; 
            strIdentPadre = string.Empty; 
            strError = string.Empty; 
            strMensaje = string.Empty; 
        }
        public clsUsuariosOPE(string nombreApp, string formulario)
        {
            this.strNombreApp = nombreApp;
            this.formulario = formulario;
            strContrasena = string.Empty;
            strIdentificacion = string.Empty; 
            strRol = string.Empty; ;
            strNombre = string.Empty; 
            strEmail = string.Empty; 
            strApellido = string.Empty; 
            strFechaNacimiento = string.Empty; 
            strTelefono = string.Empty; 
            strIdentPadre = string.Empty; 
            strError = string.Empty; 
            strMensaje = string.Empty; 
        }
        #endregion

        #region "Propiedades"
        public string NombreUsuario { get => strNombreUsuario; set => strNombreUsuario = value; }
        public string Rol { get => strRol; set => strRol = value; }
        public int IdRol { get => intIdRol; set => intIdRol = value; }
        public string Identificacion { get => strIdentificacion; set => strIdentificacion = value; }
        public string Error { get => strError; }
        public string Contrasena { get => strContrasena; set => strContrasena = value; }
        public string Nombre { get => strNombre; set => strNombre = value; }
        public string Email { get => strEmail; set => strEmail = value; }
        public string Apellido { get => strApellido; set => strApellido = value; }
        public string FechaNacimiento { get => strFechaNacimiento; set => strFechaNacimiento = value; }
        public string Telefono { get => strTelefono; set => strTelefono = value; }
        public string IdentPadre { get => strIdentPadre; set => strIdentPadre = value; }
        public DataSet DatosRpta { get => dsDatos; }
        public string Mensaje { get => strMensaje; }
        public DropDownList DdlGen { set => ddlGen = value; }
        public GridView GvGen { set => gvGen = value; }
        #endregion

        #region "Métodos Privados"      
        
        private bool validar(string metodoOrigen)
        {
            if (strNombreApp == string.Empty)
            {
                strError = "Olvido enviar el nombre de la aplicación";
                return false;
            }
            switch (metodoOrigen.ToLower())
            {
                case "loginusuario":
                    if (strNombreUsuario == string.Empty)
                    {
                        strError = "El nombre de usuario no puede estar vacio";
                        return false;
                    }
                    if (strContrasena == string.Empty)
                    {
                        strError = "La contraseña no puede estar vacia";
                        return false;
                    }
                    if (strRol == string.Empty)
                    {
                        strError = "El rol no puede estar vacio";
                        return false;
                    }
                    break;
                case "registrarEstudiante":
                    if (strNombreUsuario == string.Empty)
                    {
                        strError = "El nombre de usuario no puede estar vacio";
                        return false;
                    }
                    if (strEmail == string.Empty)
                    {
                        strError = "El email no puede estar vacio";
                        return false;
                    }
                    if (strEmail == string.Empty)
                    {
                        strError = "El nombre de usuario no puede estar vacio";
                        return false;
                    }
                    if (strContrasena == string.Empty)
                    {
                        strError = "La contraseña no puede estar vacia";
                        return false;
                    }
                    if (strIdentificacion == string.Empty)
                    {
                        strError = "La identificación no puede estar vacia";
                        return false;
                    }
                    if (strNombre == string.Empty)
                    {
                        strError = "El nombre no puede estar vacio";
                        return false;
                    }
                    if (strApellido == string.Empty)
                    {
                        strError = "El apellido no puede estar vacio";
                        return false;
                    }
                    if (strFechaNacimiento == string.Empty)
                    {
                        strError = "La fecha de nacimiento no puede estar vacia";
                        return false;
                    }
                    if (strTelefono == string.Empty)
                    {
                        strError = "El telefono no puede estar vacio";
                        return false;
                    }
                    if ( strIdentPadre == string.Empty)
                    {
                        strError = "La identificación del padre no puede estar vacio";
                        return false;
                    }
                    break;
                case "modificarOpe":
                    if (strNombreUsuario == string.Empty)
                    {
                        strError = "El nombre de usuario no puede estar vacio";
                        return false;
                    }
                    if (strEmail == string.Empty)
                    {
                        strError = "El email no puede estar vacio";
                        return false;
                    }
                    if (strEmail == string.Empty)
                    {
                        strError = "El nombre de usuario no puede estar vacio";
                        return false;
                    }
                    if (strContrasena == string.Empty)
                    {
                        strError = "La contraseña no puede estar vacia";
                        return false;
                    }
                    if (strNombre == string.Empty)
                    {
                        strError = "El nombre no puede estar vacio";
                        return false;
                    }
                    if (strApellido == string.Empty)
                    {
                        strError = "El apellido no puede estar vacio";
                        return false;
                    }
                    if (strFechaNacimiento == string.Empty)
                    {
                        strError = "La fecha de nacimiento no puede estar vacia";
                        return false;
                    }
                    if (strTelefono == string.Empty)
                    {
                        strError = "El telefono no puede estar vacio";
                        return false;
                    }
                    if (strIdentPadre == string.Empty)
                    {
                        strError = "La identificación del padre no puede estar vacio";
                        return false;
                    }
                    if (formulario == string.Empty)
                    {
                        strError = "Olvidó enviar el nombre del formulario";
                        return false;
                    }
                    break;
                case "eliminarOpe":
                    if (strIdentificacion == string.Empty)
                    {
                        strError = "La identificación no puede estar vacia";
                        return false;
                    }
                    if (formulario == string.Empty)
                    {
                        strError = "Olvidó enviar el nombre del formulario";
                        return false;
                    }
                    break;
                case "consultarUsuarioOpe":
                    goto case "eliminarOpe";
            }
            return true;
        }

        private bool llenarGrid(DataTable dtDatos)
        {
            try
            {
                if (!validar("llenarGrid"))
                {
                    return false;
                }
                clsLlenarGrids objGrids = new clsLlenarGrids();

                if (!objGrids.llenarGridWeb(gvGen, dtDatos))
                {
                    strError = objGrids.Error;
                    objGrids = null;
                    return false;
                }
                objGrids = null;
                return true;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        #endregion

        #region "Métodos públicos"

        public bool consConex()
        {
            try
            {
                if (!validar("consConex"))
                {
                    return false;
                }
                clsUsuariosRN objConsRn = new clsUsuariosRN(strNombreApp);

                if (!objConsRn.consConex())
                {
                    objConsRn = null;
                    return false;
                }
                if (objConsRn.DatosRptaBd.Tables[0].Rows.Count == 0)
                {
                    strError = "Desconectado";
                    return false;
                }
                strNombre = objConsRn.DatosRptaBd.Tables[0].Rows[0]["nombre"].ToString();
                strContrasena = objConsRn.DatosRptaBd.Tables[0].Rows[0]["contrasena"].ToString();
                strRol = objConsRn.DatosRptaBd.Tables[0].Rows[0]["rol"].ToString();
                strIdentificacion = objConsRn.DatosRptaBd.Tables[0].Rows[0]["identificacion"].ToString();
                objConsRn = null;

                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }

        public bool realizarConex()
        {
            try
            {
                if (!validar("loginusuario"))
                {
                    return false;
                }
                clsUsuariosRN objRn = new clsUsuariosRN(this.strNombreApp);
                

                objRn.NombreUsuario = strNombreUsuario;
                objRn.Contrasena = strContrasena;
                objRn.Rol = strRol;               

                if (!objRn.realizarConex())
                {
                    strError = objRn.Error;
                    objRn = null;
                    return false;
                }

                if (objRn.DatosRptaBd.Tables[0].Rows[0]["Respuesta"].ToString() == "1")
                {
                    strError = objRn.DatosRptaBd.Tables[0].Rows[0]["Mensaje"].ToString();
                    objRn = null;
                    return false;
                }

                strMensaje = objRn.DatosRptaBd.Tables[0].Rows[0]["Mensaje"].ToString();

                objRn = null;
                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }

        public bool registrarEstudiante()
        {
            try
            {
                if (!validar("registrarEstudiante"))
                {
                    return false;
                }
                clsUsuariosRN objRn = new clsUsuariosRN(this.strNombreApp);

                objRn.NombreUsuario = strNombreUsuario;
                objRn.Email = strEmail;
                objRn.Contrasena = strContrasena;
                objRn.Identificacion = strIdentificacion;
                objRn.Nombre = strNombre;
                objRn.Apellido = strApellido;
                objRn.FechaNacimiento = strFechaNacimiento;
                objRn.Telefono = strTelefono;
                objRn.IdentPadre = strIdentPadre;


                if (!objRn.registrarEstudiante())
                {
                    strError = objRn.Error;
                    objRn = null;
                    return false;
                }
                if (objRn.DatosRptaBd.Tables[0].Rows[0]["CodRpta"].ToString() == "1")
                {
                    strError = objRn.DatosRptaBd.Tables[0].Rows[0]["Mensaje"].ToString();
                    objRn = null;
                    return false;
                }
                if (objRn.DatosRptaBd.Tables.Count > 1)
                {
                    if (!llenarGrid(objRn.DatosRptaBd.Tables[1]))
                    {
                        objRn = null;
                        return false;
                    }
                }
                strMensaje = objRn.DatosRptaBd.Tables[0].Rows[0]["Mensaje"].ToString();
                objRn = null;
                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }

        public bool modificarOpe()
        {
            try
            {
                if (!validar("modificarOpe"))
                {
                    return false;
                }
                clsUsuariosRN objRn = new clsUsuariosRN(this.strNombreApp , formulario);

                objRn.Identificacion = strIdentificacion;
                objRn.NombreUsuario = strNombreUsuario;
                objRn.Email = strEmail;
                objRn.Contrasena = strContrasena;
                objRn.Nombre = strNombre;
                objRn.Apellido = strApellido;
                objRn.FechaNacimiento = strFechaNacimiento;
                objRn.Telefono = strTelefono;
                objRn.IdentPadre = strIdentPadre;

                if (!objRn.modificarUsuario())
                {
                    strError = objRn.Error;
                    objRn = null;
                    return false;
                }
                if (objRn.DatosRptaBd.Tables[0].Rows[0]["CodRpta"].ToString() == "1")
                {
                    strError = objRn.DatosRptaBd.Tables[0].Rows[0]["Mensaje"].ToString();
                    objRn = null;
                    return false;
                }
                if (objRn.DatosRptaBd.Tables.Count > 1)
                {
                    if (!llenarGrid(objRn.DatosRptaBd.Tables[1]))
                    {
                        objRn = null;
                        return false;
                    }
                }
                strMensaje = objRn.DatosRptaBd.Tables[0].Rows[0]["Mensaje"].ToString();
                objRn = null;
                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }

        public bool eliminarOpe()
        {
            try
            {
                if (!validar("eliminarOpe"))
                {
                    return false;
                }
                clsUsuariosRN objRn = new clsUsuariosRN(this.strNombreApp, formulario);

                objRn.Identificacion = strIdentificacion;

                if (!objRn.eliminarUsuario())
                {
                    strError = objRn.Error;
                    objRn = null;
                    return false;
                }
                if (objRn.DatosRptaBd.Tables[0].Rows[0]["CodRpta"].ToString() == "1")
                {
                    strError = objRn.DatosRptaBd.Tables[0].Rows[0]["Mensaje"].ToString();
                    objRn = null;
                    return false;
                }
                if (objRn.DatosRptaBd.Tables.Count > 1)
                {
                    if (!llenarGrid(objRn.DatosRptaBd.Tables[1]))
                    {
                        objRn = null;
                        return false;
                    }
                }
                strMensaje = objRn.DatosRptaBd.Tables[0].Rows[0]["Mensaje"].ToString();
                objRn = null;
                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }

        public bool cargarEstudiantesOpe()
        {
            try
            {
                if (!validar("cargarEstudiantesOpe"))
                {
                    return false;
                }
                clsUsuariosRN objRn = new clsUsuariosRN(this.strNombreApp);

                if (!objRn.cargarEstudiantes())
                {
                    strError = objRn.Error;
                    objRn = null;
                    return false;
                }

                if (!llenarGrid(objRn.DatosRptaBd.Tables[0]))
                {
                    objRn = null;
                    return false;
                }
                objRn = null;
                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }

        public bool cargarUsuariosOpe()
        {
            try
            {
                if (!validar("cargarUsuariosOpe"))
                {
                    return false;
                }
                clsUsuariosRN objRn = new clsUsuariosRN(this.strNombreApp);

                if (!objRn.cargarUsuarios())
                {
                    strError = objRn.Error;
                    objRn = null;
                    return false;
                }

                if (!llenarGrid(objRn.DatosRptaBd.Tables[0]))
                {
                    objRn = null;
                    return false;
                }
                objRn = null;
                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }

        public bool consultarUsuarioOpe()
        {
            try
            {
                if (!validar("consultarUsuarioOpe"))
                {
                    return false;
                }
                clsUsuariosRN objRn = new clsUsuariosRN(this.strNombreApp , this.formulario);

                objRn.Identificacion = strIdentificacion;

                if (!objRn.consultarUsuario())
                {
                    strError = objRn.Error;
                    objRn = null;
                    return false;
                }

                if (objRn.DatosRptaBd.Tables[0].Rows[0]["CodRpta"].ToString() == "1")
                {
                    strError = objRn.DatosRptaBd.Tables[0].Rows[0]["Mensaje"].ToString();
                    objRn = null;
                    return false;
                }

                strNombreUsuario = objRn.DatosRptaBd.Tables[1].Rows[0]["nombre_usuario"].ToString();
                strEmail = objRn.DatosRptaBd.Tables[1].Rows[0]["email"].ToString();
                strContrasena = objRn.DatosRptaBd.Tables[1].Rows[0]["contrasena"].ToString();
                strIdentificacion = objRn.DatosRptaBd.Tables[1].Rows[0]["identificacion"].ToString();
                strNombre = objRn.DatosRptaBd.Tables[1].Rows[0]["nombre"].ToString();
                strApellido = objRn.DatosRptaBd.Tables[1].Rows[0]["apellido"].ToString();
                strFechaNacimiento = objRn.DatosRptaBd.Tables[1].Rows[0]["fecha_nacimiento"].ToString();
                strTelefono = objRn.DatosRptaBd.Tables[1].Rows[0]["telefono"].ToString();
                if (objRn.DatosRptaBd.Tables[1].Columns.Contains("documento_padre"))
                {
                    strIdentPadre = objRn.DatosRptaBd.Tables[1].Rows[0]["documento_padre"].ToString();
                }
                if (objRn.DatosRptaBd.Tables[1].Columns.Contains("id_rol"))
                {
                    strRol = objRn.DatosRptaBd.Tables[1].Rows[0]["id_rol"].ToString();
                }
                strMensaje = objRn.DatosRptaBd.Tables[0].Rows[0]["Mensaje"].ToString();
                objRn = null;
                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }

        public bool cerrarSesion()
        {
            try
            {
                if (!validar("cerrarSesion"))
                {
                    return false;
                }
                clsUsuariosRN objConsRn = new clsUsuariosRN(strNombreApp);

                if (!objConsRn.cerrarSesion())
                {
                    objConsRn = null;
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }

        public bool registrarUsuarioOpe()
        {
            try
            {
                if (!validar("registrarUsuarioOpe"))
                {
                    return false;
                }
                clsUsuariosRN objUsuarioRN = new clsUsuariosRN(this.strNombreApp);
                objUsuarioRN.NombreUsuario = strNombreUsuario;
                objUsuarioRN.Email = strEmail;
                objUsuarioRN.Contrasena = strContrasena;
                objUsuarioRN.Identificacion = strIdentificacion;
                objUsuarioRN.Nombre = strNombre;
                objUsuarioRN.Apellido = strApellido;
                objUsuarioRN.FechaNacimiento = strFechaNacimiento;
                objUsuarioRN.Telefono = strTelefono;
                objUsuarioRN.IdRol = intIdRol;

                if (!objUsuarioRN.registrarUsuarioOpeRN())
                {
                    strError = objUsuarioRN.Error;
                    objUsuarioRN = null;
                    return false;
                }
                if (objUsuarioRN.DatosRptaBd.Tables[0].Rows[0]["CodRpta"].ToString() == "1")
                {
                    strError = objUsuarioRN.DatosRptaBd.Tables[0].Rows[0]["Mensaje"].ToString();
                    objUsuarioRN = null;
                    return false;
                }
                if (objUsuarioRN.DatosRptaBd.Tables.Count > 1)
                {
                    if (!llenarGrid(objUsuarioRN.DatosRptaBd.Tables[1]))
                    {
                        objUsuarioRN = null;
                        return false;
                    }
                }
                strMensaje = objUsuarioRN.DatosRptaBd.Tables[0].Rows[0]["Mensaje"].ToString();
                objUsuarioRN = null;

                return true;
            }
            catch (Exception ex)
            {
                strMensaje = ex.Message;
                return false;
            }
        }

        public bool modificarUsuarioOpe()
        {
            try
            {
                if (!validar("modificarusuarioope"))
                {
                    return false;
                }
                clsUsuariosRN objUsuarioRN = new clsUsuariosRN(this.strNombreApp, formulario);


                objUsuarioRN.NombreUsuario = strNombreUsuario;
                objUsuarioRN.Email = strEmail;
                objUsuarioRN.Identificacion = strIdentificacion;
                objUsuarioRN.Contrasena = strContrasena;
                objUsuarioRN.Nombre = strNombre;
                objUsuarioRN.Apellido = strApellido;
                objUsuarioRN.FechaNacimiento = strFechaNacimiento;
                objUsuarioRN.Telefono = strTelefono;

                if (!objUsuarioRN.modificarUsuarioOpeRN())
                {
                    strError = objUsuarioRN.Error;
                    objUsuarioRN = null;
                    return false;
                }
                if (objUsuarioRN.DatosRptaBd.Tables[0].Rows[0]["CodRpta"].ToString() == "1")
                {
                    strError = objUsuarioRN.DatosRptaBd.Tables[0].Rows[0]["Mensaje"].ToString();
                    objUsuarioRN = null;
                    return false;
                }
                if (objUsuarioRN.DatosRptaBd.Tables.Count > 1)
                {
                    if (!llenarGrid(objUsuarioRN.DatosRptaBd.Tables[1]))
                    {
                        objUsuarioRN = null;
                        return false;
                    }
                }
                strMensaje = objUsuarioRN.DatosRptaBd.Tables[0].Rows[0]["Mensaje"].ToString();
                objUsuarioRN = null;
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion
    }
}
