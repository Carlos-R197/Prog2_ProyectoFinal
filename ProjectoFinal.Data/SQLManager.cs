using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace ProjectoFinal.Data
{
    public static class SQLManager
    {
        public static MySqlConnection conexion = new MySqlConnection("Server=remotemysql.com; Database=QRqGefDOkx; Uid=QRqGefDOkx; Pwd=P80kXnXOFM; port=3306");
        private static string conexionString = "Server=remotemysql.com; Database=QRqGefDOkx; Uid=QRqGefDOkx; Pwd=P80kXnXOFM;";

        public static void AbrirConexion()
        {
            conexion.Open();
        }

        public static void CerrarConexion()
        {
            conexion.Close();
        }

        public static bool Comprobar(string nombre, string contrasena)
        {
            bool result = false;
            string query = "use QRqGefDOkx; SELECT * FROM perfiles_registrados WHERE nombre='" + nombre+"' AND contrasena='"+contrasena+"'";
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

            return new Perfil((string)table.Rows[0].ItemArray[0], (string)table.Rows[0].ItemArray[1], (string)table.Rows[0].ItemArray[2], edad, (int)table.Rows[0].ItemArray[4]);
        }
        public static void EnviarInvitacion(string nombreTabla, string nombre, Perfil perfil)
        {
            if (RevisaSiNombreExiste(nombreTabla, nombre))
            {
                string query = "INSERT INTO `invitaciones`(`invitador`, `invitado`) VALUE(\"" + perfil.Nombre + "\", \"" + nombre + "\")";
                SQLManager.EjecutarQuery(query);
                Console.WriteLine("Invitacion enviada");
            }
        }
        public static bool RevisaSiInvitado(Perfil perfil)
        {
            bool result = false;
            string query = "Select invitado FROM invitaciones WHERE invitado = \"" + perfil.Nombre + "\"";
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
        public static void VerInvitaciones(string nombretabla, Perfil perfil)
        {
            string query = "SELECT `invitador` FROM `invitaciones` WHERE `invitado` = \"" + perfil.Nombre + "\"";
            AbrirConexion();
            MySqlCommand comando = new MySqlCommand(query, conexion);
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter(comando);

            adapter.Fill(table);
            CerrarConexion();

            List<string> list = new List<string>();
            foreach (DataRow row in table.Rows)
            {
                list.Add(row[0].ToString());
            }
            Console.ForegroundColor = ConsoleColor.Cyan;
            foreach (string nombre in list)
            {
                Console.WriteLine(nombre);
            }
            Console.ResetColor();
        }
        public static string AceptarRechazar(Perfil perfil) 
        {
            char separador = ' ';
            int count = 2;
            string statement = Console.ReadLine();
            string[] desicion = statement.Split(separador, count, StringSplitOptions.RemoveEmptyEntries);

            switch (desicion[0].ToLower())
            {
                case "aceptar":
                     if (RevisaSiNombreExisteInvitacion("invitaciones", desicion[1]))
                    {
                        string query1 = "INSERT INTO `lista_amigos`(`usuario`, `amigo`) VALUE(\"" + desicion[1] + "\", \"" + perfil.Nombre + "\")";
                        string query2 = "DELETE FROM `invitaciones` WHERE `invitador` = \"" + desicion[1] + "\" AND `invitado` = \"" + perfil.Nombre + "\"";
                        SQLManager.EjecutarQuery(query1);
                        SQLManager.EjecutarQuery(query2);
                        Console.WriteLine("Invitacion aceptada");
                        Console.ReadLine();
                    }
                    return desicion[0];
                case "rechazar":
                    string query22 = "DELETE FROM `invitaciones` WHERE `invitador` = \"" + desicion[1] + "\" AND `invitado` = \"" + perfil.Nombre + "\"";
                    EjecutarQuery(query22);
                    Console.WriteLine("Invitacion rechazada");
                    Console.ReadLine();
                    return desicion[0];
                case "1":
                    return desicion[0];
                default:
                    return desicion[0];
            }
        }
        public static void ObtenerAmigos(Perfil perfil) 
        {
            string query1 = "SELECT `amigo` FROM `lista_amigos` WHERE `usuario` = \"" + perfil.Nombre + "\"";
            string query2 = "SELECT `usuario` FROM `lista_amigos` WHERE `amigo` = \"" + perfil.Nombre + "\"";
            AbrirConexion();
            MySqlCommand comando1 = new MySqlCommand(query1, conexion);
            MySqlCommand comando2 = new MySqlCommand(query2 , conexion);
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter(comando1);
            adapter.Fill(table);
            adapter.Dispose();
            MySqlDataAdapter adapter2 = new MySqlDataAdapter(comando2);
            adapter2.Fill(table);
            adapter2.Dispose();
            CerrarConexion();

            List<string> list = new List<string>();
            foreach (DataRow row in table.Rows)
            {
                if (row[0] != null)
                { list.Add(row[0].ToString()); }
                if (row[1] != null)
                { list.Add(row[1].ToString()); }
            }
            list = list.Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
            foreach (string nombre in list)
            {
                Console.WriteLine(nombre);
            }
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
                perfilArray[i] = new Perfil((string)table.Rows[i].ItemArray[0], (string)table.Rows[i].ItemArray[1], (string)table.Rows[i].ItemArray[2], edad, (int)table.Rows[i].ItemArray[4]);
            }

            foreach (Perfil perfil in perfilArray)
            {
                string PerfilNombre = MayusculasNombres(perfil.Nombre);
                Console.WriteLine("{0}", PerfilNombre);
            }

            return perfilArray;
        }

        public static DataTable ObtenTodosPost(string nombreCirculo)
        {
            DataTable table = new DataTable();
            string query = "SELECT * FROM posts WHERE circulo_pertenece = " + "'" + nombreCirculo + "'"; 
            using (MySqlConnection conexion = new MySqlConnection(conexionString))
            {
                conexion.Open();
                MySqlCommand comando = new MySqlCommand(query, conexion);
                MySqlDataAdapter adapter = new MySqlDataAdapter(comando);
                adapter.Fill(table);
            }
            return table;
        }

        public static DataTable ObtenTodosComentarios(string nombrePost)
        {
            DataTable table = new DataTable();
            string query = "SELECT * FROM comentarios WHERE post_pertenece = " + "'" + nombrePost + "'";
            using (MySqlConnection conexion = new MySqlConnection(conexionString))
            {
                conexion.Open();
                MySqlCommand comando = new MySqlCommand(query, conexion);
                MySqlDataAdapter adapter = new MySqlDataAdapter(comando);
                adapter.Fill(table);
            }
            return table;
        }

        public static DataTable ObtenComentarioAutor(string nombrePost)
        {
            DataTable table = new DataTable();
            string query = "SELECT comentario FROM posts WHERE nombre = " + "'" + nombrePost + "'";
            using (MySqlConnection conexion = new MySqlConnection(conexionString))
            {
                conexion.Open();
                MySqlCommand comando = new MySqlCommand(query, conexion);
                MySqlDataAdapter adapter = new MySqlDataAdapter(comando);
                adapter.Fill(table);
            }
            return table;
        }

        public static string MayusculasNombres(string nombre)
        {
            char[] MayusNombre = nombre.ToCharArray();
            // Handle the first letter in the string.  
            if (MayusNombre.Length >= 1)
            {
                if (char.IsLower(MayusNombre[0]))
                {
                    MayusNombre[0] = char.ToUpper(MayusNombre[0]);
                }
            }
            // Scan through the letters, checking for spaces.  
            // ... Uppercase the lowercase letters following spaces.  
            for (int i = 1; i < MayusNombre.Length; i++)
            {
                if (MayusNombre[i - 1] == ' ')
                {
                    if (char.IsLower(MayusNombre[i]))
                    {
                        MayusNombre[i] = char.ToUpper(MayusNombre[i]);
                    }
                }
            }
            return new string(MayusNombre);
        }

        public static void EjecutarQuery(params string[] querys)
        {
            foreach (string query in querys)
            {
                using (MySqlConnection conexion = new MySqlConnection(conexionString))
                {
                    conexion.Open();
                    MySqlCommand comando = new MySqlCommand(query, conexion);
                    comando.ExecuteNonQuery();
                }
            }
        }
         
        public static void CambiarPerfil(Perfil perfil, string nombreViejo)
        {
            string nombre = perfil.Nombre;
            string correo = perfil.Correo;
            string contraseña = perfil.Contraseña;
            byte edad = perfil.Edad;
            //Actualizar perfil
            string query = "UPDATE `perfiles_registrados` SET `nombre`= \"" + nombre + "\", `contrasena`= \""+contraseña+"\", `edad`= \""+edad+"\" WHERE `correo`= \""+correo+"\"" ;
            //Actualizar chat
            string query2 = "UPDATE `chat` SET `mensajero`= \"" + nombre + "\"  WHERE `mensajero`= \"" + nombreViejo + "\"";
            string query3 = "UPDATE `chat` SET `receptor`= \"" + nombre + "\"  WHERE `receptor`= \"" + nombreViejo + "\"";
            //Actualizar circulo_perfil
            string query4 = "UPDATE `circulos_perfiles` SET `perfil_nombre`= \"" + nombre + "\"  WHERE `perfil_nombre`= \"" + nombreViejo + "\"";
            //Actualizar comentarios
            string query5 = "UPDATE `comentarios` SET `autor`= \"" + nombre + "\"  WHERE `autor`= \"" + nombreViejo + "\"";
            //Actualizar invitaciones
            string query6 = "UPDATE `invitaciones` SET `invitador`= \"" + nombre + "\"  WHERE `invitador`= \"" + nombreViejo + "\"";
            string query7 = "UPDATE `invitaciones` SET `invitado`= \"" + nombre + "\"  WHERE `invitado`= \"" + nombreViejo + "\"";
            //Actualizar lista_amigos
            string query8 = "UPDATE `lista_amigos` SET `usuario`= \"" + nombre + "\"  WHERE `usuario`= \"" + nombreViejo + "\"";
            string query9 = "UPDATE `lista_amigos` SET `amigo`= \"" + nombre + "\" WHERE `amigo`= \"" + nombreViejo + "\"";
            //Actualizar post
            string query10 = "UPDATE `posts` SET `autor`= \"" + nombre + "\"  WHERE `autor`= \"" + nombreViejo + "\"";

            SQLManager.EjecutarQuery(query, query2, query3, query4, query5, query6, query7, query8, query9, query10);
            Console.WriteLine("Modificado exitosamente");
        }
        public static bool RevisaSiNombreExisteInvitacion(string nombreTabla, string nombre)
        {
            bool result = false;
            string query = "Select invitador" + " FROM " + nombreTabla + " WHERE invitador = " + "\"" + nombre + "\"";
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

        public static void GuardarChat(Chat chat)
        {
            string mensajero = chat.Perfil1.Nombre;
            string receptor = chat.Perfil2.Nombre;
            string mensaje = chat.Conversacion.ToString();

            string query = "insert into chat(mensajero,receptor,mensaje) value('" + mensajero + "','" + receptor + "','" + mensaje + "')";
            AbrirConexion();
            MySqlCommand cmd = new MySqlCommand(query, conexion);
            cmd.ExecuteNonQuery();
            CerrarConexion();
        }
        
        public static void VerMensajes(Chat chat)
        {
            string mensajero = chat.Perfil1.Nombre;
            string receptor = chat.Perfil2.Nombre;

            string query = "select mensaje from chat where mensajero='"+mensajero+"'and receptor='"+receptor+"' OR mensajero='"+receptor+"' and receptor='"+mensajero+"'";
            AbrirConexion();
            MySqlCommand cmd = new MySqlCommand(query, conexion);
            DataTable table = new DataTable();

            MySqlDataAdapter dap = new MySqlDataAdapter(cmd);

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

        public static void VerChats(Perfil perfil)
        {
            string mensajero = perfil.Nombre;


            string query = "select receptor from chat where mensajero='"+mensajero+"'";
            AbrirConexion();
            MySqlCommand cmd = new MySqlCommand(query, conexion);
            DataTable table = new DataTable();

            MySqlDataAdapter dap = new MySqlDataAdapter(cmd);
            dap.Fill(table);
            CerrarConexion();

            DataView vista = new DataView(table);
            DataTable dt = vista.ToTable(true, "receptor");

            foreach (DataRow row in dt.Rows)
            {
                foreach (var item in row.ItemArray)
                {
                    Console.WriteLine("{0}", item);

                }
            }

        }

        public static Post ObtenPost(string nombrePost)
        {
            string query = "SELECT * FROM posts WHERE nombre = " + "'" + nombrePost + "'";
            DataTable table = new DataTable();
            using (MySqlConnection conexion = new MySqlConnection(conexionString))
            {
                conexion.Open();
                MySqlCommand comando = new MySqlCommand(query, conexion);
                MySqlDataAdapter adapter = new MySqlDataAdapter(comando);
                adapter.Fill(table);
            }
            int edad = int.Parse(table.Rows[0].ItemArray[7].ToString());
            Post post = new Post(table.Rows[0].ItemArray[0].ToString(), table.Rows[0].ItemArray[1].ToString(), table.Rows[0].ItemArray[4].ToString(), 
                (DateTime)table.Rows[0].ItemArray[5], edad);
            return post;
        }
    }
}
