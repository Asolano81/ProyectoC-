﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using libConexionBd;
using libLlenarGrids;
using libLlenarCombos;

namespace libEscuelaRN
{
    public class clsGruposRN
    {
        #region "Atributos"
        private string strTipoCons;
        private string strNombreApp;
        private string strDescripcion;
        private string strHoraIn;
        private string strHoraFin;
        private string strDia;
        private int intIdDeporte;
        private int intIdProfesor;
        private string strError;
        private SqlParameter[] objDatosGrupo;
        private clsConexionBd objConex;
        private clsLlenarGrids objLlenaGrids;
        private DataSet dsDatos;
        #endregion

        #region "Constructor"
        public clsGruposRN(string nombreApp)
        {
            this.strNombreApp = nombreApp;
            strError = string.Empty;
            strDescripcion = string.Empty;
            strHoraIn = string.Empty;
            strHoraFin = string.Empty;
            strDia = string.Empty;
            intIdDeporte = -1;
            intIdProfesor = -1;
        }
        #endregion

        #region "Propiedades"        
        public string Error { get => strError; }
        public DataSet DatosRptaBd { get => dsDatos; }
        public string Descripcion {set => strDescripcion = value; }
        public string HoraIn {set => strHoraIn = value; }
        public string HoraFin {set => strHoraFin = value; }
        public string Dia {set => strDia = value; }
        public int IdDeporte {set => intIdDeporte = value; }
        public int IdProfesor {set => intIdProfesor = value; }
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
                case "registrar":
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
                case "consultarGrupo":
                    if (strDescripcion == string.Empty)
                    {
                        strError = "La descripcion del grupo no puede estar vacia";
                        return false;
                    }
                    break;
                case "modificarGrupo":
                    goto case "registrar";
                case "eliminarOpe":
                    goto case "consultarGrupo";
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
                    case "registrarope":
                        objDatosGrupo = new SqlParameter[6];
                        objDatosGrupo[0] = new SqlParameter("@Descripcion", strDescripcion);
                        objDatosGrupo[1] = new SqlParameter("@HoraIn", strHoraIn);
                        objDatosGrupo[2] = new SqlParameter("@HoraFin", strHoraFin);
                        objDatosGrupo[3] = new SqlParameter("@Dia", strDia);
                        objDatosGrupo[4] = new SqlParameter("@Id_Deporte", intIdDeporte);
                        objDatosGrupo[5] = new SqlParameter("@Id_Profesor", intIdProfesor);
                        break;
                    case "llenardropdowns":
                        objDatosGrupo = new SqlParameter[1];
                        objDatosGrupo[0] = new SqlParameter("@TipoConsulta", strTipoCons);
                        break;
                    case "consgrupo":
                        objDatosGrupo = new SqlParameter[1];
                        objDatosGrupo[0] = new SqlParameter("@Descripcion", strDescripcion);
                        break;
                    case "modificargrupo":
                        goto case "registrarope";
                    case "eliminargrupo":
                        goto case "consgrupo";
                }
                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }
        #endregion

        #region "Métodos Públicos"

        public bool llenarDropDowns(DropDownList ddlGen)
        {
            try
            {
                string strCampoTexto = "";
                string strCampoId = "";
                switch (ddlGen.ID)
                {
                    case "ddlDeporte":
                        strTipoCons = "DEPORTES";
                        strCampoId = "id";
                        strCampoTexto = "descripcion";
                        break;
                    case "ddlProfesor":
                        strTipoCons = "PROFESORES";
                        strCampoId = "id";
                        strCampoTexto = "nom_profesor";
                        break;
                    default:
                        strError = "Control desconocido";
                        return false;
                }
                if (!agregarParam("llenarDropDowns"))
                {
                    return false;
                }
                clsLlenarCombos objLlenar = new clsLlenarCombos(strNombreApp);

                objLlenar.SQL = "SP_ConsultarComboGrupos";
                objLlenar.CampoID = strCampoId;
                objLlenar.CampoTexto = strCampoTexto;
                objLlenar.ParametrosSQL = objDatosGrupo;

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

        public bool registrar()
        {
            try
            {
                if (!agregarParam("registrarope"))
                {
                    return false;
                }

                objConex = new clsConexionBd(strNombreApp);
                objConex.SQL = "[SP_CrearGrupo]";
                objConex.ParametrosSQL = objDatosGrupo;

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

        public bool modificarGrupo()
        {
            try
            {
                if (!agregarParam("modificarGrupo"))
                {
                    return false;
                }
                clsConexionBd objCnx = new clsConexionBd(strNombreApp);

                objCnx.SQL = "[SP_ModificarGrupo]";
                objCnx.ParametrosSQL = objDatosGrupo;

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

        public bool eliminarGrupo()
        {
            try
            {
                if (!agregarParam("eliminarGrupo"))
                {
                    return false;
                }
                clsConexionBd objCnx = new clsConexionBd(strNombreApp);

                objCnx.SQL = "[SP_EliminarGrupo]";
                objCnx.ParametrosSQL = objDatosGrupo;

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

        public bool cargarGrupo()
        {
            try
            {
                objConex = new clsConexionBd(strNombreApp);
                objConex.SQL = "[SP_CargarGrupos]";
                objConex.ParametrosSQL = objDatosGrupo;

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

        public bool consultarGrupo()
        {
            try
            {
                if (!agregarParam("consGrupo"))
                {
                    return false;
                }
                clsConexionBd objCnx = new clsConexionBd(strNombreApp);

                objCnx.SQL = "[SP_ConsultarGrupo]";
                objCnx.ParametrosSQL = objDatosGrupo;

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

        #endregion
    }
}
