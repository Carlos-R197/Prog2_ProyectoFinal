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
        private StateMachine appState;

        public SignInState(StateMachine machine)
        {
            this.appState = machine;
        }

        public void Enter()
        {
            Console.Clear();
            Console.Write("Escriba sus nombres y apellidos: ");
            string nombreCompleto = Console.ReadLine();
            Console.Write("Escriba su contraseña: ");
            string contraseña = Console.ReadLine(); 
            //Check if the perfil exist in the database y log in if it does.
            if (SQLManager.comprobar(nombreCompleto,contraseña) == true)
            {
                SQLManager.AbrirConexion();
                string Querycorreo = "Select correo from perfiles_registrados where nombre='" + nombreCompleto + "';";
                string Queryedad = "Select edad from perfiles_registrados where nombre='" + nombreCompleto + "';";
                MySqlCommand Correocomando = new MySqlCommand(Querycorreo, SQLManager.conexion);
                MySqlCommand Edadcomando = new MySqlCommand(Queryedad, SQLManager.conexion);
                string correo = Convert.ToString(Correocomando.ExecuteScalar());
                string tempedad = Convert.ToString(Edadcomando.ExecuteScalar());
                byte edad = byte.Parse(tempedad);
                Perfil perfil = new Perfil(nombreCompleto,contraseña,correo,edad);
                appState.ChangeState(new MainMenuState(appState,perfil));
                SQLManager.CerrarConexion();
                Console.WriteLine("Usuario Existe");
                //appState.ChangeState(new MainMenuState(appState, ));
            }
            else
            {
                Console.WriteLine("Datos Erroneos");

            }

            Thread.Sleep(1000);
        }
    }
}
