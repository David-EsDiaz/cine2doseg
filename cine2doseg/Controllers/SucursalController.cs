pusing System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//-------------------------------
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using cine2doseg.Models;
//-------------------------------

namespace cine2doseg.Controllers
{
    public class SucursalController : ApiController
    {

        [HttpGet] //estas son directivas
        [Route("cine/sucursal/vwrptsucursales")]
        public DataSet vwRptsucursales()
        {
            DataSet ds = new DataSet();
            try
            {
                // Creacion del objeto usuario para la insercion 
                clsSucursal objUsuario = new clsSucursal();

                //Ejecucion del metodo de insercion y recepion de resultados
                ds = objUsuario.vwRptsucursales();
            }
            catch (Exception ex)
            {
                //Configurar el DataSet para salida
                //(Formateo con clsApiStatus
                DataTable dt = new DataTable("vwRptsucursales");
                dt.Columns.Add("statusExec");
                dt.Columns.Add("msg");
                dt.Columns.Add("ban");
                dt.Columns.Add("msgData");
                dt.Columns.Add("msgException");
                //Formateo de los datos de salida
                DataRow dr = dt.NewRow();
                dr["statusExec"] = "false";
                dr["msg"] = "Error en reporte de ususaios, verificar ...";
                dr["ban"] = "-1";
                dr["msgData"] = ex.Message.ToString();
                dr["msgException"] = ex.InnerException.ToString();
                //asignar datos de salida
                dt.Rows.Add(dr);
                ds.Tables.Add(dt);

            }
            //Return del DataSet con los datos recibidos
            // (o formateados dentro del catch
            return ds;
        }


        [HttpPost]
        [Route("cine/sucursal/spinssucursales")]
        public clsApiStatus spInsSucursales([FromBody] clsSucursal modelo)
        {
            clsApiStatus objRespuesta = new clsApiStatus();
            JObject jsonResp = new JObject();
            try
            {
                // Creacion del objeto usuario para la insercion 
                clsSucursal objSucursal = new clsSucursal(modelo.nombre,
                                                       modelo.direccion,
                                                       modelo.url,
                                                       modelo.logo);
                DataSet ds = new DataSet();
                //Ejecucion del metodo de insercion y recepion de resultados
                ds = objSucursal.spInsSucursales();
                //Configuracion de los atributos de salida
                objRespuesta.statusExec = true;
                objRespuesta.msg = "Registro de usuario exitoso (cine)";
                objRespuesta.ban = int.Parse(ds.Tables[0].Rows[0][0].ToString());
                jsonResp.Add("msgData", "Registro de usuario exitoso (cine)");
                objRespuesta.datos = jsonResp;

            }
            catch (Exception ex)
            {
                objRespuesta.statusExec = false;
                objRespuesta.msg = "Registro de usuario fallido (cine)";
                objRespuesta.ban = -1;
                jsonResp.Add("msgData", ex.Message.ToString());
                objRespuesta.datos = jsonResp;
            }
            return objRespuesta;
        }

    }
}
