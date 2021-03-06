﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using libEscuelaRN;
using libLlenarGrids;

namespace libEscuelaOpe
{
    public class clsGruposOPE
    {
        #region "Atributos"
        private string strNombreApp;
        private string strDescripcion;
        private string strHoraIn;
        private string strHoraFin;
        private string strDia;
        private int intIdDeporte;
        private int intIdProfesor;
        private string strNomProf;
        private string strError;
        private DataSet dsDatos;
        private string strMensaje;
        DropDownList ddlGen;
        GridView gvGen;
        #endregion

        #region "Constructor"
        public clsGruposOPE(string nombreApp)
        {
            this.strNombreApp = nombreApp;
            strError = string.Empty;
            strDescripcion = string.Empty; 
            strHoraIn = string.Empty;
            strHoraFin = string.Empty;
            strDia = string.Empty;
            intIdDeporte = -1;
            intIdProfesor = -1;
            strNomProf = string.Empty;
            strMensaje = string.Empty;
    }
        #endregion

        #region "Propiedades"
        public string Error { get => strError; }
        public DataSet DatosRpta { get => dsDatos; }
        public string Mensaje { get => strMensaje; }
        public DropDownList DdlGen { set => ddlGen = value; }
        public GridView GvGen { set => gvGen = value; }
        public string Descripcion { get => strDescripcion; set => strDescripcion = value; }
        public string HoraIn { get => strHoraIn; set => strHoraIn = value; }
        public string HoraFin { get => strHoraFin; set => strHoraFin = value; }
        public string Dia { get => strDia; set => strDia = value; }
        public string NomProf { get => strNomProf; set => strNomProf = value; }
        public int IdDeporte { get => intIdDeporte; set => intIdDeporte = value; }
        public int IdProfesor { get => intIdProfesor; set => intIdProfesor = value; }
        #endregion

        #region "Métodos Privados"
        private bool validar(string metodoOrigen)
        {
            if (strNombreApp == string.Empty)
            {
                strError = "Olvido enviar el nombre de la aplicación";
                return false;
            }
            switch (metodoOrigen)
            {
                case "registrarOpe":
                    if (strDescripcion == string.Empty)
                    {
                        strError = "La descripcion del grupo no puede estar vacia";
                        return false;
                    }
                    if (strHoraIn == string.Empty)
                    {
                        strError = "La fecha inicio no puede estar vacia";
                        return false;
                    }
                    if (strDia == string.Empty)
                    {
                        strError = "El día no puede estar vacio";
                        return false;
                    }
                    if (intIdDeporte <= 0)
                    {
                        strError = "Debe seleccionar un deporte";
                        return false;
                    }
                    if (intIdProfesor <= 0)
                    {
                        strError = "Debe seleccionar un profesor";
                        return false;
                    }
                    break;
                case "consultarGrupoConParOpe":
                    if (strDescripcion == string.Empty)
                    {
                        strError = "La descripcion del grupo no puede estar vacia";
                        return false;
                    }
                    break;
                case "modificarOpe":
                    goto case "registrarOpe";
                case "eliminarOpe":
                    goto case "consultarGrupoConParOpe";
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
                strError = ex.Message;
                return false;
            }
        }
        #endregion

        #region "Métodos públicos"

        public bool llenarDrop()
        {
            try
            {
                if (!validar("llenarDrop"))
                {
                    return false;
                }

                clsGruposRN objRn = new clsGruposRN(strNombreApp);
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
                strError = ex.Message;
                return false;
            }
        }

        public bool registrarOpe()
        {
            try
            {
                if (!validar("registrarOpe"))
                {
                    return false;
                }
                clsGruposRN objRn = new clsGruposRN(this.strNombreApp);

                objRn.Descripcion = strDescripcion;
                objRn.HoraIn = strHoraIn;
                objRn.HoraFin = strHoraFin;
                objRn.Dia = strDia;
                objRn.IdDeporte = intIdDeporte;
                objRn.IdProfesor = intIdProfesor;

                if (!objRn.registrar())
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
                clsGruposRN objRn = new clsGruposRN(this.strNombreApp);

                objRn.Descripcion = strDescripcion;
                objRn.HoraIn = strHoraIn;
                objRn.HoraFin = strHoraFin;
                objRn.Dia = strDia;
                objRn.IdDeporte = intIdDeporte;
                objRn.IdProfesor = intIdProfesor;

                if (!objRn.modificarGrupo())
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
                clsGruposRN objRn = new clsGruposRN(this.strNombreApp);

                objRn.Descripcion = strDescripcion;

                if (!objRn.eliminarGrupo())
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

        public bool cargarGrupoOpe()
        {
            try
            {
                if (!validar("cargarGrupoOpe"))
                {
                    return false;
                }
                clsGruposRN objRn = new clsGruposRN(this.strNombreApp);

                if (!objRn.cargarGrupo())
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

        public bool consultarGrupoConParOpe()
        {
            try
            {
                if (!validar("consultarGrupoConParOpe"))
                {
                    return false;
                }
                clsGruposRN objRn = new clsGruposRN(this.strNombreApp);

                objRn.Descripcion = strDescripcion;

                if (!objRn.consultarGrupo())
                {
                    strError = objRn.Error;
                    objRn = null;
                    return false;
                }

                if (objRn.DatosRptaBd.Tables[0].Rows.Count == 0)
                {
                    strError = "El grupo no existe";
                    return false;
                }
                strDescripcion = objRn.DatosRptaBd.Tables[0].Rows[0]["descripcion_grupo"].ToString();
                strHoraIn = objRn.DatosRptaBd.Tables[0].Rows[0]["hora_inicio"].ToString();
                strHoraFin = objRn.DatosRptaBd.Tables[0].Rows[0]["hora_fin"].ToString();
                strDia = objRn.DatosRptaBd.Tables[0].Rows[0]["dia"].ToString();
                intIdDeporte = Convert.ToInt32(objRn.DatosRptaBd.Tables[0].Rows[0]["deporte_id"].ToString());
                intIdProfesor = Convert.ToInt32(objRn.DatosRptaBd.Tables[0].Rows[0]["profesor_id"].ToString());
                strNomProf = objRn.DatosRptaBd.Tables[0].Rows[0]["nombre"].ToString();
                objRn = null;
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
