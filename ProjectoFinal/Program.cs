using System;
using ProjectoFinal.Data;
using MySql.Data.MySqlClient;
using System.Data;

namespace ProjectoFinal
{
    class Program
    {
        static void Main(string[] args)
        {
            //StateMachine appState = new StateMachine();

            //try
            //{
            //    SQLManager.AbrirConexion();
            //    Console.WriteLine("Papa ute e un mmg");
            //    SQLManager.CerrarConexion();
            //}
            //catch (Exception)
            //{

            //    Console.WriteLine("error");

            //}

            //appState.ChangeState(new InicioState(appState));
            Console.WriteLine("Escriba un nombre");
            string nombre = Console.ReadLine();
            DataTable table = new DataTable();
            using (MySqlCommand cmd = new MySqlCommand("SELECT nombre FROM perfiles_registrados WHERE nombre='"+nombre+"'", SQLManager.conexion))
            {
                MySqlDataAdapter dap = new MySqlDataAdapter(cmd);
                SQLManager.AbrirConexion();
                dap.Fill(table);
                SQLManager.CerrarConexion();
            }
            Console.WriteLine(table.Rows.Count);
            foreach (DataRow datarow in table.Rows)
            {
                foreach (var item in datarow.ItemArray)
                {

                    Console.WriteLine("{0} \n", item);

                }
            }

        }
    }
}
