using System;
using System.Data;
using System.Data.SqlClient;
using libConexionBd;
using libLlenarGrids;
using libLlenarCombos;
using System.Web.UI.WebControls;

namespace libEscuelaRN
{
    public class clsUsuariosRN
    {

        #region "Atributos"
        private string strNombreUsuario;
        private string strContrasena;
        private string strIdentificacion;
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
        private SqlParameter[] objDatosUsuario;
        private clsConexionBd objConex;
        private clsLlenarGrids objLlenaGrids;
        private DataSet dsDatos;
        #endregion

        #region "Constructor"
        public clsUsuariosRN(string nombreApp)
        {
            this.strNombreApp = nombreApp;
            strError = string.Empty;
            strContrasena = string.Empty;
            strIdentificacion = string.Empty;
            strRol = string.Empty;
            strNombre = string.Empty;
            strEmail = string.Empty;
            strApellido = string.Empty;
            strFechaNacimiento = string.Empty;
            strTelefono = string.Empty;
            formulario = string.Empty;
        }
        public clsUsuariosRN(string nombreApp, string formulario)
        {
            this.strNombreApp = nombreApp;
            strError = string.Empty;
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
        }
        #endregion

        #region "Propiedades"        
        public string NombreUsuario { set => strNombreUsuario = value; }
        public string Identificacion { set => strIdentificacion = value; }
        public string Rol { set => strRol = value; }
        public string Error { get => strError; }
        public DataSet DatosRptaBd { get => dsDatos; }
        public string Contrasena { set => strContrasena = value; }
        public string Nombre { set => strNombre = value; }
        public string Email { set => strEmail = value; }
        public string Apellido { set => strApellido = value; }
        public string FechaNacimiento { set => strFechaNacimiento = value; }
        public string Telefono { set => strTelefono = value; }
        public string IdentPadre { set => strIdentPadre = value; }
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
                case "registrarestudiante":
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
                    if (strIdentPadre == string.Empty)
                    {
                        strError = "La identificación del padre no puede estar vacio";
                        return false;
                    }
                    break;
                case "modificarusuario":
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
                case "eliminarusuario":
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
                case "consultarusuario":
                    goto case "eliminarusuario";
            }
            return true;
        }

        private bool agregarParam(string metodoOrigen)
        {
            try
            {
                if (!validar(metodoOrigen))
                {
                    return false;
                }

                switch (metodoOrigen.ToLower())
                {
                    case "realizarconex":
                        objDatosUsuario = new SqlParameter[3];
                        objDatosUsuario[0] = new SqlParameter("@Nombre", strNombreUsuario);
                        objDatosUsuario[1] = new SqlParameter("@contrasena", strContrasena);
                        objDatosUsuario[2] = new SqlParameter("@Rol", strRol);
                        break;
                    case "registrarestudiante":
                        objDatosUsuario = new SqlParameter[9];
                        objDatosUsuario[0] = new SqlParameter("@nombre_usuario", strNombreUsuario);
                        objDatosUsuario[1] = new SqlParameter("@email", strEmail);
                        objDatosUsuario[2] = new SqlParameter("@contrasena", strContrasena);
                        objDatosUsuario[3] = new SqlParameter("@identificacion", strIdentificacion);
                        objDatosUsuario[4] = new SqlParameter("@nombre", strNombre);
                        objDatosUsuario[5] = new SqlParameter("@apellido", strApellido);
                        objDatosUsuario[6] = new SqlParameter("@fecha_nacimiento", strFechaNacimiento);
                        objDatosUsuario[7] = new SqlParameter("@telefono", strTelefono);
                        objDatosUsuario[8] = new SqlParameter("@identPadre", strIdentPadre);
                        break;
                    case "consusuario":
                        objDatosUsuario = new SqlParameter[2];
                        objDatosUsuario[0] = new SqlParameter("@formulario", formulario);
                        objDatosUsuario[1] = new SqlParameter("@identificacion", strIdentificacion);
                        break;
                    case "modificarusuario":
                        objDatosUsuario = new SqlParameter[10];

                        if (formulario.Equals("frmGestHijo"))
                        {
                            objDatosUsuario[9] = new SqlParameter("@identPadre", strIdentPadre);
                        }
                        else
                        {
                            objDatosUsuario[9] = new SqlParameter("@identPadre", "SIN INFO");
                        }
               
                        objDatosUsuario[0] = new SqlParameter("@formulario", formulario);
                        objDatosUsuario[1] = new SqlParameter("@identificacion", strIdentificacion);
                        objDatosUsuario[2] = new SqlParameter("@nombre_usuario", strNombreUsuario);
                        objDatosUsuario[3] = new SqlParameter("@email", strEmail);
                        objDatosUsuario[4] = new SqlParameter("@contrasena", strContrasena);
                        objDatosUsuario[5] = new SqlParameter("@nombre", strNombre);
                        objDatosUsuario[6] = new SqlParameter("@apellido", strApellido);
                        objDatosUsuario[7] = new SqlParameter("@fecha_nacimiento", strFechaNacimiento);
                        objDatosUsuario[8] = new SqlParameter("@telefono", strTelefono);
                        break;
                    case "eliminarusuario":
                        objDatosUsuario = new SqlParameter[2];
                        objDatosUsuario[0] = new SqlParameter("@formulario", formulario);
                        objDatosUsuario[1] = new SqlParameter("@identificacion", strIdentificacion);
                        break;
                }
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        #endregion

        #region "Métodos Públicos"
        public bool llenarDropDowns(DropDownList ddlGen)
        {
            try
            {
                String strCampoTexto = "";
                String strCampoId = "";
                switch (ddlGen.ID)
                {
                    case "ddlRol":
                        strCampoId = "id";
                        strCampoTexto = "descripcion";
                        break;
                    default:
                        strError = "Control desconocido";
                        return false;
                }
                
                clsLlenarCombos objLlenar = new clsLlenarCombos(strNombreApp);

                objLlenar.SQL = "SP_ConsultarRoles";
                objLlenar.CampoID = strCampoId;
                objLlenar.CampoTexto = strCampoTexto;
                objLlenar.ParametrosSQL = objDatosUsuario;

                if (!objLlenar.llenarComboWeb(ddlGen))
                {
                    strError = objLlenar.Error;
                    objLlenar = null;
                    return false;
                }
                objLlenar = null;
                return true;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }

        public bool consConex()
        {
            try
            {
                if (!validar("consConex"))
                {
                    return false;
                }

                clsConexionBd objCnx = new clsConexionBd(strNombreApp);

                objCnx.SQL = "SP_ConsultarConexion";
                objCnx.ParametrosSQL = objDatosUsuario;

                if (!objCnx.llenarDataSet(false, true))
                {
                    strError = objCnx.Error;
                    objCnx.cerrarCnx();
                    objCnx = null;
                    return false;
                }
                dsDatos = objCnx.DataSetLleno;
                objCnx.cerrarCnx();
                objCnx = null;
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
                if (!agregarParam("realizarConex"))
                {
                    return false;
                }

                objConex = new clsConexionBd(strNombreApp);
                objConex.SQL = "[SP_RealizarConexion]";
                objConex.ParametrosSQL = objDatosUsuario;

                if (!objConex.llenarDataSet(true, true))
                {
                    strError = objConex.Error;
                    objConex.cerrarCnx();
                    objConex = null;
                    return false;
                }
                dsDatos = objConex.DataSetLleno;
                objConex.cerrarCnx();
                objConex = null;
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
                if (!agregarParam("registrarEstudiante"))
                {
                    return false;
                }

                objConex = new clsConexionBd(strNombreApp);
                objConex.SQL = "[SP_RegistrarEstudiante]" +
                    "";
                objConex.ParametrosSQL = objDatosUsuario;

                if (!objConex.llenarDataSet(true, true))
                {
                    strError = objConex.Error;
                    objConex.cerrarCnx();
                    objConex = null;
                    return false;
                }
                dsDatos = objConex.DataSetLleno;
                objConex.cerrarCnx();
                objConex = null;
                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }

        public bool modificarUsuario()
        {
            try
            {
                if (!agregarParam("modificarUsuario"))
                {
                    return false;
                }
                clsConexionBd objCnx = new clsConexionBd(strNombreApp);

                objCnx.SQL = "[SP_ModificarUsuario]";
                objCnx.ParametrosSQL = objDatosUsuario;

                if (!objCnx.llenarDataSet(true, true))
                {
                    strError = objCnx.Error;
                    objCnx.cerrarCnx();
                    objCnx = null;
                    return false;
                }
                dsDatos = objCnx.DataSetLleno;
                objCnx.cerrarCnx();
                objCnx = null;
                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }

        public bool eliminarUsuario()
        {
            try
            {
                if (!agregarParam("eliminarUsuario"))
                {
                    return false;
                }
                clsConexionBd objCnx = new clsConexionBd(strNombreApp);

                objCnx.SQL = "[SP_EliminarUsuario]";
                objCnx.ParametrosSQL = objDatosUsuario;

                if (!objCnx.llenarDataSet(true, true))
                {
                    strError = objCnx.Error;
                    objCnx.cerrarCnx();
                    objCnx = null;
                    return false;
                }
                dsDatos = objCnx.DataSetLleno;
                objCnx.cerrarCnx();
                objCnx = null;
                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }

        public bool cargarEstudiantes()
        {
            try
            {
                objConex = new clsConexionBd(strNombreApp);
                objConex.SQL = "[SP_CargarEstudiantes]";
                objConex.ParametrosSQL = objDatosUsuario;

                if (!objConex.llenarDataSet(false, true))
                {
                    strError = objConex.Error;
                    objConex.cerrarCnx();
                    objConex = null;
                    return false;
                }
                dsDatos = objConex.DataSetLleno;
                objConex.cerrarCnx();
                objConex = null;
                return true;
            }
            catch (Exception ex)
            {

                strError = ex.Message;
                return false;
            }
        }

        public bool consultarUsuario()
        {
            try
            {
                if (!agregarParam("consUsuario"))
                {
                    return false;
                }
                clsConexionBd objCnx = new clsConexionBd(strNombreApp);

                objCnx.SQL = "[SP_ConsultarUsuario]";
                objCnx.ParametrosSQL = objDatosUsuario;

                if (!objCnx.llenarDataSet(true, true))
                {
                    strError = objCnx.Error;
                    objCnx.cerrarCnx();
                    objCnx = null;
                    return false;
                }
                dsDatos = objCnx.DataSetLleno;
                objCnx.cerrarCnx();
                objCnx = null;
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

                clsConexionBd objCnx = new clsConexionBd(strNombreApp);

                objCnx.SQL = "[SP_CerrarSesion]";
                objCnx.ParametrosSQL = objDatosUsuario;

                if (!objCnx.llenarDataSet(false, true))
                {
                    strError = objCnx.Error;
                    objCnx.cerrarCnx();
                    objCnx = null;
                    return false;
                }
                dsDatos = objCnx.DataSetLleno;
                objCnx.cerrarCnx();
                objCnx = null;
                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }
        #endregion
    }
}
