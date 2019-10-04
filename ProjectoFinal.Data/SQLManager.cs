using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace ProjectoFinal.Data
{
    public static class SQLManager
    {
        public static MySqlConnection conexion = new MySqlConnection("Server=remotemysql.com; Database=QRqGefDOkx; Uid=QRqGefDOkx; Pwd=P80kXnXOFM;");

        public static void AbrirConexion()
        {
            conexion.Open();
        }

        public static void CerrarConexion()
        {
            conexion.Close();
        }

        public static void llenartabla(string nombre, string contrasena, string correo, byte edad)
        {
            string query = "INSERT perfiles_registrados(nombre,contrasena,correo,edad) VALUE($'"+nombre+"','"+ contrasena+"','" +correo+"','" +edad+"')";
            AbrirConexion();
            MySqlCommand comando = new MySqlCommand(query, conexion);
            comando.ExecuteNonQuery();
            CerrarConexion();
        }
    }
}
