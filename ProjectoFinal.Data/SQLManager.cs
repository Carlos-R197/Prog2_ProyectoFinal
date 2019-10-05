using System;
using System.Collections.Generic;
using System.Data;
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

        public static void LlenarTabla(string nombre, string contrasena, string correo, byte edad)
        {
            string query = "INSERT perfiles_registrados (nombre,contrasena,correo,edad) VALUE ('"+nombre+"','"+ contrasena+"','" +correo+"','" +edad+"')";
            AbrirConexion();
            MySqlCommand comando = new MySqlCommand(query, conexion);
            comando.ExecuteNonQuery();
            CerrarConexion();
        }

        public static bool Comprobar(string nombre, string contrasena)
        {
            bool result = false;
            string query = "SELECT * FROM perfiles_registrados WHERE nombre='"+nombre+"' AND contrasena='"+contrasena+"'";
            AbrirConexion();
            MySqlCommand comando = new MySqlCommand(query, conexion);
            MySqlDataReader reg = null;
            reg = comando.ExecuteReader();

            if (reg.Read())
            {
                result = true;
            }
            else
                result = false;
            CerrarConexion();
            return result;
        }

        public static void LlenarTablaCirculo(string nombre)
        {
            string query = "INSERT INTO circulos VALUE('" + nombre + "')";
            AbrirConexion();
            MySqlCommand comando = new MySqlCommand(query, conexion);
            comando.ExecuteNonQuery();
            CerrarConexion();
        }

        public static void ImprimirTodosCirculos()
        {
            string query = "SELECT * FROM circulos";
            AbrirConexion();
            MySqlCommand comando = new MySqlCommand(query, conexion);
            DataTable table = new DataTable();

            MySqlDataAdapter dap = new MySqlDataAdapter(comando);
            
            dap.Fill(table);
            CerrarConexion();

            foreach (DataRow row in table.Rows)
            {
                foreach (var item in row.ItemArray)
                {
                    Console.WriteLine("{0}", item);
                }
            }
        }
        //Imprimira la columna de nombres de una tabla si le pasas el nombre de la tabla
        public static void ImprimirTodosNombre(string nombreTabla) 
        {
            string query = "SELECT nombre FROM " + nombreTabla;
            AbrirConexion();
            MySqlCommand comando = new MySqlCommand(query, conexion);
            DataTable table = new DataTable();

            MySqlDataAdapter dap = new MySqlDataAdapter(comando);

            dap.Fill(table);
            CerrarConexion();

            foreach (DataRow row in table.Rows)
            {
                foreach (var item in row.ItemArray)
                {
                    Console.WriteLine("{0}", item);
                }
            }
        }

        public static bool RevisaSiNombreExiste(string nombreTabla, string nombre)
        {
            bool result = false;
            string query = "Select nombre" + " FROM " + nombreTabla + " WHERE nombre = " + "\"" + nombre + "\"";
            AbrirConexion();
            MySqlCommand comando = new MySqlCommand(query, conexion);

            MySqlDataReader reg = null;
            reg = comando.ExecuteReader();

            if (reg.Read())
            {
                result = true;
            }
            CerrarConexion();
            return result;
        }
        
        public static Perfil ObtenPerfil(string nombre)
        {
            string query = "SELECT * FROM perfiles_registrados WHERE nombre = " + "\"" + nombre + "\"";
            AbrirConexion();
            MySqlCommand comando = new MySqlCommand(query, conexion);
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter(comando);

            adapter.Fill(table);
            CerrarConexion();

            Console.WriteLine(table.Rows.Count);
            byte edad = byte.Parse(table.Rows[0].ItemArray[3].ToString());

            return new Perfil((string)table.Rows[0].ItemArray[0], (string)table.Rows[0].ItemArray[1], (string)table.Rows[0].ItemArray[2], edad);
        }

        public static Perfil[] EncuentraPerfilesQueContienen(string nombre)
        {
            string query = "SELECT * FROM perfiles_registrados WHERE nombre LIKE '%" + nombre + "%' ";
            AbrirConexion();
            MySqlCommand comando = new MySqlCommand(query, conexion);
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter(comando);

            adapter.Fill(table);
            CerrarConexion();

            Perfil[] perfilArray = new Perfil[table.Rows.Count];

            for (int i = 0; i < table.Rows.Count; i++)
            {
                byte edad = byte.Parse(table.Rows[i].ItemArray[3].ToString());
                perfilArray[i] = new Perfil((string)table.Rows[i].ItemArray[0], (string)table.Rows[i].ItemArray[1], (string)table.Rows[i].ItemArray[2], edad);
            }

            return perfilArray;
        }

        public static void AñadirCirculo(Circulo nuevoCirculo)
        {
            string query = "INSERT INTO circulos VALUE('" + nuevoCirculo.Nombre + "')";
            AbrirConexion();
            MySqlCommand comando = new MySqlCommand(query, conexion);
            comando.ExecuteNonQuery();
            CerrarConexion();
        }
    }
}
