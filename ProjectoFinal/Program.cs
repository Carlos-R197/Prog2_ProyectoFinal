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
            try
            {
                StateMachine appState = new StateMachine();
                appState.ChangeState(new InicioState());
            }
            catch (MySqlException)
            {
                Console.WriteLine("Necesita conexión a internet");
                Console.ReadKey();
            }
        }
    }
}
