using System;
using ProjectoFinal.Data;

namespace ProjectoFinal
{
    class Program
    {
        static void Main(string[] args)
        {
            StateMachine appState = new StateMachine();

            try
            {
                SQLManager.AbrirConexion();
                Console.WriteLine("Papa ute e un mmg");
                SQLManager.CerrarConexion();
            }
            catch (Exception)
            {

                Console.WriteLine("error");

            }

            //appState.ChangeState(new InicioState(appState));
        }
    }
}
