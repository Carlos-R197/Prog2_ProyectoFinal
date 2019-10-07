using System;
using System.Collections.Generic;
using System.Text;
using ProjectoFinal.Data;
using MySql.Data.Common;
using MySql.Data.MySqlClient;
using System.Threading;

namespace ProjectoFinal
{
    public class SignInState : IState
    {
        public void Handle(StateMachine appState)
        {
            Console.Clear();
            Console.Write("Escriba sus nombres y apellidos: ");
            string nombreCompleto = Console.ReadLine();
            Console.Write("Escriba su contraseña: ");
            string contraseña = Console.ReadLine(); 
            //Check if the perfil exist in the database y log in if it does.
            if (SQLManager.Comprobar(nombreCompleto,contraseña) == true)
            {
                SQLManager.AbrirConexion();
                string QueryCorreo = "Select correo from perfiles_registrados where nombre='" + nombreCompleto + "';";
                string QueryEdad = "Select edad from perfiles_registrados where nombre='" + nombreCompleto + "';";
                string QueryRating = "Select rating from perfiles_registrados where nombre='" + nombreCompleto + "';";
                MySqlCommand Correocomando = new MySqlCommand(QueryCorreo, SQLManager.conexion);
                MySqlCommand Edadcomando = new MySqlCommand(QueryEdad, SQLManager.conexion);
                MySqlCommand Ratingcomando = new MySqlCommand(QueryRating, SQLManager.conexion);
                string correo = Convert.ToString(Correocomando.ExecuteScalar());
                string tempedad = Convert.ToString(Edadcomando.ExecuteScalar());
                int rating = Convert.ToInt32(Ratingcomando.ExecuteScalar());
                byte edad = byte.Parse(tempedad);
                Perfil perfil = new Perfil(nombreCompleto,correo,contraseña,edad, rating);
                SQLManager.CerrarConexion();
                //Console.WriteLine("Usuario Existe");
                MainMenuState state = new MainMenuState();
                state.Inicializar(perfil); //Pasale los valores que necesitas al singleton.
                appState.ChangeState(state);
            }
            else
            {
                Console.WriteLine("Datos Erroneos");
            }
        }
    }
}
