using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//--------
using System.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using MySql.Data.MySqlClient;
using System.Data;
//-------

namespace cine2doseg.Models
{
    public class clsSucursal
    {
        public string nombre { get; set; }
        public string direccion { get; set; }
        public string url { get; set; }

        public string logo { get; set; }


        public clsSucursal()
        {

        }

        public clsSucursal(string nombre, string direccion, string url,string logo)
        {
            this.nombre = nombre;
            this.direccion = direccion;
            this.url = url;
            this.logo = logo;


        }

        private string cadCnn = ConfigurationManager.
            ConnectionStrings["CINE"].
            ConnectionString;


        public DataSet vwRptsucursales()
        {
            // Crear el comando SQL
            string cadSQL = "";
            cadSQL = "SELECT * FROM vwrptsucursales;";
            // Configuracion de objetos de conexion
            MySqlConnection cnn = new MySqlConnection(cadCnn);
            MySqlDataAdapter da = new MySqlDataAdapter(cadSQL, cadCnn); //da es por datadatos
            DataSet ds = new DataSet();
            //ejecucion y salida
            da.Fill(ds, "vwRptsucursales"); // <----------------
            return ds;
            //ds es igual al dataset

        }


        public DataSet spInsSucursales()
        {
            // Crear el comando SQL

            string cadSQL = "";
            cadSQL = "CALL spInsSucursales('" + this.nombre + "', '" + this.direccion + "', '" + this.url + "', '" + this.logo + "');";

            // Configuracion de objetos de conexion
            MySqlConnection cnn = new MySqlConnection(cadCnn);  
            MySqlDataAdapter da = new MySqlDataAdapter(cadSQL, cadCnn); //da es por datadatos
            DataSet ds = new DataSet();

            //ejecucion y salida
            da.Fill(ds, "spInsSucursales"); // <----------------
            return ds;
        }

    }
    
}
   