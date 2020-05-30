﻿using System;
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
        private int intIdUsuario;
        private string strNombreUsuario;
        private string strEmail;
        private string strContrasena;
        private string strIdentificacion;
        private string strNombre;
        private string strApellido;
        private string strFechaNacimiento;
        private string strTelefono;
        private string strNombreApp;
        private int intPadreId;
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
            
           
        }
        #endregion

        #region "Propiedades"

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
                        strError = "LÑa contraseña no puede estar vacia";
                        return false;
                    }
                    break;
            }
            return true;

        }

        #endregion

        #region "Métodos públicos"   

        /*public bool llenarDrop()
        {
            try
            {
                if (!validar("llenarDrop"))
                {
                    return false;
                }

                clsUsuariosRN objRn = new clsUsuariosRN(strNombreApp);
                switch (ddlGen.ID.ToLower())
                {
                    case "dll":
                        objRn.Asignatura = intIdAsignatura;
                        break;
                    case "ddlaula":
                        objRn.Asignatura = intIdAsignatura;
                        objRn.Docente = intIdDocente;
                        break;
                }


                if (!objRn.llenarDropDowns(ddlGen))
                {
                    strError = objRn.Error;
                    objRn = null;
                    return false;
                }
                objRn = null;
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }  
        */
        #endregion


    }
}
